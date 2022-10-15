using CitadellesDotIO.Model;
using CitadellesDotIO.Model.Characters;
using CitadellesDotIO.Model.Districts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace CitadellesDotIO.Tests.SpellTests
{
    [TestClass]
    public class DemolishTest
    {
        [TestMethod]
        public void DemolishCast_PlayerDistrict_IsDestroyed()
        {
            // Arrange
            Mock<Player> player = new("CondotierreTestPlayer");
            Condottiere caster = new(0);
            player.Object.PickCharacter(caster);

            Mock<Player> target = new("TargetTestPlayer");
            player.Object.Gold = target.Object.Gold = 99;

            target.Object.PickDistrict(new TestDistrict());

            target.Object.BuildDistrict(target.Object.BuildableDistricts.First());

            caster.Spell.Cast(target.Object.BuiltDistricts.First());

            Assert.IsFalse(target.Object.BuiltDistricts.Any());
        }

        [TestMethod]
        public void DemolishTargets_BishopDistricts_NotInList()
        {
            Mock<Player> player = new("CondotierreTestPlayer");
            player.Object.Gold = 99;
            Condottiere caster = new(0);
            player.Object.PickCharacter(caster);

            Mock<Player> target = new("TargetTestPlayer");
            target.Object.Gold = 99;
            Bishop bishop = new(1);
            target.Object.PickCharacter(bishop);
            target.Object.PickDistrict(new Church());
            target.Object.BuildDistrict(target.Object.BuildableDistricts.First());

            List<ITarget> targets = new();
            target.Object.BuiltDistricts.ToList().ForEach(t => targets.Add(t));

            caster.Spell.GetAvailableTargets(targets);

            Assert.IsFalse(caster.Spell.HasTargets);
        }
    }
}
