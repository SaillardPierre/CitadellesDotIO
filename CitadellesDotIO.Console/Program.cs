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
using System.Threading.Tasks;

namespace CitadellesDotIO
{
    public static class Program
    {
        static async Task Main(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();

            string siteUrl = configuration.GetValue<string>("Connectivity:SiteUrl");

            List<string> playerNames = new List<string>() { "Pierre", "Thomas", "Ryan", "Maze", "Vincent", "Danaé", "Amélie" };

            PlayerClient playerClient = new(siteUrl, playerNames.First());
            await playerClient.StartLobbyConnection();
            await playerClient.CreateGameAsync("PartieAJoindre");
            Console.ReadKey();
            PlayerClient player3Client = new(siteUrl, "UnAutre");
            await player3Client.StartLobbyConnection();
            PlayerClient player2Client = new(siteUrl, "Danaé");
            await player2Client.StartLobbyConnection();
            string gameId = Console.ReadLine();
            await player2Client.JoinGameAsync(gameId);
            
            Console.ReadKey();
        }        
    }
}
