using CitadellesDotIO.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using CitadellesDotIO.Model;
using Moq;
using CitadellesDotIO.View;

namespace CitadellesDotIO.Tests
{
    [TestClass]
    public class GameControllerTest
    {
        GameController gameControllerUnderTest;

        [TestMethod]
        public void ConsoleViewTest()
        {
            this.gameControllerUnderTest = this.GetGameControllerForPlayerNumber(4);
            this.gameControllerUnderTest.View = new View.ConsoleView();
            this.gameControllerUnderTest.Run();
        }


        [TestMethod]
        [DataRow(2,1,4)]
        [DataRow(1,1,5)]
        [DataRow(0,1,6)]
        [DataRow(0,1,7)]
        public void CharacterBin_ShouldHaveXShownYHidden_ForZPlayers(int xVisible, int yHidden, int zPlayers)
        {
            this.gameControllerUnderTest = this.GetGameControllerForPlayerNumber(zPlayers);
            this.gameControllerUnderTest.PrepareCharactersDistribution();
            Assert.AreEqual(true,
                this.gameControllerUnderTest.CharactersBin.Where(c => c.IsVisible).Count() == xVisible &&
                this.gameControllerUnderTest.CharactersBin.Where(c => !c.IsVisible).Count() == yHidden);
        }

        public GameController GetGameControllerForPlayerNumber(int number)
        {
            List<string> playerNames = new List<string>() { "Pierre", "Thomas", "Ryan", "Maze", "Vincent", "Danaé", "Amélie" };
            GameController gc = new GameController();
            List<Player> players = new List<Player>();
            for (int i=0; i<number; i++)
            {
                players.Add(new Player() { Name = playerNames[i] });
            }            
            gc.StartNewVanillaGame(players, new Mock<IView>().Object);
            return gc;
        }
    }
}
