using CitadellesDotIO.Model;
using CitadellesDotIO.Model.Characters;
using CitadellesDotIO.Model.Districts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CitadellesDotIO.Tests
{
    [TestClass]
    public class DistrictsTest
    {
        private const int InitialGold = 99;
        private static Mock<Player> GetPlayerWithCharacterAndBuiltDistrict(Type characterType, Type districtType)
        {
            Mock<Player> player = new();
            Character character = Activator.CreateInstance(characterType) as Character;
            player.Object.PickCharacter(character);
            District toBuild = Activator.CreateInstance(districtType) as District;
            player.Object.PickDistrict(toBuild);
            player.Object.Gold = InitialGold;
            player.Object.BuildDistrict(player.Object.BuildableDistricts.First());
            return player;
        }

        [TestMethod]
        [DataRow(typeof(King))]
        [DataRow(typeof(Bishop))]
        [DataRow(typeof(Merchant))]
        [DataRow(typeof(Condottiere))]
        public void MagicAcademy_ShouldYield_BonusIcome(Type characterType)
        {
            // Arrange
            Mock<Player> player = GetPlayerWithCharacterAndBuiltDistrict(characterType, typeof(MagicAcademy));

            // Act
            player.Object.Character.PercieveBonusIncome();

            // Assert
            int expectedGold = InitialGold - player.Object.BuiltDistricts.First().BuildingCost + 1;
            Assert.AreEqual(expectedGold, player.Object.Gold);
        }

        [TestMethod]
        [DataRow(typeof(Architect))]
        [DataRow(typeof(Assassin))]
        [DataRow(typeof(Thief))]
        [DataRow(typeof(Wizard))]
        public void MagicAcademy_ShouldNotYield_BonusIcome(Type characterType)
        {
            // Arrange
            Mock<Player> player = GetPlayerWithCharacterAndBuiltDistrict(characterType, typeof(MagicAcademy));

            // Act
            player.Object.Character.PercieveBonusIncome();

            // Assert
            int expectedGold = InitialGold - player.Object.BuiltDistricts.First().BuildingCost;
            Assert.AreEqual(expectedGold, player.Object.Gold);
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
        public void Dungeon_NotInTargets_DemolishCast(Type characterType)
        {
            // Arrange
            Mock<Player> target = GetPlayerWithCharacterAndBuiltDistrict(characterType, typeof(Dungeon));
            Mock<Player> caster = new();
            caster.Object.PickCharacter(new Condottiere());
            caster.Object.Gold = InitialGold;

            // Act
            caster.Object.Character.Spell.GetAvailableTargets(new List<ITarget>(){
                target.Object.BuiltDistricts.First()
            });

            // Assert
            Assert.IsFalse(caster.Object.Character.Spell.HasTargets);
        }
    }
}
