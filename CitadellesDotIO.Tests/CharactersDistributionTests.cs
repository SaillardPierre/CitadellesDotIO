using CitadellesDotIO.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using PrivateObjectExtension;
using CitadellesDotIO.Controllers.Factory;
using CitadellesDotIO.Model.Factories;

namespace CitadellesDotIO.Tests
{
    [TestClass]
    public class CharactersDistributionTests
    {
        private Game GameUnderTest;
        private PrivateObject PrivateGameUnderTest;

        private void SetTestObjects(int playersCount)
        {
            // Arrange
            this.GameUnderTest = GameFactory.VanillaGame(PlayersFactory.BuddiesPlayerList(playersCount).ToList());
            this.PrivateGameUnderTest = new PrivateObject(this.GameUnderTest);
        }

        [TestMethod]
        [DataRow(1, 4)]
        [DataRow(1, 5)]
        [DataRow(1, 6)]
        [DataRow(0, 7)]
        public void LeftoversCharactersCount_ShouldEqual_X_ForYPlayers(int xLeftoverCharacters, int yPlayers)
        {
            // Arrange
            this.SetTestObjects(yPlayers);

            // Act
            this.PrivateGameUnderTest.Invoke("ShuffleCharacters");
            this.PrivateGameUnderTest.Invoke("PrepareCharactersDistribution");
            this.PrivateGameUnderTest.Invoke("PickCharacters");

            // Assert
            Assert.IsTrue(
                this.GameUnderTest.CharactersDeck.Count == xLeftoverCharacters);
        }

        [TestMethod]
        [DataRow(4)]
        [DataRow(5)]
        [DataRow(6)]
        [DataRow(7)]
        public void PlayerCount_ShouldEqual_HasPickedPlayerCount_AfterPickCharactersMethod_ForXPlayers(int xPlayers)
        {
            // Arrange
            this.SetTestObjects(xPlayers);

            // Act
            this.PrivateGameUnderTest.Invoke("ShuffleCharacters");
            this.PrivateGameUnderTest.Invoke("PrepareCharactersDistribution");
            this.PrivateGameUnderTest.Invoke("PickCharacters");

            // Assert
            Assert.IsTrue(
                this.GameUnderTest.Players.Count(p => p.HasPickedCharacter) == this.GameUnderTest.Players.Count);
        }

        [TestMethod]
        [DataRow(2, 1, 4)]
        [DataRow(1, 1, 5)]
        [DataRow(0, 1, 6)]
        [DataRow(0, 1, 7)]
        public void CharacterBin_ShouldHaveXShownYHidden_ForZPlayers(int xVisible, int yHidden, int zPlayers)
        {
            // Arrange
            this.SetTestObjects(zPlayers);

            // Act
            this.PrivateGameUnderTest.Invoke("ShuffleCharacters");
            this.PrivateGameUnderTest.Invoke("PrepareCharactersDistribution");

            // Assert
            Assert.IsTrue(
                this.GameUnderTest.CharactersBin.Count(c => c.IsVisible) == xVisible &&
                this.GameUnderTest.CharactersBin.Count(c => !c.IsVisible) == yHidden);
        }
    }
}
