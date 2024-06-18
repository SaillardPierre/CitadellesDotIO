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

        private const string ExceptionMessage = "Cet environnement de test ne permets pas de tester la connectivité.";

        private static IWebHost BuildWebHost()
        => WebHost.CreateDefaultBuilder(Array.Empty<string>())
            .UseStartup<Startup>()
            .UseUrls(new[] { GetSiteUrl() })
            .Build();


        [TestMethod]
        public async Task ConnectToLobby()
        {
            try
            {
                using var host = BuildWebHost();
                host.Start();

                // Arrange
                PlayerClient playerClient = new(GetSiteUrl(), "Pierre");

                // Act
                await playerClient.StartLobbyConnection();
                System.Threading.Thread.Sleep(15); // On attends que les promesses soient tenues avant que le test ne se termine

                // Assert
                Assert.AreEqual(HubConnectionState.Connected, playerClient.LobbyConnectionState);
                Assert.AreEqual(LobbyState.GamesPulled, playerClient.LobbyState);

                await playerClient.Quit();
            }
            catch (System.Net.Http.HttpRequestException)
            {
                Assert.Inconclusive(ExceptionMessage);
            }
        }

        [TestMethod]
        public async Task CreateGame()
        {
            try
            {
                using var host = BuildWebHost();
                host.Start();

                // Arrange
                PlayerClient playerClient = new(GetSiteUrl(), "Pierre");

                // Act
                await playerClient.StartLobbyConnection();
                await playerClient.CreateGame("GameDeTest");
                System.Threading.Thread.Sleep(1500); // On attends que les promesses soient tenues avant que le test ne se termine

                // Assert
                Assert.AreEqual(HubConnectionState.Disconnected, playerClient.LobbyConnectionState);
                Assert.AreEqual(HubConnectionState.Connected, playerClient.GameConnectionState);
                Assert.AreEqual(LobbyState.GameJoined, playerClient.LobbyState);               

                await playerClient.Quit();
            }
            catch (System.Net.Http.HttpRequestException)
            {
                Assert.Inconclusive(ExceptionMessage);
            }
        }

        [TestMethod]
        public async Task ConnectToExistingGameAndSetReadyState()
        {
            try
            {
                using var host = BuildWebHost();
                host.Start();

                // Arrange
                PlayerClient playerOneClient = new(GetSiteUrl(), "Pierre");
                PlayerClient playerTwoClient = new(GetSiteUrl(), "Thomas");

                // Act
                await playerOneClient.StartLobbyConnection();
                await playerOneClient.CreateGame("PartieAJoindre");

                await playerTwoClient.StartLobbyConnection();
                System.Threading.Thread.Sleep(15); // On attends que les promesses soient tenues avant que le test ne se termine

                await playerTwoClient.JoinGameAsync(playerOneClient.GameId);
                System.Threading.Thread.Sleep(15);

                await playerOneClient.SetReadyState(true);
                await playerTwoClient.SetReadyState(true); 
                System.Threading.Thread.Sleep(15);

                // Assert
                Assert.AreEqual(HubConnectionState.Disconnected, playerOneClient.LobbyConnectionState);
                Assert.AreEqual(HubConnectionState.Disconnected, playerTwoClient.LobbyConnectionState);
                Assert.AreEqual(LobbyState.GameJoined, playerOneClient.LobbyState);
                Assert.AreEqual(LobbyState.GameJoined, playerTwoClient.LobbyState);
                Assert.IsTrue(playerOneClient.Game.Players[1].IsReady);
                Assert.IsTrue(playerOneClient.Game.Players[0].IsReady);

                await playerOneClient.Quit();
                await playerTwoClient.Quit();
            }
            catch (System.Net.Http.HttpRequestException)
            {
                Assert.Inconclusive(ExceptionMessage);
            }
        }
    }
}
