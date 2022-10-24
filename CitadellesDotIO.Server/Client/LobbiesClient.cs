using CitadellesDotIO.Engine;
using CitadellesDotIO.Server.Client.CustomEventArgs;
using CitadellesDotIO.Server.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;

namespace CitadellesDotIO.Server.Client
{
    public class LobbiesClient
    {
        private HubConnection HubConnection;

        public delegate void StateChangedEventHandler(object sender, StateChangedEventArgs e);
        public event StateChangedEventHandler StateChanged;

        public delegate void DataChangedEventHandler();
        public event DataChangedEventHandler DataChanged;

        public bool IsConnected => HubConnection?.State == HubConnectionState.Connected;
        public string? ConnectionId => HubConnection != null ? HubConnection.ConnectionId : null;

        public Player Player { get; set; }
        public List<Lobby> Lobbies { get; set; }
        public List<Player> Players { get; set; }
        public Lobby NewLobby { get; set; }
        public LobbiesClient(Player player, string siteUrl, DataChangedEventHandler dataChangedEventHandler, StateChangedEventHandler stateChangedEventHandler )
        {
            this.Lobbies = new();
            this.Players = new();
            this.Player = player;
            this.NewLobby = new(string.Empty);
            this.DataChanged = dataChangedEventHandler;
            this.StateChanged = stateChangedEventHandler;

            string hubUrl = siteUrl.TrimEnd('/') + "/lobbieshub";
            HubConnection = new HubConnectionBuilder()
                   .WithUrl(hubUrl, cfg =>
                   {
                       cfg.Transports = HttpTransportType.WebSockets;
                   })
                   .AddNewtonsoftJsonProtocol(opts =>
                        opts.PayloadSerializerSettings.TypeNameHandling = TypeNameHandling.Auto)
                   .WithAutomaticReconnect()
                   .Build();

            HubConnection.Closed += HubConnection_Closed;
            HubConnection.Reconnected += HubConnection_Reconnected;
            HubConnection.Reconnecting += HubConnection_Reconnecting;
            // Ici ajouter les event handlers
            HubConnection.On<IList<Lobby>>("PullLobbies", (lobbies) =>
            {
                this.Lobbies = lobbies.ToList();
                this.DataChanged.Invoke();
            });
            HubConnection.On<IList<Player>>("PullPlayers", (players) =>
            {
                this.Players = players.ToList();
                this.DataChanged.Invoke();
            });
        }

        public async Task StartAsync()
        {
            await this.HubConnection.StartAsync();
            this.Player.Id = this.ConnectionId;
            await this.HubConnection.InvokeAsync("RegisterPlayerAsync", this.Player);
        }

        public async Task GetLobbiesAsync()
        {
            await this.HubConnection.InvokeAsync("SendLobbiesAsync");
        }

        public async Task GetPlayersAsync()
        {
            await this.HubConnection.InvokeAsync("SendPlayersAsync");
        }

        public async Task CreateLobbyAsync()
        {            
            await this.HubConnection.InvokeAsync("CreateLobbyAsync", NewLobby);
            await this.JoinLobbyAsync(NewLobby.Id);
        }

        public async Task JoinLobbyAsync(string lobbyId)
        {
            await this.HubConnection.InvokeAsync("JoinLobbyAsync", lobbyId, this.ConnectionId);
        }

        public async Task StopAsync()
        {
            if (this.HubConnection != null && this.HubConnection.State != HubConnectionState.Disconnected)
            {
                // Gérer l'evenement de déconnexion
                await this.HubConnection.StopAsync();
                await this.HubConnection.DisposeAsync();
                this.HubConnection = null!;
            }
        }

        protected async Task HubConnection_Reconnecting(Exception? arg)
        {
            StateChanged?.Invoke(this, new StateChangedEventArgs(HubConnectionState.Reconnecting, arg?.Message!));
        }

        protected async Task HubConnection_Reconnected(string? arg)
        {
            StateChanged?.Invoke(this, new StateChangedEventArgs(HubConnectionState.Connected, arg!));
        }

        protected async Task HubConnection_Closed(Exception? arg)
        {
            StateChanged?.Invoke(this, new StateChangedEventArgs(HubConnectionState.Disconnected, arg?.Message!));
        }
        public async ValueTask DisposeAsync()
        {
            await StopAsync();
        }
    }
}
