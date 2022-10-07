using CitadellesDotIO.Controllers;
using CitadellesDotIO.Model;
using CitadellesDotIO.Model.Characters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace CitadellesDotIO.Tests
{
    [TestClass]
    public class CharactersTests
    {
        [TestMethod]
        public void SpellTest()
        {
            Assassin assassin = new Assassin(1);
            Assassin assassin2 = new Assassin(2);
            Thief voleur = new Thief(3);

            assassin.Spell.Cast(assassin2);
            assassin.Spell.Cast(voleur);            

            Assert.IsFalse(assassin2.IsMurdered);
            Assert.IsTrue(voleur.IsMurdered);
        }


        [TestMethod]
        public void Character_IsMurdered_After_Being_Killed_By_Assassin()
        {
            Mock<Assassin> assassin = new Mock<Assassin>(1);
            Mock<Character> target = new Mock<Character>();

            ITarget targetObject = target.Object;

            //assassin.Object.GetSpell().Cast(ref targetObject);

            Assert.IsTrue(target.Object.IsMurdered);
        }

        public void Character_Gold_Is0_After_Being_Stolen_By_Thief()
        {
            Mock<GameController> gc = new Mock<GameController>();
            Mock<Thief> thief = new Mock<Thief>();
            Mock<Character> target = new Mock<Character>();
        }
    }
}
