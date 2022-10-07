using CitadellesDotIO.Config;
using CitadellesDotIO.Controllers;
using CitadellesDotIO.Model;
using CitadellesDotIO.View;
using System;
using System.Collections.Generic;

namespace CitadellesDotIO.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                List<string> playerNames = new List<string>() { "Pierre", "Thomas", "Ryan", "Maze", "Vincent", "Danaé", "Amélie" };
                List<Player> players = new List<Player>();
                for (int i = 0; i < 5; i++)
                {
                    players.Add(new Player(playerNames[i]));
                }
                GameController gc = new GameController(
                    players,
                    CharactersLists.VanillaCharactersList,
                    DistrictLists.TestDistrictList(),
                    new RandomActionView());
                gc.Run();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }        
    }
}
