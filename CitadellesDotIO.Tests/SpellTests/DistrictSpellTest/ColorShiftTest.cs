using CitadellesDotIO.Enums;
using CitadellesDotIO.Model.Characters;
using CitadellesDotIO.Model.Districts;
using CitadellesDotIO.Model.Spells;
using CitadellesDotIO.Tests.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace CitadellesDotIO.Tests.SpellTests.DistrictSpellTest
{
    [TestClass]
    public class ColorShiftTest
    {

        [TestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public void ColorShiftCast_AtDistrictThreshold_ShouldChangeDistrictType(int colorShiftIndex)
        {
            // Arrange
            ColorShift colorShiftUnderTest
                = PlayerMockFactory.WithBuiltDistrict(
                    typeof(CourtOfMiracles),
                    PlayerMockFactory.InitialGold,
                    1).Object.DistrictSpellSources.First().Spell as ColorShift;

            // Act
            colorShiftUnderTest.GetAvailableTargets();
            colorShiftUnderTest.Cast(colorShiftUnderTest.Targets[colorShiftIndex]);

            // Assert
            Assert.AreEqual((DistrictType)colorShiftIndex, colorShiftUnderTest.SpellSource.DistrictType);
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
        public void ColorShiftTargets_AtDistrictThreshold_ShouldBeFourHollows_InList(Type characterType)
        {
            // Arrange
            ColorShift colorShiftUnderTest
                = PlayerMockFactory.WithCharacterAndBuiltDistrict(
                    characterType,
                    typeof(CourtOfMiracles),
                    PlayerMockFactory.InitialGold,
                    1).Object.DistrictSpellSources.First().Spell as ColorShift;

            // Act
            colorShiftUnderTest.GetAvailableTargets();

            // Assert
            Assert.IsTrue(colorShiftUnderTest.HasTargets);
            Assert.AreEqual(4, colorShiftUnderTest.Targets.Count);
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
        public void ColorShiftTargets_BeforeDistrictThreshold_ShouldBeEmpty(Type characterType)
        {
            // Arrange
            ColorShift colorShiftUnderTest
                = PlayerMockFactory.WithCharacterAndBuiltDistrict(
                    characterType,
                    typeof(CourtOfMiracles)).Object.DistrictSpellSources.First().Spell as ColorShift;

            // Act
            colorShiftUnderTest.GetAvailableTargets();

            // Assert
            Assert.IsFalse(colorShiftUnderTest.HasTargets);
        }
    }
}
