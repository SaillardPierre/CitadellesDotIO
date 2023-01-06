using CitadellesDotIO.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.IO;
using Microsoft.AspNetCore.SignalR.Client;
using CitadellesDotIO.Enums;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using CitadellesDotIO.Server;
using CitadellesDotIO.Engine.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System;

namespace CitadellesDotIO.Tests
{
    [TestClass]
    public class PlayerClientTest
    {
        // Remplacer ca pour ne pas avoir a l'appeler en boucle
        private static string GetSiteUrl()
        {
            string json = File.ReadAllText("appsettings.json");
            dynamic config = JsonConvert.DeserializeObject(json);
            return config.Connectivity.SiteUrl;
        }

        [TestMethod] 
        public async Task ConnectToLobby()
        {
            using (var host = WebHost.CreateDefaultBuilder(Array.Empty<string>())
                 .UseStartup<Startup>().UseUrls(new[] { GetSiteUrl() }).Build())
            {
                host.Start();

                // Arrange
                PlayerClient playerClient = new(GetSiteUrl(), "Pierre");

                // Act
                await playerClient.StartLobbyConnection();
                await playerClient.CreateGameAsync("PartieAJoindre");

                // Assert
                Assert.AreEqual(HubConnectionState.Connected, playerClient.LobbyConnectionState);
                Assert.AreEqual(LobbyState.GameJoined, playerClient.LobbyState);

                await playerClient.Quit();
            }
        }
    }
}
