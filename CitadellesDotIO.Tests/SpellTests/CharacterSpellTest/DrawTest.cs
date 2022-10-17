using CitadellesDotIO.Model.Districts;
using CitadellesDotIO.Model.Factories;
using CitadellesDotIO.Model.Spells;
using CitadellesDotIO.Model;
using CitadellesDotIO.Tests.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitadellesDotIO.Model.Characters;

namespace CitadellesDotIO.Tests.SpellTests.CharacterSpellTest
{
    [TestClass]
    public class DrawTest
    {
        [TestMethod]
        public void DrawCast_PlayerDistrictDeckShouldGrowBy2()
        {
            // Arrange
            Draw drawUnderTest =
                PlayerMockFactory.WithCharacter(typeof(Architect)).Object.Character.Spell as Draw;
            Deck<District> tableDeck = DeckFactory.TestDistrictDeck(9);

            // Act
            drawUnderTest.GetAvailableTargets(new() { tableDeck });
            drawUnderTest.Cast();

            // Assert
            Assert.AreEqual(2, drawUnderTest.Caster.DistrictsDeck.Count);           
        }
    }
}
