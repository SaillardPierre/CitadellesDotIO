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
        private Game Game;

        public void StartNewVanillaGame(List<Player> players)
        {
            // Ajout et mélange des joueurs à la partie
            this.Game = new Game(players, CharactersLists.VanillaCharactersList);
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
            this.Game.CharactersDeck.OrderBy(_ => new Random().Next()).ToList();
        }
    }
}
