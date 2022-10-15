﻿using CitadellesDotIO.Model;
using CitadellesDotIO.Model.Characters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace CitadellesDotIO.Tests.SpellTests
{
    [TestClass]
    public class MurderTest
    {
        [TestMethod]
        public void MurderCast_Assassin_IsNotMurdered()
        {
            // Arrange
            Assassin caster = new(0);
            Mock<Assassin> unmurderable = new();

            // Act
            caster.Spell.Cast(unmurderable.Object);

            // Assert
            Assert.IsFalse(unmurderable.Object.IsMurdered);
        }

        [TestMethod]
        public void MurderCast_Character_IsMurdered()
        {
            // Arrange
            Assassin caster = new(0);
            Mock<Character> murderable = new();

            // Act
            caster.Spell.Cast(murderable.Object);

            // Assert
            Assert.IsTrue(murderable.Object.IsMurdered);
        }

        [TestMethod]
        public void MurderTargets_Assassin_NotInList()
        {
            // Arrange
            Assassin caster = new(0);
            Mock<Assassin> unmurderable = new();

            // Act
            caster.Spell.GetAvailableTargets(new List<ITarget>()
            {
                unmurderable.Object
            });

            // Assert
            Assert.IsFalse(caster.Spell.HasTargets);
        }
        [TestMethod]
        public void MurderTargets_IsMurdered_NotInList()
        {
            // Arrange
            Assassin caster = new(0);
            Mock<Character> murdered = new();
            murdered.Object.IsMurdered = true;

            // Act
            caster.Spell.GetAvailableTargets(new List<ITarget>()
            {
                murdered.Object
            });

            // Assert
            Assert.IsFalse(caster.Spell.HasTargets);
        }

    }
}
