using CitadellesDotIO.Engine;
using CitadellesDotIO.Engine.Factory;
using CitadellesDotIO.Engine.View;
using CitadellesDotIO.Server.Client;
using CitadellesDotIO.Server.Client.CustomEventArgs;
using CitadellesDotIO.Server.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

            // Joueur courant 
            Player playingPlayer = new Player(playerNames[0], new ConsoleView());
            LobbiesClient playingLobbiesClient = new LobbiesClient(playingPlayer, "https://localhost:7257", () => { }, StateChanged);
            clients.Add(playingLobbiesClient);
            await playingLobbiesClient.StartAsync();
            await playingLobbiesClient.GetLobbiesAsync();
            await playingLobbiesClient.CreateLobbyAsync();

            string lobbyId = playingLobbiesClient.LobbyId;

            for (int i = 1; i < 5; i++)
            {
                Player player = new Player(playerNames[i], new RandomActionView());
                LobbiesClient lobbiesClient = new LobbiesClient(player, "https://localhost:7257", () => { }, StateChanged);
                clients.Add(lobbiesClient);
                await lobbiesClient.StartAsync();
                await lobbiesClient.GetLobbiesAsync();
            }

            foreach(LobbiesClient client in clients)
            {
                await client.JoinLobbyAsync(lobbyId);
            }

            await playingLobbiesClient.StartGameAsync();

            Console.ReadKey();
        }

        public static void StateChanged(object sender, StateChangedEventArgs e)
        {
            Console.WriteLine("State changed");
        }
    }
}
