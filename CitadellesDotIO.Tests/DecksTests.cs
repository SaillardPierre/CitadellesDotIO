using CitadellesDotIO.Engine.Factories;
using CitadellesDotIO.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace CitadellesDotIO.Tests
{
    [TestClass]
    public class DecksTests
    {
        [TestMethod]
        public void VanillaDistrictsDeckCount_ShouldEqual_65()
        {
            Assert.AreEqual(65, DeckFactory.VanillaDistrictsDeck().Count);
        }

        [TestMethod]

        public void TestDistricsListCount_ShouldEqualSeed()
        {
            int targetSize = Dice.Roll(100);

            Assert.AreEqual(targetSize, DistrictsFactory.TestDistrictList(targetSize).Count);
        }
    }
}
