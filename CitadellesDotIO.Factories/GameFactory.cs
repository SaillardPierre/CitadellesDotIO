using CitadellesDotIO.Config;
using CitadellesDotIO.Controllers;
using CitadellesDotIO.Model;
using CitadellesDotIO.View;

namespace CitadellesDotIO.Factories
{
    public static class GameFactory
    {
        public static Game VanillaGame(List<Player> players)
        {
            return new Game(
                players,
                CharactersLists.VanillaCharactersList,
                DistrictLists.TestDistrictList(),
                new RandomActionView());
        }
    }
}
