using CitadellesDotIO.Engine.Districts;
using CitadellesDotIO.Engine.Factories;
using CitadellesDotIO.Tests.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitadellesDotIO.Engine.Characters;
using CitadellesDotIO.Engine;
using CitadellesDotIO.Engine.Spells;

namespace CitadellesDotIO.Tests.SpellTests.CharacterSpellTests
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
