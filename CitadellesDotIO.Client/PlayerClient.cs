using CitadellesDotIO.Client.CustomEventArgs;
using CitadellesDotIO.Engine;
using CitadellesDotIO.Engine.DTOs;
using CitadellesDotIO.Engine.View;
using CitadellesDotIO.Enums;
using Microsoft.AspNet.SignalR.Client;
using System.Text;

namespace CitadellesDotIO.Client
{
    public class PlayerClient
    {
        public LobbyState LobbyState { get; set; }

        private static string siteUrl = "https://localhost:7257";
        
        private string PlayerName { get; set; }
        private List<GameDto> Games { get; set; }
        private LobbyConnection LobbyConnection { get; set; }
        private GameConnection GameConnection { get; set; }
        public PlayerClient(string playerName)
        {
            this.PlayerName = playerName;
            this.Games = new();
            LobbyConnection = new(playerName, siteUrl, HubStateChanged, LobbyStateChanged);
            GameConnection = new(siteUrl, HubStateChanged, GameStateChanged);
        }

        public async Task StartLobbyConnection()
        {
            await this.LobbyConnection.StartAsync();
        }

        public async Task CreateGameAsync(string gameName)
        {
            await this.LobbyConnection.CreateGameAsync(gameName, this.PlayerName);
        }

        public async Task JoinGameAsync(string gameId)
        {
            await this.LobbyConnection.JoinGameAsync(gameId, this.PlayerName);
        }

        void HubStateChanged(object sender, HubConnectionStateChangedEventArgs e)
        {
            Console.WriteLine("[" + this.PlayerName + "] "+e.State + " | " + e.Message);
        }

        async void LobbyStateChanged(object sender, LobbyStateChangedEventArgs e)
        {
            Console.WriteLine("["+this.PlayerName+"] "+ e.State + " | " + e.Message);
            switch (e)
            {
                case GamesPulledEventArgs gamesPulledEvent:
                    this.Games = gamesPulledEvent.Games.ToList();
                    StringBuilder message = new();
                    foreach (GameDto g in this.Games)
                    {
                        message.AppendLine("Id : " + g.Id + " - Name : " + g.Name + " - Players : " + g.Players.Count);
                    }
                    Console.WriteLine(message.ToString());
                    break;
                case GameJoinedEventArgs gameJoinedEvent:
                    // Peut etre stocker la game ?
                    await GameConnection.StartAsync(gameJoinedEvent.GameId, this.PlayerName);
                    break;
                default:
                    break;
            }
        }

        void GameStateChanged(object sender, GameStateChangedEventArgs e)
        {
            Console.WriteLine("[" + this.PlayerName + "] " + e.State + " | " + e.Message);
        }
    }
}