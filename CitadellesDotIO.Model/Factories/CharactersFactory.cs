using CitadellesDotIO.Model.Characters;
using System.Collections.Generic;

namespace CitadellesDotIO.Model.Factories
{
    public static class CharactersFactory
    {
        public static List<Character> VanillaCharactersList => new()
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
