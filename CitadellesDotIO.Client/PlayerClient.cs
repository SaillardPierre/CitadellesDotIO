using CitadellesDotIO.Client.CustomEventArgs;
using CitadellesDotIO.Engine.DTOs;
using CitadellesDotIO.Enums;
using Microsoft.AspNetCore.SignalR.Client;
using System.Text;

namespace CitadellesDotIO.Client
{
    public class PlayerClient
    {
        public LobbyState LobbyState { get; private set; }
        public HubConnectionState LobbyConnectionState => this.LobbyConnection.ConnectionState;
        public HubConnectionState GameConnectionState => this.GameConnection.ConnectionState;
        private string PlayerName { get; set; }
        public List<GameDto> Games { get; set; }
        public GameDto? Game { get; set; }
        private LobbyConnection LobbyConnection { get; set; }
        private GameConnection GameConnection { get; set; }
        public PlayerClient(string siteUrl, string playerName)
        {
            this.PlayerName = playerName;
            this.Games = new();
            LobbyConnection = new(playerName, siteUrl, HubStateChanged, LobbyStateChanged);
            GameConnection = new(playerName, siteUrl, HubStateChanged, GameStateChanged);
        }

        public string? GameSecret => this.GameConnection.GameSecret;
        public string? GameId => this.GameConnection.GameId;

        public async Task StartLobbyConnection()
        {
            await this.LobbyConnection.StartAsync();
        }

        public async Task<bool> CreateGame(string gameName)
        => await this.LobbyConnection.CreateGameAsync(gameName, this.PlayerName);

        public async Task<bool> ConnectToGame(string gameId, string gameSecret)
        {
            await LobbyConnection.StopAsync();
            return await GameConnection.StartAsync(gameId, gameSecret);            
        }

        public async Task<bool> JoinGameAsync(string gameId)
        {
            return await this.LobbyConnection.JoinGameAsync(gameId, this.PlayerName);
        }
        
        public async Task SetReadyState(bool isReady)
        => await this.GameConnection.SetReadyStateAsync(isReady);

        void HubStateChanged(object sender, HubConnectionStateChangedEventArgs e)
        {
            StringBuilder message = new();
            message.Append('[').Append(this.PlayerName).Append("] ");
            switch (e)
            {
                case GameHubConnectionStateChangedEventArgs:
                    message.Append("GameHub");
                    break;
                case LobbyHubConnectionStateChangedEventArgs:
                    message.Append("LobbyHub");
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
                    break;
                case GameJoinedEventArgs gameJoinedEvent:
                    if (!await this.ConnectToGame(gameJoinedEvent.GameId, gameJoinedEvent.GameSecret))
                    {
                        message.AppendLine("Could not connect to game");
                    }
                    break;
                default:
                    break;
            }
            Console.WriteLine(message.ToString());
        }

        void GameStateChanged(object sender, GameStateChangedEventArgs e)
        {
            this.Game = e.Game;
            StringBuilder message = new();
            message.AppendLine("[" + this.PlayerName + "] " + e.Message);
            message.AppendLine("[ Game : " + e.Game.Name + " ] : " + e.State + " ," + e.Game.Players.Count.ToString() + " players :");
            foreach (PlayerDto p in e.Game.Players)
            {
                string isHost = p.IsHost ? " |HOST| " : string.Empty;
                message.AppendLine("Id : " + p.Id + " - Name : " + p.Name + " |" + p.IsReady + "|" + isHost);
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