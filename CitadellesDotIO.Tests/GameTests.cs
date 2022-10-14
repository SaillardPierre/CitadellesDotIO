using CitadellesDotIO.Controllers;
using CitadellesDotIO.Enums;
using CitadellesDotIO.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Assert.IsTrue(
                game.Run() && game.GameState.Equals(GameState.Finished));
        }
    }
}
