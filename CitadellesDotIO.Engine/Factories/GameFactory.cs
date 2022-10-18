using CitadellesDotIO.Engine.Factories;
using CitadellesDotIO.Engine.View;
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
                view == null ? new RandomActionView() : view);
        }
    }
}
