using CitadellesDotIO.Config;
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
using System.Security.Cryptography;

namespace CitadellesDotIO.Controllers
{
    public class GameController
    {
        private bool ApplyKingShuffleRule;
        public IView View {get;set;} 
        public GameState GameState { get; set; }
        public List<Player> Players { get; set; }
        public List<Character> CharactersDeck { get; set; }
        public List<Character> CharactersBin { get; set; }
        public Queue<District> DistrictsDeck { get; set; }
        public List<District> DistrictsBin { get; set; }


        public void Run(){
            while (this.GameState != GameState.Finished)
            {
                switch (this.GameState)
                {
                    case GameState.CharacterPickPhase:
                        this.PickCharacters();
                        break;
                    case GameState.TableRoundPhase:
                        Console.WriteLine("Should start table round phase");
                        this.PlayTableRound();
                        //this.SimulateTableRound();
                        break;
                }
            }            
        }

        // Méthode temporaire permettant de mettre fin à la partie pour les test unitaires
        public void SimulateTableRound()
        {
            this.GameState = GameState.Finished;
        }

        public void PickCharacters(){            
            this.PrepareCharactersDistribution();
            // Tant que tous les joueurs n'ont pas de personnages
            while (this.Players.Any(p=>!p.HasPickedCharacter))
            {
                Player currentPicker = this.Players.FirstOrDefault(p=>!p.HasPickedCharacter);
                if (currentPicker != null)
                {
                    Character pickedCharacter = this.View.PickCharacter(this.CharactersDeck);
                    // Liaison symétrique entre joueur et personnage
                    currentPicker.PickCharacter(pickedCharacter);                    
                    this.CharactersDeck.Remove(currentPicker.Character);
                }
            }
            this.GameState = GameState.TableRoundPhase;
            this.Run();
        }

        public void PlayTableRound()
        {
            List<Character> characters = this.Players.Select(p=> p.Character).OrderBy(c=>c.Order).ToList();
            foreach(Character character in characters)           
            {
                this.PlayCharacterRound(character);
            }
            this.GameState = GameState.TableRoundOver;
        }

        public void PlayCharacterRound(Character character)
        {
            // On montre la carte du personnage courant s'il n'est pas assassiné
            if (!character.IsMurdered)
            {
                character.Flip();
                this.SetNewKing(character);

                // Le personnage détroussé l'est au début de son tour
                if (character.IsStolen)
                {
                    // Récupération du butin
                    int stolenGold = character.Player.Gold;
                    // Mise à 0 du trésor de la victime
                    character.Player.Gold = 0;
                    // Ajout du butin au trésor du voleur
                    this.Players.SingleOrDefault(p => p.Character is Thief).Gold += stolenGold;
                }

                // Le marchand prend une pièce bonus quoi qu'il arrive
                if (character is Merchant)
                {
                    character.Player.Gold += 1;
                }

                MandatoryTurnChoice turnChoice = this.View.PickMandatoryTurnChoice();

                // Le joueur prend l'argent
                if(turnChoice == MandatoryTurnChoice.BaseIncome)
                {
                    character.Player.Gold += 2;                    
                }
                // Le joueur prend la pioche
                else
                {
                    this.PickDistrictInPool(character);   
                }

                while (!character.Player.TakenChoices.Contains(UnorderedTurnChoice.EndTurn)){

                    UnorderedTurnChoice currentChoice = this.View.PickUnorderedTurnChoice(character.Player.AvailableChoices);

                    // Ajout du choix courant à la liste des choix pris => Peut etre déplacer ca dans les actions associées
                    character.Player.TakenChoices.Add(currentChoice);

                    switch(currentChoice)
                    {
                        case UnorderedTurnChoice.BonusIncome:
                            this.PercieveBonusIncome(character);
                            break;
                        case UnorderedTurnChoice.BuildDistrict:
                            this.BuildDistrict(character);
                            break;
                        case UnorderedTurnChoice.UseCharacterSpell:
                            // TODO : Gérer le spell du perso
                            break;
                        case UnorderedTurnChoice.EndTurn:
                            break;
                    }                    
                }                
            }
        }

        public void PickDistrictInPool(Character character)
        {
            // Pioche des deux premières cartes
            List<District> districtPool = this.GenerateDistrictPool(2);
            // Choix des districts à garder
            List<District> pickedDistrics = this.View.PickDistrictsFromPool(1, districtPool);
            // Défausse des districts non choisis
            this.DistrictsBin.AddRange(districtPool.Except(pickedDistrics));
            // Ajout des districts choisis à la main du joueur
            character.Player.DistrictsDeck.AddRange(pickedDistrics);
        }

        public void PercieveBonusIncome(Character character)
        {
            if (character.HasAssociatedDistrictType)
            {
                int bonusIncome = character.Player.BuiltDistricts.Count(d => d.DistrictType == character.AssociatedDistrictType);
                character.Player.Gold += bonusIncome;
            }
        }

        public void BuildDistrict(Character character)
        {
            // Les quartiers constructibles sont ceux que le joueur peut s'offrir et ceux qui ne sont pas déja construits
            List<District> buildables = character.Player.DistrictsDeck.Where(
                d => d.BuildingCost <= character.Player.Gold
                && !character.Player.BuiltDistricts.Any(bd=> bd.Name == d.Name)).ToList();

            District toBuild = this.View.PickDistrictToBuild(buildables);
            if (toBuild != null)
            {
                character.Player.Gold -= toBuild.BuildingCost;
                character.Player.BuiltDistricts.Add(toBuild);
            }
        }

        public List<District> GenerateDistrictPool(int poolSize)
        {
            List<District> pool = new List<District>();
            for (int i=0; i<poolSize; i++)
            {
                pool.Add(this.DistrictsDeck.Dequeue());
            }
            return pool;
        }

        public void SetNewKing(Character character)
        {
            if (character is King)
            {
                // Destitution de l'ancien Roi s'il existe
                Player oldKing = this.Players.SingleOrDefault(p => p.IsCurrentKing);
                if(oldKing != null)
                {
                    oldKing.IsCurrentKing = false;
                }

                // Investiture du nouveau Roi
                character.Player.IsCurrentKing = true;
                this.Players.SetFirstElement(character.Player);
            }
        }

        public void StartNewVanillaGame(List<Player> players, IView view)
        {
            // Ajout et mélange des joueurs à la partie
            this.Players = players;
            this.CharactersDeck = CharactersLists.VanillaCharactersList;
            this.CharactersBin = new List<Character>();
            this.DistrictsDeck = DistrictLists.TestDistrictList();
            foreach (Player player in players)
            {
                player.Gold = 2;
                player.DistrictsDeck = new List<District>();
                for (int i=0;i<3;i++)
                {
                    player.DistrictsDeck.Add(this.DistrictsDeck.Dequeue());
                }
                player.BuiltDistricts = new List<District>();
            }
            this.DistrictsBin = new List<District>();
            this.ApplyKingShuffleRule = true;
            this.ShufflePlayers();
            // Ajout de la vue
            this.View = view;
        }

        private void ShufflePlayers()
        {
            this.Players = this.Players.OrderBy(_ => RandomNumberGenerator.GetInt32(0,100)).ToList();
        }     

        private void ShuffleCharacters()
        {
            // Récupération de la défausse des personnages
            if(this.CharactersBin != null) { 
                this.CharactersDeck.AddRange(this.CharactersBin);
            }
            else
            {
                this.CharactersBin = new List<Character>();
            }
            // Vidage de la défausse
            this.CharactersBin.Clear();
            // Mélange aléatoire
            this.CharactersDeck = this.CharactersDeck.OrderBy(_ => RandomNumberGenerator.GetInt32(0, 100)).ToList();
        }

        public void PrepareCharactersDistribution()
        {
            this.ShuffleCharacters();
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
                    hiddenCharactersCount++;
                    break;
                default: throw new NotImplementedException("4 à 7 joueurs pour l'instant svp");
            }
            // Ajout des cartes faces visibles à la défausse
            this.CharactersBin.AddRange(this.CharactersDeck.GetRange(0, visibleCharactersCount));
            this.CharactersBin.ForEach(c => c.Flip());
            // Si le roi est parmis les cartes visibles, on remélange
            // TODO : Voir comment faire pour notifier l'interface / Si on notifie l'interface
            if (this.ApplyKingShuffleRule && this.CharactersBin.Any(c => c.Name == nameof(King))){
                this.PrepareCharactersDistribution();
            }
    
            // Ajout des cartes cachées à la défausse
            this.CharactersBin.AddRange(this.CharactersDeck.GetRange(visibleCharactersCount, hiddenCharactersCount));

            // Suppression des cartes écartées du deck
            this.CharactersDeck.RemoveRange(0, visibleCharactersCount + hiddenCharactersCount);            
        }
    }
}
