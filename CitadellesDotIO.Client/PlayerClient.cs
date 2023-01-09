using CitadellesDotIO.Client.CustomEventArgs;
using CitadellesDotIO.Client;
using CitadellesDotIO.Engine;
using CitadellesDotIO.Engine.DTOs;
using CitadellesDotIO.Engine.View;
using CitadellesDotIO.Enums;
using Microsoft.AspNetCore.SignalR.Client;
using System.Text;

namespace CitadellesDotIO.Client
{
    public class PlayerClient
    {
        public LobbyState LobbyState { get; private set; }
        public HubConnectionState LobbyConnectionState { get; private set; }
        public HubConnectionState GameConnectionState { get; private set; }
        private string PlayerName { get; set; }
        private List<GameDto> Games { get; set; }
        private LobbyConnection LobbyConnection { get; set; }
        private GameConnection GameConnection { get; set; }
        public PlayerClient(string siteUrl, string playerName)
        {
            this.PlayerName = playerName;
            this.Games = new();
            LobbyConnection = new(playerName, siteUrl, HubStateChanged, LobbyStateChanged);
            GameConnection = new(playerName, siteUrl, HubStateChanged, GameStateChanged);
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

        public async Task JoinGameByGameNameAsync(string gameName)
        {
            await this.LobbyConnection.JoinGameByNameAsync(gameName, this.PlayerName);
        }

        void HubStateChanged(object sender, HubConnectionStateChangedEventArgs e)
        {
            StringBuilder message = new();
            message.Append('[').Append(this.PlayerName).Append("] ");
            switch (e)
            {
                case GameHubConnectionStateChangedEventArgs:
                    message.Append("GameHub");
                    this.GameConnectionState = e.State;
                    break;
                case LobbyHubConnectionStateChangedEventArgs:
                    message.Append("LobbyHub");
                    this.LobbyConnectionState = e.State;
                    break;
            }            
            
            message.Append(" "+e.State + " | " + e.Message);
            Console.WriteLine(message.ToString());
        }

        async void LobbyStateChanged(object sender, LobbyStateChangedEventArgs e)
        {
            Console.WriteLine("["+this.PlayerName+"] "+ e.State + " | " + e.Message);
            this.LobbyState = e.State;
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
                    await GameConnection.StartAsync(gameJoinedEvent.GameId);
                    await LobbyConnection.StopAsync();
                    break;
                default:
                    break;
            }
        }

        void GameStateChanged(object sender, GameStateChangedEventArgs e)
        {
            Console.WriteLine("[" + this.PlayerName + "] " + e.State + " | " + e.Message);
        }

        public async Task Quit()
        {
            if (this.LobbyConnectionState != HubConnectionState.Disconnected)
            {
                await LobbyConnection.StopAsync();
            }
            if (this.GameConnectionState != HubConnectionState.Disconnected)
            {
                await GameConnection.StopAsync();
            }
        }
    }
}