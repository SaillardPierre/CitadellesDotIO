using CitadellesDotIO.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using CitadellesDotIO.Model;
using Moq;
using CitadellesDotIO.View;
using System;
using CitadellesDotIO.Config;

namespace CitadellesDotIO.Tests
{
    [TestClass]
    public class GameControllerTest
    {
        GameController gameControllerUnderTest;

        [TestMethod]
        public void GeneralTest()
        {
            this.gameControllerUnderTest = this.GetGameControllerForPlayerNumber(5);
            this.gameControllerUnderTest.Run();
            Assert.IsTrue(this.gameControllerUnderTest.Players.Any(p => p.BuiltDistricts.Count == 8));
        }



        [TestMethod]
        [DataRow(4)]
        [DataRow(5)]
        [DataRow(6)]
        [DataRow(7)]
        public void PlayerCount_ShouldEqual_HasPickedPlayerCount_AfterPickCharactersMethod_ForXPlayers(int xPlayers)
        {
            this.gameControllerUnderTest = this.GetGameControllerForPlayerNumber(xPlayers);
            this.gameControllerUnderTest.Run();

            Assert.AreEqual(true,
                this.gameControllerUnderTest.Players.Count(p => p.HasPickedCharacter) == this.gameControllerUnderTest.Players.Count);
        }


        [TestMethod]
        [DataRow(2,1,4)]
        [DataRow(1,1,5)]
        [DataRow(0,1,6)]
        [DataRow(0,1,7)]
        public void CharacterBin_ShouldHaveXShownYHidden_ForZPlayers(int xVisible, int yHidden, int zPlayers)
        {
            this.gameControllerUnderTest = this.GetGameControllerForPlayerNumber(zPlayers);
            Assert.AreEqual(true,
                this.gameControllerUnderTest.CharactersBin.Count(c => c.IsVisible) == xVisible &&
                this.gameControllerUnderTest.CharactersBin.Count(c => !c.IsVisible) == yHidden);
        }

        [TestMethod]
        public void DistrictList_ShouldHave50TestDistrict_AfterStartingNewVanillaGame()
        {
            this.gameControllerUnderTest = this.GetGameControllerForPlayerNumber(4);
            Assert.IsTrue(gameControllerUnderTest.DistrictsDeck.Count == 50);
        }

        public GameController GetGameControllerForPlayerNumber(int number)
        {
            List<string> playerNames = new List<string>() { "Pierre", "Thomas", "Ryan", "Maze", "Vincent", "Danaé", "Amélie" };
            List<Player> players = new List<Player>();
            for (int i = 0; i < number; i++)
            {
                players.Add(new Player(playerNames[i]));
            }
            return new GameController(
                players,
                CharactersLists.VanillaCharactersList,
                DistrictLists.TestDistrictList(),
                new RandomActionView());                       
        }
    }
}
