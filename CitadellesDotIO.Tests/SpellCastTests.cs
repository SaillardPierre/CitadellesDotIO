using CitadellesDotIO.Extensions;
using CitadellesDotIO.Factories;
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
    public class SpellCastTests
    {
        [TestMethod]
        public void MurderCast_CharacterIsMurdered_AssassinIsNotMurdered()
        {
            // Arrange
            Assassin caster = new(0);
            Mock<Assassin> unmurderable = new();
            Mock<Character> murderable = new();

            // Act
            caster.Spell.Cast(unmurderable.Object);
            caster.Spell.Cast(murderable.Object);

            // Assert
            Assert.IsFalse(unmurderable.Object.IsMurdered);
            Assert.IsTrue(murderable.Object.IsMurdered);
        }

        [TestMethod]
        public void StealCast_CharacterIsStolen_ThiefIsNotStolen()
        {
            // Arrange
            Thief caster = new(0);
            Mock<Thief> unstealable = new();
            Mock<Character> stealable = new();

            // Act
            caster.Spell.Cast(unstealable.Object);
            caster.Spell.Cast(stealable.Object);

            // Assert
            Assert.IsFalse(unstealable.Object.IsStolen);
            Assert.IsTrue(stealable.Object.IsStolen);
        }

        [TestMethod]
        public void SwapCast_TableDistrictDeckIsSwapped()
        {
            // Arrange
            Mock<Player> player = new("WizardTestPlayer");
            Wizard caster = new(0);
            player.Object.PickCharacter(caster);
            Deck<District> deck = DeckFactory.TestDistrictDeck(50);
            player.Object.PickDistricts(deck.PickCards(Dice.Roll(25)).ToList());
            List<District> oldCastersDeck = new List<District>(caster.Player.DistrictsDeck);

            // Act
            caster.Spell.Cast(deck);

            // Assert
            Assert.AreEqual(oldCastersDeck.Count, caster.Player.DistrictsDeck.Count);
            Assert.AreNotEqual(oldCastersDeck, caster.Player.DistrictsDeck);
        }

        [TestMethod]
        public void SwapCast_PlayerDistrictDeckIsSwapped()
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
            List<District> oldCastersDeck = new List<District>(caster.Player.DistrictsDeck);
            List<District> oldTargetDeck = new List<District>(target.Object.DistrictsDeck);

            caster.Spell.Cast(target.Object);

            // Assert
            Assert.IsTrue(caster.Player.DistrictsDeck.SequenceEqual(oldTargetDeck, new DistrictComparer()));
            Assert.IsTrue(target.Object.DistrictsDeck.SequenceEqual(oldCastersDeck, new DistrictComparer()));
        }

        [TestMethod]
        public void DemolishCast_PlayerDistrictIsDestroyed()
        {
            // Arrange
            Mock<Player> player = new("CondotierreTestPlayer");
            Condottiere caster = new(0);
            player.Object.PickCharacter(caster);

            Mock<Player> target = new("TargetTestPlayer");
            player.Object.Gold = target.Object.Gold = 99;

            target.Object.PickDistrict(new TestDistrict());

            target.Object.BuildDistrict(target.Object.BuildableDistricts.First());

            caster.Spell.Cast(target.Object.City.First());

            Assert.IsFalse(target.Object.City.Any());
        }

        [TestMethod]
        public void DemolishCast_BishopDistrictsNotInTargets()
        {
            Mock<Player> player = new("CondotierreTestPlayer");
            player.Object.Gold = 99;
            Condottiere caster = new(0);
            player.Object.PickCharacter(caster);

            Mock<Player> target = new("TargetTestPlayer");
            target.Object.Gold = 99;
            Bishop bishop = new(1);
            target.Object.PickCharacter(bishop);
            Deck<District> deck = DeckFactory.TestDistrictDeck(50);
            target.Object.PickDistrict(deck.PickCard());
            target.Object.BuildDistrict(target.Object.BuildableDistricts.First());

            List<ITarget> targets = new();
            target.Object.City.ToList().ForEach(t => targets.Add(t));

            caster.Spell.GetAvailableTargets(targets);

            Assert.IsFalse(caster.Spell.HasTargets);
        }
    }
}
