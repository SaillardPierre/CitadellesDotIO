using CitadellesDotIO.Model.Characters;
using System;
using System.Collections.Generic;
using System.Text;

namespace CitadellesDotIO.Config
{
    public static class CharactersLists
    {
        public static List<Character> VanillaCharactersList => new List<Character>
        {
            new Assassin(0),
            new Thief(1),
            new Wizard(2),
            new King(3),
            new Bishop(4),
            new Merchant(5),
            new Architect(6),
            new Condottiere(7)
        };        
    }
}
