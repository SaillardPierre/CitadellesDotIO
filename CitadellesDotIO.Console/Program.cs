using CitadellesDotIO.Engine;
using CitadellesDotIO.Engine.Factory;
using CitadellesDotIO.Engine.View;
using CitadellesDotIO.Server.Client;
using CitadellesDotIO.Server.Client.CustomEventArgs;
using CitadellesDotIO.Server.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace CitadellesDotIO
{
    public static class Program
    {
        static async Task Main(string[] args)
        {
            await Task.Delay(2000);
            List<string> playerNames = new List<string>() { "Pierre", "Thomas", "Ryan", "Maze", "Vincent", "Danaé", "Amélie" };
            playerNames.GetRange(0, 0).ForEach(p => Console.WriteLine(p));            
            List<Player> players = new List<Player>();
            List<LobbiesClient> clients = new();
            for (int i = 0; i < 5; i++)
            {
                Player player = new Player(playerNames[i], new ConsoleView());
                players.Add(player);
                LobbiesClient lobbiesClient = new LobbiesClient(player, "https://localhost:7257", () => { }, StateChanged);
                clients.Add(lobbiesClient);
                await lobbiesClient.StartAsync();
                await lobbiesClient.GetLobbiesAsync();
            }

            clients.ForEach(async c =>
            {
                Lobby test = c.Lobbies.Last();
                await c.JoinLobbyAsync(test.Id);
            });
            
            Game gc = GameFactory.VanillaGame(players);
            if (await gc.Run())
            {
                Console.WriteLine("La partie est terminée");
                int rank = 1;
                gc.GetRanking().ToList().ForEach(p =>
                {
                    Console.WriteLine($"{rank} : {p.Name} with {p.Score} points and {p.City.Count} districts");
                    p.City.ToList().ForEach(d =>
                    {
                        Console.WriteLine($"\t {d.Name} {d.ScoreValue}");
                    });
                    rank++;
                });
            }
            Console.ReadKey();
        }

        public static void StateChanged(object sender, StateChangedEventArgs e)
        {
            Console.WriteLine("State changed");
        }
    }
}
