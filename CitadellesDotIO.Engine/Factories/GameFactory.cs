using CitadellesDotIO.Engine.Factories;
using CitadellesDotIO.Engine.View;
using System.Collections.Generic;

namespace CitadellesDotIO.Engine.Factory
{
    public static class GameFactory
    {
        public static Game VanillaGame(List<Player> players, int districtDeckSize = 0)
        {
            return new Game(
                players,
                CharactersFactory.VanillaCharactersList,
                //DeckFactory.Laboratories().ToList(),
                DeckFactory.VanillaDistrictsDeck(),
                new RandomActionView());
        }
    }
}
