using CitadellesDotIO.Controllers;
using CitadellesDotIO.Controllers.Factory;
using CitadellesDotIO.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CitadellesDotIO
{
    public static class Program
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
                int rank = 1;
                gc.GetRanking().ToList().ForEach(p =>
                {
                    Console.WriteLine($"{rank} : {p.Name} with {p.Score} points and {p.City.Count()} districts");
                    p.City.ToList().ForEach(d =>
                    {
                        Console.WriteLine($"\t {d.Name} {d.ScoreValue}");
                    });
                    rank++;
                });
            }
            Console.ReadKey();
        }        
    }
}
