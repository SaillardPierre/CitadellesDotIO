using CitadellesDotIO.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
