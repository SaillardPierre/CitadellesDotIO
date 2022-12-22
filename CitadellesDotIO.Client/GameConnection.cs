using CitadellesDotIO.Client.CustomEventArgs;
using CitadellesDotIO.Engine;
using CitadellesDotIO.Engine.HubsClient;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using static CitadellesDotIO.Client.LobbyConnection;

namespace CitadellesDotIO.Client
{
    internal class GameConnection : IGameHubClient
    {
        private string GameId { get; set; }

        private HubConnection GameHubConnection;
        public delegate void HubConnectionStateChangedEventHandler(object sender, HubConnectionStateChangedEventArgs e);
        public event HubConnectionStateChangedEventHandler HubConnectionStateChanged;

        public delegate void GameStateChangedEventHandler(object sender, GameStateChangedEventArgs e);
        public event GameStateChangedEventHandler GameStateChanged;

        public GameConnection(string siteUrl,
            HubConnectionStateChangedEventHandler hubConnectionStateChangedEventHandler,
            GameStateChangedEventHandler gameStateChangedEventHandler)
        {
            string hubUrl = siteUrl.TrimEnd('/') + "/gamehub";
            GameHubConnection = new HubConnectionBuilder()
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

            GameHubConnection.Closed += HubConnection_Closed;
            GameHubConnection.Reconnected += HubConnection_Reconnected;
            GameHubConnection.Reconnecting += HubConnection_Reconnecting;
            this.HubConnectionStateChanged += hubConnectionStateChangedEventHandler;
            this.GameStateChanged += gameStateChangedEventHandler;

            GameHubConnection.On(nameof(RegisterPlayer), async () => await this.RegisterPlayer());
            GameHubConnection.On<string>("SendTest", async (msg) => await this.SendTest(msg));


        }
        #region Appelées par le serveur vers les client du hub
        public async Task RegisterPlayer()
        {
            await GameHubConnection.InvokeAsync(nameof(RegisterPlayer), this.GameId);
        }

        public Task SendTest(string message)
        {
            this.GameStateChanged.Invoke(this, new(Enums.GameState.Created, message));
            return Task.CompletedTask;
        }
        #endregion
        #region Appelées par le client du hub vers le serveur

        #endregion
        #region Lifecyle
        public async Task StartAsync(string gameId)
        {
            this.GameId = gameId;
            await this.GameHubConnection.StartAsync();
        }
        public async Task StopAsync()
        {
            if (this.GameHubConnection != null && this.GameHubConnection.State != HubConnectionState.Disconnected)
            {
                // Gérer l'evenement de déconnexion
                await this.GameHubConnection.StopAsync();
                await this.GameHubConnection.DisposeAsync();
                this.GameHubConnection = null!;
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
