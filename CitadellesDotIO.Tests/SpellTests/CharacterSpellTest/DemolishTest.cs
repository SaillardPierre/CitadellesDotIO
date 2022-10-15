using CitadellesDotIO.Model;
using CitadellesDotIO.Model.Characters;
using CitadellesDotIO.Model.Districts;
using CitadellesDotIO.Model.Spells;
using CitadellesDotIO.Tests.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;

namespace CitadellesDotIO.Tests.SpellTests.CharacterSpellTest
{
    [TestClass]
    public class DemolishTest
    {
        private Demolish DemolishUnderTest = (Demolish)PlayerMockFactory.WithCharacter(typeof(Condottiere)).Object.Character.Spell;

        [TestMethod]
        public void DemolishCast_PlayerDistrict_IsDestroyed()
        {
            // Arrange
            Mock<Player> target = PlayerMockFactory.WithCharacterAndBuiltDistrict(typeof(Condottiere), typeof(TestDistrict));

            // Act            
            DemolishUnderTest.Cast(target.Object.BuiltDistricts.First());

            // Assert 
            Assert.IsFalse(target.Object.BuiltDistricts.Any());
        }

        [TestMethod]
        [DataRow(typeof(King))]
        [DataRow(typeof(Merchant))]
        [DataRow(typeof(Condottiere))]
        [DataRow(typeof(Architect))]
        [DataRow(typeof(Assassin))]
        [DataRow(typeof(Thief))]
        [DataRow(typeof(Wizard))]
        public void DemolishTargets_PlayerDistricts_InList(Type characterType)
        {
            // Arrange
            Mock<Player> target = PlayerMockFactory.WithCharacterAndBuiltDistrict(characterType, typeof(TestDistrict));

            // Act
            DemolishUnderTest.GetAvailableTargets(new(target.Object.BuiltDistricts));

            // Assert
            Assert.IsTrue(DemolishUnderTest.HasTargets);
            Assert.AreEqual(target.Object.BuiltDistricts.Count, DemolishUnderTest.Targets.Count);
        }


        [TestMethod]
        public void DemolishTargets_BishopDistricts_NotInList()
        {
            // Arrange
            Mock<Player> target = PlayerMockFactory.WithCharacterAndBuiltDistrict(typeof(Bishop), typeof(Cathedral));

            // Act
            DemolishUnderTest.GetAvailableTargets(new() { target.Object.BuiltDistricts.First() });

            // Assert
            Assert.IsFalse(DemolishUnderTest.HasTargets);
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
        public void DemolishTargets_Dungeon_NotInList(Type characterType)
        {
            // Arrange
            Mock<Player> target = PlayerMockFactory.WithCharacterAndBuiltDistrict(characterType, typeof(Dungeon));

            // Act
            DemolishUnderTest.GetAvailableTargets(new() { target.Object.BuiltDistricts.First() });

            // Assert
            Assert.IsFalse(DemolishUnderTest.HasTargets);
        }
    }
}
