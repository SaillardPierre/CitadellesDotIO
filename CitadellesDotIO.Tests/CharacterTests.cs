using CitadellesDotIO.Controllers;
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
        public void Character_Cant_Play_After_Being_Killed_By_Assassin()
        {
            Mock<Assassin> assassin = new Mock<Assassin>();
            Mock<Character> target = new Mock<Character>();
            Console.WriteLine("Instantiated assassin and target");


            assassin.Object.KillCharacter(target.Object);
            Console.WriteLine("Assassin killed target");

            Assert.IsFalse(target.Object.CanPlay);
            Console.WriteLine("Target cant play : CanPlay = "+ target.Object.CanPlay.ToString());
        }

        public void Character_Gold_Is0_After_Being_Stolen_By_Thief()
        {
            Mock<GameController> gc = new Mock<GameController>();
            Mock<Thief> thief = new Mock<Thief>();
            Mock<Character> target = new Mock<Character>();
        }
    }
}
