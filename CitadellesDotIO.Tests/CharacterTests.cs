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
        public void AssassinSpellTest()
        {
            Assassin assassin = new Assassin(1);
            Assassin assassin2 = new Assassin(2);
            Thief voleur = new Thief(3);

            assassin.Spell.Cast(assassin2);
            assassin.Spell.Cast(voleur);
            
            Assert.IsTrue(voleur.IsMurdered);
            Assert.IsFalse(assassin2.IsMurdered);
        }       
    }
}
