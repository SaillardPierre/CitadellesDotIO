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

        public void CreateGame(string gameName)
        => this.LobbyConnection.CreateGameAsync(gameName, this.PlayerName);

        public void JoinGame(string gameId)
        => this.LobbyConnection.JoinGameAsync(gameId, this.PlayerName);

        public void JoinGameByGameName(string gameName)
        => this.LobbyConnection.JoinGameByNameAsync(gameName, this.PlayerName);


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

            message.AppendLine(" " + e.State + " | " + e.Message);
            Console.WriteLine(message.ToString());
        }

        async void LobbyStateChanged(object sender, LobbyStateChangedEventArgs e)
        {
            StringBuilder message = new();
            message.AppendLine("[" + this.PlayerName + "] " + e.State + " | " + e.Message);
            this.LobbyState = e.State;
            switch (e)
            {
                case GamesPulledEventArgs gamesPulledEvent:
                    this.Games = gamesPulledEvent.Games.ToList();
                    foreach (GameDto g in this.Games)
                    {
                        message.AppendLine("Id : " + g.Id + " - Name : " + g.Name + " - Players : " + g.Players.Count);
                    }
                    Console.WriteLine(message.ToString());
                    break;
                case GameJoinedEventArgs gameJoinedEvent:
                    await LobbyConnection.StopAsync();
                    await GameConnection.StartAsync(gameJoinedEvent.GameId);
                    break;                
                default:
                    break;
            }
        }

        void GameStateChanged(object sender, GameStateChangedEventArgs e)
        {
            StringBuilder message = new();
            message.AppendLine("[" + this.PlayerName + "] " + e.Message);
            message.AppendLine("[ Game : " + e.Game.Name + " ] : " + e.State + " ," + e.Game.Players.Count.ToString() + " players :");
            foreach (PlayerDto p in e.Game.Players)
            {
                string isHost = p.IsHost ? " |HOST|" : string.Empty;
                message.AppendLine("Id : " + p.Id + " - Name : " + p.Name + isHost);
            }
            Console.WriteLine(message.ToString());
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