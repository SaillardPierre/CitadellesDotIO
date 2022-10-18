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
    public class IncreasePoolSizeTest
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
        public void IncreasePoolSizeApply_PoolSizeShouldEqualThree(Type characterType)
        {
            // Arrange
            IncreasePoolSize IncreasePoolSizeUnderTest =
                PlayerMockFactory.WithCharacterAndBuiltDistrict(characterType, typeof(Observatory)).Object.DistrictPassiveSources.First().Passive as IncreasePoolSize;

            // Act
            IncreasePoolSizeUnderTest.Apply();

            // Assert
            Assert.AreEqual(3, IncreasePoolSizeUnderTest.Player.PoolSize);
        }
    }
}
