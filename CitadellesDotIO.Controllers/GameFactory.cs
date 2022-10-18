using CitadellesDotIO.Engine;
using CitadellesDotIO.Engine.Factories;
using CitadellesDotIO.View;
using System.Collections.Generic;

namespace CitadellesDotIO.Controllers.Factory
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
