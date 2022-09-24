using CitadellesDotIO.Config;
using CitadellesDotIO.Model;
using CitadellesDotIO.Model.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CitadellesDotIO.Controllers
{
    public class GameController
    {
        public Game Game;
        private bool ApplyKingShuffleRule;

        public void StartNewVanillaGame(List<Player> players)
        {
            // Ajout et mélange des joueurs à la partie
            this.Game = new Game(players, CharactersLists.VanillaCharactersList);
            this.ApplyKingShuffleRule = true;
            this.ShufflePlayers();            
        }

        private void ShufflePlayers()
        {
            this.Game.Players.OrderBy(_ => new Random().Next()).ToList();
        }

        private void ShuffleCharacters()
        {
            // Récupération de la défausse des personnages
            this.Game.CharactersDeck.AddRange(this.Game.CharactersBin);
            // Vidage de la défausse
            this.Game.CharactersBin.Clear();
            // Mélange aléatoire
            this.Game.CharactersDeck.OrderBy(_ => new Random().Next()).ToList();
        }

        public void DistributeCharacters()
        {
            this.ShuffleCharacters();
            int visibleCharactersCount = 0;
            int hiddenCharactersCount = 0;
            switch (this.Game.Players.Count())
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
            this.Game.CharactersBin.AddRange(this.Game.CharactersDeck.GetRange(0, visibleCharactersCount));
            this.Game.CharactersBin.ForEach(c => c.Flip());
            // Si le roi est parmis les cartes visibles, on reméllange
            // TODO : Voir comment faire pour notifier l'interface / Si on notifie l'interface
            if (this.ApplyKingShuffleRule && this.Game.CharactersBin.Any(c => c.Name == nameof(King))){
                this.DistributeCharacters();
            }
    
            // Ajout des cartes cachées à la défausse
            this.Game.CharactersBin.AddRange(this.Game.CharactersDeck.GetRange(visibleCharactersCount, hiddenCharactersCount));

            // Suppression des cartes écartées du deck
            this.Game.CharactersDeck.RemoveRange(0, visibleCharactersCount + hiddenCharactersCount);                    
        }
    }
}
