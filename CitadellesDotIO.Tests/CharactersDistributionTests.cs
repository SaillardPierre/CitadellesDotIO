using CitadellesDotIO.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using PrivateObjectExtension;
using CitadellesDotIO.Controllers.Factory;
using CitadellesDotIO.Model.Factories;
using CitadellesDotIO.Tests.Factories;
using System;

namespace CitadellesDotIO.Tests
{
    [TestClass]
    public class CharactersDistributionTests
    {
        [TestMethod]
        [DataRow(1, 4)]
        [DataRow(1, 5)]
        [DataRow(1, 6)]
        [DataRow(0, 7)]
        public void LeftoversCharactersCount_ShouldEqual_X_ForYPlayers(int xLeftoverCharacters, int yPlayers)
        {
            // Arrange
            (Game game, PrivateObject privateGame) = PrivateGameFactory.GetPrivateGame(yPlayers);

            // Act
            privateGame.Invoke("ShuffleCharacters");
            privateGame.Invoke("PrepareCharactersDistribution");
            privateGame.Invoke("PickCharacters");

            // Assert
            Assert.AreEqual(game.CharactersDeck.Count, xLeftoverCharacters);
        }

        [TestMethod]
        [DataRow(4)]
        [DataRow(5)]
        [DataRow(6)]
        [DataRow(7)]
        public void PlayerCount_ShouldEqual_HasPickedPlayerCount_AfterPickCharactersMethod_ForXPlayers(int xPlayers)
        {
            // Arrange
            (Game game, PrivateObject privateGame) = PrivateGameFactory.GetPrivateGame(xPlayers);

            // Act
            privateGame.Invoke("ShuffleCharacters");
            privateGame.Invoke("PrepareCharactersDistribution");
            privateGame.Invoke("PickCharacters");

            // Assert
            Assert.AreEqual(game.Players.Count(p => p.HasPickedCharacter), game.Players.Count);
        }

        [TestMethod]
        [DataRow(2, 1, 4)]
        [DataRow(1, 1, 5)]
        [DataRow(0, 1, 6)]
        [DataRow(0, 1, 7)]
        public void CharacterBin_ShouldHaveXShownYHidden_ForZPlayers(int xVisible, int yHidden, int zPlayers)
        {
            // Arrange
            (Game game, PrivateObject privateGame) = PrivateGameFactory.GetPrivateGame(zPlayers);

            // Act
            privateGame.Invoke("ShuffleCharacters");
            privateGame.Invoke("PrepareCharactersDistribution");

            // Assert
            Assert.AreEqual(game.CharactersBin.Count(c => c.IsVisible), xVisible);
            Assert.AreEqual(game.CharactersBin.Count(c => !c.IsVisible), yHidden);
        }
    }
}
