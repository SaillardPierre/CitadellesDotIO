using CitadellesDotIO.Client.CustomEventArgs;
using CitadellesDotIO.Engine;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Web.Helpers;

namespace CitadellesDotIO.Client
{
    public class LobbiesConnection
    {
        private HubConnection HubConnection;

        public delegate void StateChangedEventHandler(object sender, HubConnectionStateChangedEventArgs e);
        public event StateChangedEventHandler StateChanged;

        public bool IsConnected => HubConnection?.State == HubConnectionState.Connected;
        public string? ConnectionId => HubConnection?.ConnectionId;
        public string? LobbyId { get; set; }


        public Player Player { get; set; }
        public List<Lobby> Lobbies { get; set; }
        public List<Player> Players { get; set; }
        public Lobby NewLobby { get; set; }
        public LobbiesConnection(Player player, string siteUrl, StateChangedEventHandler stateChangedEventHandler)
        {
            this.Lobbies = new();
            this.Players = new();
            this.Player = player;
            this.NewLobby = new(string.Empty);
            this.StateChanged = stateChangedEventHandler;

            string hubUrl = siteUrl.TrimEnd('/') + "/lobbieshub";
            HubConnection = new HubConnectionBuilder()
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

            HubConnection.Closed += HubConnection_Closed;
            HubConnection.Reconnected += HubConnection_Reconnected;
            HubConnection.Reconnecting += HubConnection_Reconnecting;
            // Ici ajouter les event handlers
            HubConnection.On<IList<Lobby>>("PullLobbies", (lobbies) =>
            {
                this.Lobbies = lobbies.ToList();
                this.StateChanged.Invoke(this, new(HubConnectionState.Connected, "Pulled lobbies"));
            });
            HubConnection.On<IList<Player>>("PullPlayers", (players) =>
            {
                Player? self = players.SingleOrDefault(p => p.Id == this.ConnectionId);
                if (self != null)
                {
                    this.Player = self;
                    players.Remove(this.Player);
                }
                this.Players = players.ToList();
                this.StateChanged.Invoke(this, new(HubConnectionState.Connected, "Pulled players"));
            });
            HubConnection.On<string>("PullLobbyId", (lobbyId) =>
            {
                this.LobbyId = lobbyId;
                this.StateChanged.Invoke(this, new(HubConnectionState.Connected, "Pulled LobbyId"));
            });
            HubConnection.On<Game>("PullGame", (game) =>
            {
                var bp = "bp";
            });
        }

        public bool IsPlayerInLobby(string lobbyId)
        => this.LobbyId == lobbyId;

        public bool CanPlayerJoinLobby
        => string.IsNullOrWhiteSpace(this.LobbyId);

        public async Task StartAsync()
        {
            await this.HubConnection.StartAsync();
            this.Player.Id = this.ConnectionId;         
            await this.HubConnection.InvokeAsync("RegisterPlayerAsync",this.Player);
        }

        public async Task StartGameAsync()
        {
            if (string.IsNullOrWhiteSpace(this.LobbyId))
            {
                return;
            }
            await this.HubConnection.InvokeAsync("StartGameAsync", this.LobbyId);
        }

        public async Task GetLobbiesAsync()
        {
            await this.HubConnection.InvokeAsync("SendLobbiesAsync");
        }

        public async Task GetPlayersAsync()
        {
            await this.HubConnection.InvokeAsync("SendPlayersAsync");
        }

        public async Task CreateLobbyAsync(string newLobbyName)
        => await this.HubConnection.InvokeAsync("CreateLobbyAsync", newLobbyName);


        public async Task JoinLobbyAsync(string lobbyId)
        => await this.HubConnection.InvokeAsync("JoinLobbyAsync", lobbyId, this.ConnectionId);

        public async Task LeaveLobbyAsync()
        => await this.HubConnection.InvokeAsync("LeaveLobbyAsync", this.LobbyId, this.ConnectionId);

        public async Task StopAsync()
        {
            if (this.HubConnection != null && this.HubConnection.State != HubConnectionState.Disconnected)
            {
                if (!string.IsNullOrWhiteSpace(this.LobbyId))
                {
                    await this.HubConnection.InvokeAsync("LeaveLobbyAsync", this.LobbyId, this.ConnectionId);
                }
                await this.HubConnection.InvokeAsync("UnregisterPlayerAsync", this.ConnectionId);
                // Gérer l'evenement de déconnexion
                await this.HubConnection.StopAsync();
                await this.HubConnection.DisposeAsync();
                this.HubConnection = null!;
            }
        }

        protected async Task HubConnection_Reconnecting(Exception? arg)
        {
            StateChanged?.Invoke(this, new HubConnectionStateChangedEventArgs(HubConnectionState.Reconnecting, arg?.Message!));
            await Task.CompletedTask;
        }

        protected async Task HubConnection_Reconnected(string? arg)
        {
            StateChanged?.Invoke(this, new HubConnectionStateChangedEventArgs(HubConnectionState.Connected, arg!));
            await Task.CompletedTask;
        }

        protected async Task HubConnection_Closed(Exception? arg)
        {
            StateChanged?.Invoke(this, new HubConnectionStateChangedEventArgs(HubConnectionState.Disconnected, arg?.Message!));
            await Task.CompletedTask;
        }
        public async ValueTask DisposeAsync()
        {
            await StopAsync();
        }
    }
}
