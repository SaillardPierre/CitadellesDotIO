using CitadellesDotIO.Enums.TurnChoices;
using CitadellesDotIO.Engine.Characters;
using CitadellesDotIO.Engine.Districts;
using CitadellesDotIO.Engine.Factories;
using CitadellesDotIO.Engine.Passives;
using CitadellesDotIO.Tests.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace CitadellesDotIO.Tests.PassiveTests.CharacterPassiveTests
{
    [TestClass]
    public class IncreaseTurnBuildingCapTest
    {
        [TestMethod]
        public void IncreaseTurnBuildingCapApply_TurnBuildingCapShouldEqualThree()
        {
            // Arrange
            IncreaseTurnBuildingCap IncreaseTurnBuildingCapUnderTest =
                PlayerMockFactory.WithCharacter(typeof(Architect)).Object.Character.Passive as IncreaseTurnBuildingCap;

            // Act
            IncreaseTurnBuildingCapUnderTest.Apply();

            // Assert
            Assert.AreEqual(3, IncreaseTurnBuildingCapUnderTest.Player.TurnBuildingCap);
        }

        [TestMethod]
        public void IncreaseTurnBuildingCapApply_PlayerShouldBeAbleToBuildAgain()
        {
            // Arrange
            IncreaseTurnBuildingCap IncreaseTurnBuildingCapUnderTest =
                PlayerMockFactory.WithCharacter(typeof(Architect)).Object.Character.Passive as IncreaseTurnBuildingCap;

            // Act
            IncreaseTurnBuildingCapUnderTest.Apply();
            IncreaseTurnBuildingCapUnderTest.Player.PickDistricts(new() { new TestDistrict(), new TestDistrict() });
            IncreaseTurnBuildingCapUnderTest.Player.BuildDistrict(IncreaseTurnBuildingCapUnderTest.Player.BuildableDistricts.First());
            IncreaseTurnBuildingCapUnderTest.Player.TakenChoices.Add(UnorderedTurnChoice.BuildDistrict.ToString());


            // Assert
            Assert.IsTrue(IncreaseTurnBuildingCapUnderTest.Player.TakenChoices.Contains(UnorderedTurnChoice.BuildDistrict.ToString()));
            Assert.IsTrue(IncreaseTurnBuildingCapUnderTest.Player.AvailableChoices.Contains(UnorderedTurnChoice.BuildDistrict));
        }

        [TestMethod]
        public void IncreaseTurnBuildingCapApply_PlayerShouldNotBeAbleToBuildAgain()
        {
            // Arrange
            IncreaseTurnBuildingCap IncreaseTurnBuildingCapUnderTest =
                PlayerMockFactory.WithCharacter(typeof(Architect)).Object.Character.Passive as IncreaseTurnBuildingCap;

            // Act
            IncreaseTurnBuildingCapUnderTest.Apply();
            IncreaseTurnBuildingCapUnderTest.Player.PickDistricts(DistrictsFactory.TestDistrictList(4));
            for (int i = 0; i < 3; i++)
            {
                IncreaseTurnBuildingCapUnderTest.Player.BuildDistrict(IncreaseTurnBuildingCapUnderTest.Player.BuildableDistricts.First());
                IncreaseTurnBuildingCapUnderTest.Player.TakenChoices.Add(UnorderedTurnChoice.BuildDistrict.ToString());
            }

            // Assert
            Assert.AreEqual(3, IncreaseTurnBuildingCapUnderTest.Player.TakenChoices.Count(t => t.Equals(UnorderedTurnChoice.BuildDistrict.ToString())));
            Assert.IsFalse(IncreaseTurnBuildingCapUnderTest.Player.AvailableChoices.Contains(UnorderedTurnChoice.BuildDistrict));
        }
    }
}
