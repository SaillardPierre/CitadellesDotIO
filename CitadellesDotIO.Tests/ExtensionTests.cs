using CitadellesDotIO.Config;
using CitadellesDotIO.Controllers;
using CitadellesDotIO.Extensions;
using CitadellesDotIO.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CitadellesDotIO.Tests
{
    [TestClass]
    public class ExtensionTests
    {
        [TestMethod]
        public void PlayerMazeShouldBeFirstAndKeepOprder()
        {
            List<string> playerNames = new List<string>() { "Pierre", "Thomas", "Ryan", "Maze", "Vincent", "Danaé", "Amélie" };
            GameController gc = new GameController();
            List<Player> players = new List<Player>();
            for (int i = 0; i < playerNames.Count; i++)
            {
                players.Add(new Player() { Name = playerNames[i] });
            }

            Player maze = players.SingleOrDefault(p => p.Name == "Maze");

            players.SetFirstElement(maze);

            Assert.AreEqual(players.IndexOf(maze), 0);
        }
    }
}
