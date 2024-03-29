﻿using CitadellesDotIO.Engine;
using CitadellesDotIO.Engine.Spells;
using CitadellesDotIO.Engine.Characters;
using CitadellesDotIO.Engine.Districts;
using CitadellesDotIO.Engine.Factories;
using CitadellesDotIO.Tests.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitadellesDotIO.Tests.SpellTests.DistrictSpellTests
{
    [TestClass]
    public class CraftTest
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
        public void CraftCast_PlayerDistrictDeckShouldGrow_GoldCountShouldDecrease(Type characterType)
        {
            // Arrange
            Craft craftUnderTest =
                PlayerMockFactory.WithCharacterAndBuiltDistrict(
                    characterType,
                    typeof(Manufacture)).Object.DistrictSpellSources.First().Spell as Craft;
            Deck<District> tableDeck = DeckFactory.TestDistrictDeck(9);

            // Act
            craftUnderTest.GetAvailableTargets(new() { tableDeck });
            craftUnderTest.Cast();

            // Assert
            Assert.AreEqual(3, craftUnderTest.Caster.DistrictsDeck.Count);
            Assert.AreEqual(PlayerMockFactory.InitialGold - 3, craftUnderTest.Caster.Gold);
        }
    }
}
