using CitadellesDotIO.Enums.TurnChoices;
using CitadellesDotIO.Model;
using CitadellesDotIO.Model.Characters;
using CitadellesDotIO.Model.Districts;
using CitadellesDotIO.Model.Factories;
using CitadellesDotIO.Model.Passives;
using CitadellesDotIO.Tests.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitadellesDotIO.Tests
{
    [TestClass]
    public class PassivesTest
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
            for (int i =0;i<3; i++)
            {
                IncreaseTurnBuildingCapUnderTest.Player.BuildDistrict(IncreaseTurnBuildingCapUnderTest.Player.BuildableDistricts.First());
                IncreaseTurnBuildingCapUnderTest.Player.TakenChoices.Add(UnorderedTurnChoice.BuildDistrict.ToString());
            }

            // Assert
            Assert.AreEqual(3, IncreaseTurnBuildingCapUnderTest.Player.TakenChoices.Count(t=>t.Equals(UnorderedTurnChoice.BuildDistrict.ToString())));
            Assert.IsFalse(IncreaseTurnBuildingCapUnderTest.Player.AvailableChoices.Contains(UnorderedTurnChoice.BuildDistrict));
        }



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
