using CitadellesDotIO.Controllers;
using CitadellesDotIO.Extensions;
using CitadellesDotIO.Model;
using CitadellesDotIO.Model.Districts;
using CitadellesDotIO.Model.Factories;
using CitadellesDotIO.Model.Spells;
using CitadellesDotIO.Tests.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PrivateObjectExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitadellesDotIO.Tests.SpellTests.DistrictSpellTest
{
    [TestClass]
    public class DiscardTest
    {
        private Discard DiscardUnderTest = (Discard)PlayerMockFactory.WithBuiltDistrict(typeof(Laboratory)).Object.DistrictSpellSources.First().Spell;
        private Deck<District> TableDeck = DeckFactory.TestDistrictDeck(9);

        [TestMethod]
        public void DiscardTargets_ShouldHaveTableDeckAndPlayerDeck()
        {
            // Arrange
            DiscardUnderTest.Caster.PickDistricts(DistrictsFactory.TestDistrictList(6));

            // Act
            DiscardUnderTest.GetAvailableTargets(new()
            {
                TableDeck
            });

            // Assert
            Assert.AreEqual(
                DiscardUnderTest.Targets.Count,
                DiscardUnderTest.Caster.DistrictsDeck.Count);
            Assert.AreEqual(new PrivateObject(DiscardUnderTest).GetField("TableDeck"), TableDeck);
        }

        [TestMethod]
        public void DiscardCast_TableDeckShouldEnqueue_CasterGoldShouldIncrease()
        {
            // Arrange
            DiscardUnderTest.Caster.PickDistricts(DistrictsFactory.TestDistrictList(6));
            int tableDeckInitialCount = TableDeck.Count;
            int expectedGold = PlayerMockFactory.InitialGold + 2;

            // Act
            DiscardUnderTest.GetAvailableTargets(new()
            {
                TableDeck
            });
            DiscardUnderTest.Cast(DiscardUnderTest.Caster.DistrictsDeck[Dice.Roll(DiscardUnderTest.Caster.DistrictsDeck.Count)]);

            // Assert
            Assert.AreEqual(expectedGold, DiscardUnderTest.Caster.Gold);
            Assert.AreEqual(tableDeckInitialCount + 1, TableDeck.Count );
        }
    }
}
