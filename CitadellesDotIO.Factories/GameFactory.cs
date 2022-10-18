using CitadellesDotIO.Controllers;
using CitadellesDotIO.Engine;
using CitadellesDotIO.View;

namespace CitadellesDotIO.Factories
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
