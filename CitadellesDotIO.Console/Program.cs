using CitadellesDotIO.Client;
using CitadellesDotIO.Client.CustomEventArgs;
using CitadellesDotIO.Engine;
using CitadellesDotIO.Engine.View;
using CitadellesDotIO.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CitadellesDotIO
{
    public static class Program
    {
        static async Task Main(string[] args)
        {          
            Thread.Sleep(5000);

            string siteUrl = SiteUrl;
            string gameName = "PartieAJoindre";
            List<string> playerNames = new List<string>() { "Pierre", "Danaé", "Maze", "Vincent", "Thomas", "Amélie", "Lilian", "Ryan", "Spectateur" };
            List<PlayerClient> playerClients = new List<PlayerClient>();
            foreach (string playerName in playerNames)
            {
                PlayerClient playerClient = new(siteUrl, playerName);
                playerClients.Add(playerClient);
            }

            foreach (PlayerClient playerClient in playerClients)
            {
                await playerClient.StartLobbyConnection();
            }

            playerClients.First().CreateGame(gameName);
            Thread.Sleep(5000);

            foreach (PlayerClient playerClient in playerClients.Skip(1).SkipLast(1))
            {
                playerClient.JoinGameByGameName(gameName);
            }

            string ins = string.Empty;
            while (ins != "quit")
            {
                ins = Console.ReadKey().ToString();
            }            
        }
        static string SiteUrl
        => new ConfigurationBuilder()
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .Build().GetValue<string>("Connectivity:SiteUrl");
    }
}
