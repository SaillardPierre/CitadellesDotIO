using CitadellesDotIO.Engine.Characters;
using CitadellesDotIO.Engine.Districts;
using CitadellesDotIO.Engine.Passives;
using CitadellesDotIO.Tests.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace CitadellesDotIO.Tests.PassiveTests.DistrictPassiveTests
{
    [TestClass]
    public class IncreasePickSizeTest
    {
        [TestMethod]
        [DataRow(typeof(King))]
        [DataRow(typeof(Bishop))]
        [DataRow(typeof(Merchant))]
        [DataRow(typeof(Condottiere))]
        [DataRow(typeof(Architect))]
        [DataRow(typeof(Assassin))]
        [DataRow(typeof(Thief))]
        [DataRow(typeof(Wizard))]
        public void IncreasePickSizeApply_PickSizeShouldEqualTwo(Type characterType)
        {
            // Arrange
            IncreasePickSize IncreasePickSizeUnderTest =
                PlayerMockFactory.WithCharacterAndBuiltDistrict(characterType, typeof(Library)).Object.DistrictPassiveSources.First().Passive as IncreasePickSize;

            // Act
            IncreasePickSizeUnderTest.Apply();

            // Assert
            Assert.AreEqual(2, IncreasePickSizeUnderTest.Player.PickSize);
        }
    }
}
