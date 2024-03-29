﻿using CitadellesDotIO.Engine;
using CitadellesDotIO.Engine.Characters;
using CitadellesDotIO.Engine.Districts;
using CitadellesDotIO.Tests.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;

namespace CitadellesDotIO.Tests.DistrictTests
{
    [TestClass]
    public class MagicAcademyTest
    {
        [TestMethod]
        [DataRow(typeof(King))]
        [DataRow(typeof(Bishop))]
        [DataRow(typeof(Merchant))]
        [DataRow(typeof(Condottiere))]
        public void MagicAcademy_ShouldYield_BonusIcome(Type characterType)
        {
            // Arrange
            Mock<Player> player = PlayerMockFactory.WithCharacterAndBuiltDistrict(characterType, typeof(MagicAcademy));

            // Act
            player.Object.Character.PercieveBonusIncome();

            // Assert
            Assert.AreEqual(PlayerMockFactory.InitialGold + 1, player.Object.Gold);
        }

        [TestMethod]
        [DataRow(typeof(Architect))]
        [DataRow(typeof(Assassin))]
        [DataRow(typeof(Thief))]
        [DataRow(typeof(Wizard))]
        public void MagicAcademy_ShouldNotYield_BonusIcome(Type characterType)
        {
            // Arrange
            Mock<Player> player = PlayerMockFactory.WithCharacterAndBuiltDistrict(characterType, typeof(MagicAcademy));

            // Act
            player.Object.Character.PercieveBonusIncome();

            // Assert
            Assert.AreEqual(PlayerMockFactory.InitialGold, player.Object.Gold);
        }


    }
}
