using CitadellesDotIO.Client.CustomEventArgs;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.Extensions.DependencyInjection;
using CitadellesDotIO.Engine.DTOs;
using CitadellesDotIO.Enums;

namespace CitadellesDotIO.Client
{
    public class LobbyConnection
    {
        private HubConnection LobbyHubConnection;

        // Gère les evenements liés au hub
        public delegate void HubConnectionStateChangedEventHandler(object sender, HubConnectionStateChangedEventArgs e);
        public event HubConnectionStateChangedEventHandler HubConnectionStateChanged;

        // Gère les evenements liés au lobby
        public delegate void LobbyStateChangedEventHandler(object sender, LobbyStateChangedEventArgs e);
        public event LobbyStateChangedEventHandler LobbyStateChanged;

        public LobbyConnection(
            string playerName,
            string siteUrl,
            HubConnectionStateChangedEventHandler hubConnectionStateChangedEventHandler,
            LobbyStateChangedEventHandler lobbyStateChangedEventHandler)
        {
            string hubUrl = siteUrl.TrimEnd('/') + "/lobbyhub";
            LobbyHubConnection = new HubConnectionBuilder()
                   .WithUrl(hubUrl, cfg =>
                   {
                       cfg.Transports = HttpTransportType.WebSockets;
                   })
                   .AddNewtonsoftJsonProtocol(opts =>
                   {
                       opts.PayloadSerializerSettings.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto;
                       opts.PayloadSerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
                   })
                   .WithAutomaticReconnect()
                   .Build();

            LobbyHubConnection.Closed += HubConnection_Closed;
            LobbyHubConnection.Reconnected += HubConnection_Reconnected;
            LobbyHubConnection.Reconnecting += HubConnection_Reconnecting;
            this.HubConnectionStateChanged += hubConnectionStateChangedEventHandler;
            this.LobbyStateChanged += lobbyStateChangedEventHandler;

            LobbyHubConnection.On(nameof(ConfirmConnection), async () => await this.ConfirmConnection());
            LobbyHubConnection.On<IEnumerable<GameDto>>(nameof(this.PullGamesAsync), (games) => this.PullGamesAsync(games));
            LobbyHubConnection.On<string>(nameof(this.PullGameId), (gameId) => this.PullGameId(gameId));
            LobbyHubConnection.On(nameof(this.GameNotFound), () => this.GameNotFound());
        }

        
        #region Appelées par le serveur vers les client du hub
        public Task ConfirmConnection()
        {
            this.HubConnectionStateChanged.Invoke(this, new(HubConnectionState.Connected, "Client successfully connected to LobbyHub"));
            return Task.CompletedTask;
        }
        public Task PullGamesAsync(IEnumerable<GameDto> games)
        {
            this.LobbyStateChanged.Invoke(this, new GamesPulledEventArgs(games));
            return Task.CompletedTask;
        }
        public Task PullGameId(string gameId)
        {
            // Se connecter au hub grâce à la clé de la game
            this.LobbyStateChanged.Invoke(this, new GameJoinedEventArgs(gameId));
            return Task.CompletedTask;
        }
        public Task GameNotFound()
        {
            this.LobbyStateChanged.Invoke(this, new(LobbyState.GameNotFound, "Could not connect to game"));
            return Task.CompletedTask;
        }
        #endregion

        #region Appelées par le client du hub vers le serveur
        public async Task CreateGameAsync(string gameName, string playerName)
        {                    
            await this.LobbyHubConnection.InvokeAsync(nameof(CreateGameAsync), gameName, playerName);
            this.LobbyStateChanged.Invoke(this, new(LobbyState.CreatingGame, "Player creating game " + gameName));
        }
        public async Task JoinGameAsync(string gameId, string playerName)
        {
            await this.LobbyHubConnection.InvokeAsync(nameof(JoinGameAsync), gameId, playerName);
            this.LobbyStateChanged.Invoke(this, new(LobbyState.JoiningGame, "Tryin to connect to game " + gameId));            
        }
        #endregion

        #region Lifecyle
        public async Task StartAsync()
        {
            await this.LobbyHubConnection.StartAsync();
        }
        public async Task StopAsync()
        {
            if (this.LobbyHubConnection != null && this.LobbyHubConnection.State != HubConnectionState.Disconnected)
            {              
                // Gérer l'evenement de déconnexion
                await this.LobbyHubConnection.StopAsync();
                await this.LobbyHubConnection.DisposeAsync();
                this.LobbyHubConnection = null!;
            }
        }
        protected async Task HubConnection_Reconnecting(Exception? arg)
        {
            HubConnectionStateChanged?.Invoke(this, new HubConnectionStateChangedEventArgs(HubConnectionState.Reconnecting, arg?.Message!));
            await Task.CompletedTask;
        }
        protected async Task HubConnection_Reconnected(string? arg)
        {
            HubConnectionStateChanged?.Invoke(this, new HubConnectionStateChangedEventArgs(HubConnectionState.Connected, arg!));
            await Task.CompletedTask;
        }
        protected async Task HubConnection_Closed(Exception? arg)
        {
            HubConnectionStateChanged?.Invoke(this, new HubConnectionStateChangedEventArgs(HubConnectionState.Disconnected, arg?.Message!));
            await Task.CompletedTask;
        }
        #endregion
    }
}
