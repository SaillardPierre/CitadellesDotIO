using CitadellesDotIO.Engine.Factories;
using CitadellesDotIO.Engine.View;
using System;
using System.Collections.Generic;

namespace CitadellesDotIO.Engine.Factory
{
    public static class GameFactory
    {
        public static Game VanillaGame(List<Player> players, IView view = null)
        {
            return new Game(
                players,
                CharactersFactory.VanillaCharactersList,
                DeckFactory.VanillaDistrictsDeck(),
                Guid.NewGuid().ToString("n"));
        }
    }
}
