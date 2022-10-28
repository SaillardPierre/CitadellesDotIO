using CitadellesDotIO.Client;
using CitadellesDotIO.Client.CustomEventArgs;
using CitadellesDotIO.Engine;
using CitadellesDotIO.Engine.View;
using System;
using System.Collections.Generic;
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
            List<LobbiesConnection> clients = new();

            // Joueur courant 
            PlayerClient realPlayerClient = await PlayerClient.BuildPlayerClientAsync("Pierre", new ConsoleView());
            await realPlayerClient.LobbiesConnection.CreateLobbyAsync("Console Test Lobby");

            string lobbyId = realPlayerClient.LobbiesConnection.LobbyId;

            for (int i = 1; i < 5; i++)
            {
                PlayerClient otherPlayerClient = await PlayerClient.BuildPlayerClientAsync(playerNames[i], new RandomActionView());
                await otherPlayerClient.LobbiesConnection.JoinLobbyAsync(lobbyId);
            }

            await realPlayerClient.LobbiesConnection.StartGameAsync();

            Console.ReadKey();
        }
        static void StateChanged(object sender, HubConnectionStateChangedEventArgs e)
        {
            Console.Write(e.State);
        }
    }
}
