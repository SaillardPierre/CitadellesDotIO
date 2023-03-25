using CitadellesDotIO.Client.CustomEventArgs;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.Extensions.DependencyInjection;
using CitadellesDotIO.Engine.DTOs;
using CitadellesDotIO.Enums;
using CitadellesDotIO.Client;
using CitadellesDotIO.Engine;

namespace CitadellesDotIO.Client
{
    public class LobbyConnection
    {
        private HubConnection LobbyHubConnection;
        public HubConnectionState ConnectionState => LobbyHubConnection != null ? LobbyHubConnection.State : HubConnectionState.Disconnected;

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
        }



        private bool NotifyGameJoined(string gameId, string gameSecret)
        {
            if (string.IsNullOrWhiteSpace(gameId) || string.IsNullOrWhiteSpace(gameSecret))
            {
                return false;
            }
            // Se connecter au hub grâce à la clé de la game
            this.LobbyStateChanged.Invoke(this, new(LobbyState.GameJoined, "Disconnecting from Lobby after entering a game"));
            this.LobbyStateChanged.Invoke(this, new GameJoinedEventArgs(gameId, gameSecret));
            return true;
        }


        #region Appelées par le serveur vers les client du hub
        public Task ConfirmConnection()
        {
            this.HubConnectionStateChanged.Invoke(this, new LobbyHubConnectionStateChangedEventArgs(HubConnectionState.Connected, "Client successfully connected to LobbyHub"));
            return Task.CompletedTask;
        }
        public Task PullGamesAsync(IEnumerable<GameDto> games)
        {
            this.LobbyStateChanged.Invoke(this, new GamesPulledEventArgs(games));
            return Task.CompletedTask;
        }       
        #endregion

        #region Appelées par le client du hub vers le serveur
        public async Task<bool> CreateGameAsync(string gameName, string playerName)
        {
            var (gameId, gameSecret) = await this.LobbyHubConnection.InvokeAsync<Tuple<string, string>>(nameof(CreateGameAsync), gameName, playerName);
            if (string.IsNullOrWhiteSpace(gameId) || string.IsNullOrWhiteSpace(gameSecret))
            {
                return false;
            }

            return NotifyGameJoined(gameId, gameSecret);
        }

        public async Task<bool> JoinGameAsync(string gameId, string playerName)
        {
            string gameSecret = await this.LobbyHubConnection.InvokeAsync<string>(nameof(JoinGameAsync), gameId, playerName);
            if (string.IsNullOrWhiteSpace(gameSecret))
            {
                return false;
            }
            return NotifyGameJoined(gameId, gameSecret);
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
            HubConnectionStateChanged?.Invoke(this, new LobbyHubConnectionStateChangedEventArgs(HubConnectionState.Reconnecting, arg?.Message!));
            await Task.CompletedTask;
        }
        protected async Task HubConnection_Reconnected(string? arg)
        {
            HubConnectionStateChanged?.Invoke(this, new LobbyHubConnectionStateChangedEventArgs(HubConnectionState.Connected, arg!));
            await Task.CompletedTask;
        }
        protected async Task HubConnection_Closed(Exception? arg)
        {
            HubConnectionStateChanged?.Invoke(this, new LobbyHubConnectionStateChangedEventArgs(HubConnectionState.Disconnected, arg?.Message!));
            await Task.CompletedTask;
        }
        #endregion
    }
}
