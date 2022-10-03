using CitadellesDotIO.Config;
using CitadellesDotIO.Enums;
using CitadellesDotIO.Model;
using CitadellesDotIO.Model.Characters;
using CitadellesDotIO.View;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public Queue<Object> DistrictsList { get; set; }
        public List<Object> DistricstBin { get; set; }


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
                        this.SimulateTableRound();
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
                    currentPicker.Character = this.View.PickCharacter(this.CharactersDeck);
                    this.CharactersDeck.Remove(currentPicker.Character);
                }
            }
            this.GameState = GameState.TableRoundPhase;
            this.Run();
        }

        public void StartNewVanillaGame(List<Player> players, IView view)
        {
            // Ajout et mélange des joueurs à la partie
            this.Players = players;
            this.CharactersDeck = CharactersLists.VanillaCharactersList;
            this.ApplyKingShuffleRule = true;
            this.ShufflePlayers();
            // Ajout de la vue
            this.View = view;
        }

        private void ShufflePlayers()
        {
            this.Players = this.Players.OrderBy(_ => new Random().Next()).ToList();
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
            this.CharactersDeck = this.CharactersDeck.OrderBy(_ => new Random().Next()).ToList();
        }

        public void PrepareCharactersDistribution()
        {
            this.ShuffleCharacters();
            int visibleCharactersCount = 0;
            int hiddenCharactersCount = 0;
            switch (this.Players.Count())
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
            // Si le roi est parmis les cartes visibles, on reméllange
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
