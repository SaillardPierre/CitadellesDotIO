using CitadellesDotIO.Model;
using CitadellesDotIO.Model.Characters;
using System;
using System.Collections.Generic;

namespace CitadellesDotIO.Controllers
{
    public class Game
    {
        public string GameState { get; set; }
        public List<Player> Players { get; set; }
        public List<Character> CharactersDeck { get; set; }
        public List<Character> CharactersBin { get; set; }
        public Queue<Object> DistrictsList { get; set; }
        public List<Object> DistricstBin { get; set; }

        public Game(List<Player> players, List<Character> charactersDeck)
        {
            Players = players;
            CharactersDeck = charactersDeck;
            CharactersBin = new List<Character>();
        }
    }
}
