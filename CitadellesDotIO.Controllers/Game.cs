using CitadellesDotIO.Enums;
using CitadellesDotIO.Enums.TurnChoices;
using CitadellesDotIO.Extensions;
using CitadellesDotIO.Model.Characters;
using CitadellesDotIO.Model.Districts;
using CitadellesDotIO.Model;
using CitadellesDotIO.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations.Schema;
using CitadellesDotIO.Exceptions;
using CitadellesDotIO.Model.Spells;
using CitadellesDotIO.Model.Targets;

namespace CitadellesDotIO.Controllers
{
    public class Game
    {
        private int turnCount = 0;
        private const int InitialGold = 2;
        private const int InitialDeck = 4;
        private readonly bool ApplyKingShuffleRule;
        private readonly ImmutableList<Character> CharactersRoaster;
        private bool IsLastTableRound => this.Players.Any(p => p.HasReachedDistrictThreshold);
        public IView View { get; set; }
        public GameState GameState { get; set; }
        public List<Character> CharactersDeck { get; set; }
        public List<Character> CharactersBin { get; set; }
        public Deck<District> DistrictsDeck { get; set; }
        public List<Player> Players { get; set; }
        private Player CurrentKing => this.Players.SingleOrDefault(p => p.IsCurrentKing);
        public int TurnCount => this.turnCount;
        public Game()
        {
        }
        public Game(IEnumerable<Player> players, ICollection<Character> characters, ICollection<District> districts, IView view, bool applyKingShuffleRule = true, int districtThreshold = 7)
        {
            this.GameState = GameState.Starting;
            this.View = view;
            this.ApplyKingShuffleRule = applyKingShuffleRule;

            // Gestion de la pioche et de la défausse des districts
            this.DistrictsDeck = new Deck<District>(districts.OrderBy(_ => Dice.Roll(100)).ToList());
            // Gestion des joueurs
            this.Players = players.ToList();
            this.Players.ForEach(p => p.DistrictThreshold = districtThreshold);
            this.ShufflePlayers();
            this.PickInitialHandAndGold();
            // Gestion de la pioche et de la défausse des personnages
            this.CharactersRoaster = characters.ToImmutableList();
            this.CharactersBin = new List<Character>();
        }
        private void SetNewKing(Player newKing)
        {
            // Destitution de l'ancien Roi s'il existe
            if (this.CurrentKing != null)
            {
                this.CurrentKing.IsCurrentKing = false;
            }

            // Investiture du nouveau Roi
            newKing.IsCurrentKing = true;
        }
        private void PickInitialHandAndGold() => this.Players.ForEach(p =>
        {
            // Pour l'instant 4 cartes, voir pour paramétrer
            // Les index commencent à 0 mais les humains distribuent la première carte en disant 1
            for (int i = 1; i < InitialDeck; i++)
            {
                p.PickDistrict(this.DistrictsDeck.PickCard());
            }
            p.Gold = InitialGold;
        });
        private void OrderPlayers()
        {
            if (this.CurrentKing == null)
            {
                this.Players.First().IsCurrentKing = true;
            }
            else
            {
                this.Players.SetFirstElement(this.CurrentKing);
            }
            this.Players.ForEach(p => p.Character = null);
        }
        private void ShuffleCharacters()
        {
            // Vidage de la défausse
            this.CharactersBin.Clear();
            // Mélange aléatoire
            this.CharactersDeck = new List<Character>(this.CharactersRoaster);
            this.CharactersDeck = this.CharactersDeck.OrderBy(_ => Dice.Roll(100)).ToList();
            this.CharactersDeck.ForEach(c => c.Reset());
            this.Players.ForEach(p => p.Character = null);
        }
        private void PrepareCharactersDistribution()
        {
            int visibleCharactersCount = 0;
            int hiddenCharactersCount = 0;
            switch (this.Players.Count)
            {
                case 4:
                    visibleCharactersCount = 2;
                    hiddenCharactersCount = 1;
                    break;
                case 5:
                    visibleCharactersCount = 1;
                    hiddenCharactersCount = 1;
                    break;
                case 6:
                case 7:
                    hiddenCharactersCount = 1;
                    break;
                default: throw new NotImplementedException("4 à 7 joueurs pour l'instant svp");
            }

            // Ajout des cartes cachées à la défausse
            this.CharactersBin.AddRange(this.CharactersDeck.DrawElements(hiddenCharactersCount));

            // Ajout des cartes faces visibles à la défausse
            List<Character> visibleCharacters = this.CharactersDeck.DrawElements(visibleCharactersCount);
            visibleCharacters.ForEach(c =>
            {
                c.Flip();
                this.CharactersBin.Add(c);
            });

            // Si le roi est parmis les cartes visibles, on remélange
            if (ApplyKingShuffleRule && this.CharactersBin.Any(c => c.IsVisible && c.Name == nameof(King)))
            {
                this.ShuffleCharacters();
                this.PrepareCharactersDistribution();
            }
        }
        private void PickCharacters()
        {
            Players.ForEach(p =>
            {
                Character pickedCharacter = this.View.PickCharacter(this.CharactersDeck);
                p.PickCharacter(CharactersDeck.DrawElement(pickedCharacter));
            });
            this.GameState = GameState.TableRoundPhase;
        }

        public bool Run()
        {
            this.GameState = GameState.CharacterPickPhase;
            while (this.GameState != GameState.Finished)
            {
                switch (this.GameState)
                {
                    case GameState.CharacterPickPhase:
                        this.OrderPlayers();
                        this.RecoverDestroyedDistricts();
                        this.ShuffleCharacters();
                        this.PrepareCharactersDistribution();
                        this.PickCharacters();
                        break;
                    case GameState.TableRoundPhase:
                        this.PlayTableRound();
                        turnCount++;
                        break;
                }
            }
            // Récupération des personnages pour que les joueurs n'y soit plus associés
            this.ShuffleCharacters();
            this.ComputeScores();
            return true;
        }
        private void RecoverDestroyedDistricts()
        {
            this.Players.Select(p => p.City.Where(d => !d.IsBuilt)).Flatten().ToList()
                .ForEach(d =>
                {
                    d.Reset();
                    this.DistrictsDeck.Enqueue(d);
                });
        }
        private void ComputeScores() =>
            this.Players.ForEach(p => p.ComputeScore());
        public IEnumerable<Player> GetRanking()
            => this.Players.OrderByDescending(p => p.Score);
        private void PlayTableRound()
        {
            List<Character> characters = this.Players.Select(p => p.Character).OrderBy(c => c.Order).ToList();
            characters.ForEach(c => this.PlayCharacterRound(c));
            // Après le tour de table, si personne n'as terminé, on repart sur la phase de séléction des personnages
            this.GameState = this.IsLastTableRound ? GameState.Finished : GameState.CharacterPickPhase;
        }
        private void PlayCharacterRound(Character character)
        {
            // On montre la carte du personnage courant s'il n'est pas assassiné
            if (!character.IsMurdered)
            {
                character.Flip();

                HandlePassives(character.Player);

                this.HandleThievery(character);

                HandleTrading(character);

                this.HandleMandatoryTurnChoice(character);

                this.HandleUnorderedTurnChoices(character);

                this.CharactersBin.Add(character);
            }

            // La couronne passe même si le roi a été assassiné
            this.HandleKingship(character);
        }
        private void PickDistrictInPool(Character character)
        {
            // Pioche des deux premières cartes
            List<District> districtPool = this.GenerateDistrictPool(character.Player.PoolSize);
            // Choix des districts à garder
            List<District> pickedDistrics = this.View.PickDistrictsFromPool(character.Player.PickSize, districtPool);
            // Défausse des districts non choisis sous la pioche
            districtPool.Except(pickedDistrics).ToList().ForEach(d => this.DistrictsDeck.Enqueue(d));
            // Ajout des districts choisis à la main du joueur
            character.Player.PickDistricts(pickedDistrics);
        }

        private void BuildDistrict(Character character)
        {
            District toBuild = this.View.PickDistrictToBuild(character.Player.BuildableDistricts);
            if (toBuild != null)
            {
                character.Player.BuildDistrict(toBuild);

                // Si le joueur atteint le seuil de districts à construire et qu'aucun autre joueur ne l'a atteint
                if (IsLastTableRound &&
                   !this.Players.Any(p => p.IsFirstReachingDistrictThreshold))
                {
                    character.Player.IsFirstReachingDistrictThreshold = true;
                }
            }
        }

        private void CastSpell(Spell spell)
        {
            if (spell.HasTargets)
            {
                if (spell.HasToPickTargets)
                {
                    ITarget target = this.PickSpellTarget(spell.Targets);
                    spell.Cast(target);
                }
                else
                {
                    spell.Cast();
                }
            }
        }

        private District PickDistrictSpellSource(List<District> districts) => this.View.PickDistrictSpellSource(districts);

        private void HandleCharacterSpellTargets(Character character)
        {
            if (character.HasSpell)
            {
                // Liste contenant l'ensemble des cibles avant application des règles du Spell
                List<ITarget> availableTargets = new();
                switch (character.Spell.TargetType.Name)
                {
                    case nameof(ISwappable):
                        // Les swappables sont les joueurs et le deck des quartiers, on les ajoutes en cible
                        availableTargets.Add(this.DistrictsDeck);
                        availableTargets.AddRange(this.Players);
                        break;
                    case nameof(District):
                        availableTargets.AddRange(this.Players.Select(p => p.BuiltDistricts).Flatten());
                        break;
                    case nameof(Character):
                        availableTargets.AddRange(this.CharactersRoaster.ToList());
                        break;
                    case nameof(IDeck):
                        availableTargets.Add(this.DistrictsDeck);
                        break;
                    case nameof(ITarget):
                        break;
                    default: break;
                }
                // Calcul des cibles pour le spell en question
                character.Spell.GetAvailableTargets(availableTargets);
            }
        }

        private void HandleDistrictSpellTargets(IEnumerable<District> spellSources)
        {
            // Liste contenant l'ensemble des cibles avant application des règles du Spell
            foreach (Spell spell in spellSources.Select(ss => ss.Spell))
            {
                if (spell.HasTargetType)
                {
                    List<ITarget> availableTargets = new();
                    switch (spell.TargetType.Name)
                    {
                        case nameof(IDealable):
                            availableTargets.Add(this.DistrictsDeck);
                            availableTargets.AddRange(this.Players.Select(p => p.BuiltDistricts).Flatten());
                            break;
                        case nameof(IDeck):
                            availableTargets.Add(this.DistrictsDeck);
                            break;
                    }
                    spell.GetAvailableTargets(availableTargets);
                }
                else spell.GetAvailableTargets();
            }
        }

        private ITarget PickSpellTarget(List<ITarget> availableTargets) => this.View.PickSpellTarget(availableTargets);

        private List<District> GenerateDistrictPool(int poolSize)
        {
            List<District> pool = new();
            for (int i = 0; i < poolSize; i++)
            {
                pool.Add(this.DistrictsDeck.PickCard());
            }

            return pool;
        }

        private void ShufflePlayers()
        {
            this.Players = this.Players.OrderBy(_ => Dice.Roll(100)).ToList();
        }

        private void HandleKingship(Character character)
        {
            // Si le personnage est le roi, le joueur récupère la couronne
            if (character is King)
            {
                this.SetNewKing(character.Player);
            }
        }

        private void HandleThievery(Character character)
        {
            // Le personnage détroussé l'est au début de son tour
            if (character.IsStolen)
            {
                Player thief = this.Players.SingleOrDefault(p => p.Character is Thief);
                if (thief == null)
                {
                    throw new CharacterBehaviourException("Un personnage a été volé mais il n'y a pas de voleur");
                }
                // Récupération du butin
                int stolenGold = character.Player.Gold;
                // Mise à 0 du trésor de la victime
                character.Player.Gold = 0;
                // Ajout du butin au trésor du voleur
                thief.Gold += stolenGold;
            }
        }

        private static void HandlePassives(Player player)
        {
            player.ResetPickSize();
            player.ResetPoolSize();
            player.ResetTurnBuildingCap();
            if (player.Character.HasPassive)
            {
                player.Character.Passive.Apply();
            }
            foreach (District district in player.DistrictPassiveSources)
            {
                district.Passive.Apply();
            }
        }

        private static void HandleTrading(Character character)
        {
            // Le marchand prend une pièce bonus quoi qu'il arrive
            if (character is Merchant)
            {
                character.Player.Gold += 1;
            }
        }

        private void HandleMandatoryTurnChoice(Character character)
        {
            MandatoryTurnChoice turnChoice = this.View.PickMandatoryTurnChoice();

            // Le joueur prend l'argent
            if (turnChoice == MandatoryTurnChoice.BaseIncome)
            {
                character.Player.Gold += 2;
            }
            // Le joueur prend la pioche
            else
            {
                this.PickDistrictInPool(character);
            }
        }

        private void HandleUnorderedTurnChoices(Character character)
        {
            // Tant que le joueur n'a pas terminé son tour
            while (!character.Player.TakenChoices.Contains(UnorderedTurnChoice.EndTurn.ToString()))
            {
                // Raffraichissement des cibles potentielles
                this.HandleCharacterSpellTargets(character);
                HandleDistrictSpellTargets(character.Player.DistrictSpellSources);

                UnorderedTurnChoice currentChoice = this.View.PickUnorderedTurnChoice(character.Player.AvailableChoices);

                // Ajout du choix courant à la liste des choix pris
                switch (currentChoice)
                {
                    case UnorderedTurnChoice.BonusIncome:
                        character.PercieveBonusIncome();
                        character.Player.TakenChoices.Add(currentChoice.ToString());
                        break;
                    case UnorderedTurnChoice.BuildDistrict:
                        this.BuildDistrict(character);
                        character.Player.TakenChoices.Add(currentChoice.ToString());
                        break;
                    case UnorderedTurnChoice.CastCharacterSpell:
                        this.CastSpell(character.Spell);
                        character.Player.TakenChoices.Add(currentChoice.ToString());
                        break;
                    case UnorderedTurnChoice.CastDistrictSpell:
                        District casterDistrict = this.PickDistrictSpellSource(character.Player.DistrictSpellSources.ToList());
                        this.CastSpell(casterDistrict.Spell);
                        character.Player.TakenChoices.Add(casterDistrict.Name);
                        break;
                    case UnorderedTurnChoice.EndTurn:
                        character.Player.TakenChoices.Add(currentChoice.ToString());
                        break;
                }
            }
        }
    }
}
