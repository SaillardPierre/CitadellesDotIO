using CitadellesDotIO.Model;
using CitadellesDotIO.Model.Districts;
using CitadellesDotIO.Model.Factories;
using CitadellesDotIO.Model.Spells;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PrivateObjectExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitadellesDotIO.Tests.SpellTests
{
    [TestClass]
    public class ColorShiftTest
    {
        private Player Player;
        private CourtOfMiracles ComUnderTest;
        private void SetPlayerWithCourtOfMiracles()
        {
            this.Player = new();
            this.Player.Gold = 99;
            this.Player.DistrictThreshold = 2;
            this.ComUnderTest = new();
            this.Player.PickDistrict(this.ComUnderTest);
            this.Player.BuildDistrict(this.ComUnderTest);
        }

        [TestMethod]
        public void ColorShiftTargets_AtDistrictThreshold_ShouldBeFourHollows_InList()
        {
            // Arrange
            SetPlayerWithCourtOfMiracles();
            TestDistrict district = new TestDistrict();
            this.Player.PickDistrict(district);
            this.Player.BuildDistrict(district);            

            // Act
            this.ComUnderTest.Spell.GetAvailableTargets();

            // Assert
            Assert.IsTrue(this.ComUnderTest.Spell.HasTargets);
            Assert.AreEqual(4, this.ComUnderTest.Spell.Targets.Count);
        }

        [TestMethod]
        public void ColorShiftTargets_BeforeDistrictThreshold_ShouldBeEmpty()
        {
            // Arrange
            SetPlayerWithCourtOfMiracles();

            // Act
            this.ComUnderTest.Spell.GetAvailableTargets();

            // Assert
            Assert.IsFalse(this.ComUnderTest.Spell.HasTargets);
        }
    }
}
