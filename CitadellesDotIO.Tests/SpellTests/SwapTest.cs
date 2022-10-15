using CitadellesDotIO.Extensions;
using CitadellesDotIO.Model;
using CitadellesDotIO.Model.Characters;
using CitadellesDotIO.Model.Districts;
using CitadellesDotIO.Model.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CitadellesDotIO.Tests.SpellTests
{
    [TestClass]
    public class SwapTest
    {
        [TestMethod]
        public void SwapCast_TableDistrictDeck_IsSwapped()
        {
            // Arrange
            Mock<Player> player = new("WizardTestPlayer");
            Wizard caster = new(0);
            player.Object.PickCharacter(caster);
            Deck<District> deck = DeckFactory.TestDistrictDeck(50);
            player.Object.PickDistricts(deck.PickCards(Dice.Roll(25)).ToList());
            List<District> oldCastersDeck = new(caster.Player.DistrictsDeck);

            // Act
            caster.Spell.Cast(deck);

            // Assert
            Assert.AreEqual(oldCastersDeck.Count, caster.Player.DistrictsDeck.Count);
            Assert.AreNotEqual(oldCastersDeck, caster.Player.DistrictsDeck);
        }

        [TestMethod]
        public void SwapCast_PlayerDistrictDeck_IsSwapped()
        {
            // Arrange
            Mock<Player> player = new("WizardTestPlayer");
            Wizard caster = new(0);
            player.Object.PickCharacter(caster);

            Mock<Player> target = new("TargetTestPlayer");

            Deck<District> deck = DeckFactory.TestDistrictDeck(50);
            player.Object.PickDistricts(deck.PickCards(Dice.Roll(25)).ToList());
            target.Object.PickDistricts(deck.PickCards(Dice.Roll(25)).ToList());

            // Act
            List<District> oldCastersDeck = new(caster.Player.DistrictsDeck);
            List<District> oldTargetDeck = new(target.Object.DistrictsDeck);

            caster.Spell.Cast(target.Object);

            // Assert
            Assert.IsTrue(caster.Player.DistrictsDeck.SequenceEqual(oldTargetDeck, new DistrictComparer()));
            Assert.IsTrue(target.Object.DistrictsDeck.SequenceEqual(oldCastersDeck, new DistrictComparer()));
        }        
    }
}
