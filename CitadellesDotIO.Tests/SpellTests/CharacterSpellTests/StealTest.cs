using CitadellesDotIO.Engine.Targets;
using CitadellesDotIO.Engine.Characters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace CitadellesDotIO.Tests.SpellTests.CharacterSpellTests
{
    [TestClass]
    public class StealTest
    {
        [TestMethod]
        public void StealCast_Character_IsStolen()
        {
            // Arrange
            Thief caster = new(0);
            Mock<Character> stealable = new();

            // Act
            caster.Spell.Cast(stealable.Object);

            // Assert
            Assert.IsTrue(stealable.Object.IsStolen);
        }

        [TestMethod]
        public void StealCast_Thief_IsNotStolen()
        {
            // Arrange
            Thief caster = new(0);
            Thief unstealable = new(1);

            // Act
            caster.Spell.Cast(unstealable);

            // Assert
            Assert.IsFalse(unstealable.IsStolen);
        }

        [TestMethod]
        public void StealTargets_Thief_NotInList()
        {
            // Arrange
            Thief caster = new(0);
            Thief unstealable = new(1);

            // Act
            caster.Spell.GetAvailableTargets(new List<ITarget>()
            {
                unstealable
            });

            // Assert
            Assert.IsFalse(caster.Spell.HasTargets);
        }

        [TestMethod]
        public void StealTargets_MurderedCharacter_NotInList()
        {
            // Arrange
            Assassin assassin = new(0);
            Thief caster = new(1);
            Mock<Character> unstealable = new();

            // Act
            assassin.Spell.Cast(unstealable.Object);
            caster.Spell.GetAvailableTargets(new List<ITarget>()
            {
                unstealable.Object
            });

            // Assert
            Assert.IsFalse(caster.Spell.HasTargets);
        }
    }
}
