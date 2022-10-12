using CitadellesDotIO.Controllers;
using CitadellesDotIO.Model;
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
                DistrictsFactory.TestDistrictList(districtDeckSize > 0 ? districtDeckSize : 100),
                new RandomActionView());
        }
    }
}
