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
            ColorShift ColorShiftUnderTest
                = PlayerMockFactory.WithCharacterAndBuiltDistrict(
                    characterType,
                    typeof(CourtOfMiracles),
                    PlayerMockFactory.InitialGold,
                    1).Object.DistrictSpellSources.First().Spell as ColorShift;

            // Act
            ColorShiftUnderTest.GetAvailableTargets();

            // Assert
            Assert.IsTrue(ColorShiftUnderTest.HasTargets);
            Assert.AreEqual(4, ColorShiftUnderTest.Targets.Count);
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
            ColorShift ColorShiftUnderTest
                = PlayerMockFactory.WithCharacterAndBuiltDistrict(
                    characterType,
                    typeof(CourtOfMiracles)).Object.DistrictSpellSources.First().Spell as ColorShift;

            // Act
            ColorShiftUnderTest.GetAvailableTargets();

            // Assert
            Assert.IsFalse(ColorShiftUnderTest.HasTargets);
        }
    }
}
