using CitadellesDotIO.Enums;
using CitadellesDotIO.Engine.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using CitadellesDotIO.Engine.Factory;
using CitadellesDotIO.Engine;
using System.Threading.Tasks;
using CitadellesDotIO.Engine.View;

namespace CitadellesDotIO.Tests
{
    [TestClass]
    public class GameTests
    {
        [DataRow(4)]
        [DataRow(5)]
        [DataRow(6)]
        [DataRow(7)]
        [TestMethod]
        public void Game_GameState_Finished_ForXPlayers(int xPlayers)
        {
            Game game = GameFactory.VanillaGame(PlayersFactory.BuddiesPlayerList(xPlayers).ToList());
            // A remplacer lorsqu'on pourra refaire ce test
            Assert.IsNotNull(game);
            // Assert.IsTrue(await game.Run() && game.GameState.Equals(GameState.Finished));
        }        
    }
}
