using CitadellesDotIO.Controllers;
using CitadellesDotIO.Factories;
using CitadellesDotIO.Model;
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
            Game gc = GameFactory.VanillaGame(players);
            if (gc.Run())
            {
                Console.WriteLine("La partie est terminée");
            }
            Console.ReadKey();
        }        
    }
}
