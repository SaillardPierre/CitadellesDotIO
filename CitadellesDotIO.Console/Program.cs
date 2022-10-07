using CitadellesDotIO.Config;
using CitadellesDotIO.Controllers;
using CitadellesDotIO.Model;
using CitadellesDotIO.View;
using System;
using System.Collections.Generic;

namespace CitadellesDotIO
{
    public class Program
    {
        static void Main(string[] args)
        {            
            List<string> playerNames = new List<string>() { "Pierre", "Thomas", "Ryan", "Maze", "Vincent", "Danaé", "Amélie" };
            playerNames.GetRange(0, 0).ForEach(p => Console.WriteLine(p));            
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
            if (gc.Run())
            {
                Console.WriteLine("La partie est terminée");
            }
            Console.ReadKey();
        }        
    }
}
