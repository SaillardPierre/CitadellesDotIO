using CitadellesDotIO.Model.Factories;
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
    }
}
