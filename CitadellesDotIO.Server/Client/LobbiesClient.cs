using CitadellesDotIO.Engine;
using CitadellesDotIO.Server.Data;
using CitadellesDotIO.Server.Models;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;

namespace CitadellesDotIO.Server.Client
{
    public class LobbiesClient
    {
        public HubConnection HubConnection;
        private string HubUrl;

        public delegate void StateChangedEventHandler(object sender, StateChangedEventArgs e);
        public event StateChangedEventHandler StateChanged;

        public bool IsConnected => HubConnection?.State == HubConnectionState.Connected;
        public string ConnectionId => HubConnection?.ConnectionId;


        public Player Player { get; set; }
        public List<Lobby> Lobbies { get; set; }
        public Lobby newLobby => new("DefaultLobbyName");
        public LobbiesClient(Player player, string siteUrl)
        {
            this.Lobbies = new List<Lobby>();
            this.Player = player;

            HubUrl = siteUrl.TrimEnd('/') + "/lobbieshub";
            HubConnection = new HubConnectionBuilder()
                   .WithUrl(this.HubUrl, cfg =>
                   {
                       cfg.SkipNegotiation = true;
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
            });          
        }

        public async Task StartAsync()
        {
            await this.HubConnection.StartAsync();
        }

        public async Task GetLobbiesAsync()
        {
            await this.HubConnection.InvokeAsync("SendLobbiesAsync");
        }

        public async Task CreateLobbyAsync()
        {
            this.newLobby.Players.Add(this.Player);
            await this.HubConnection.InvokeAsync("CreateLobbyAsync", arg1: newLobby);
        }

        public async Task StopAsync()
        {
            if (this.HubConnection != null && this.HubConnection.State != HubConnectionState.Disconnected)
            {
                // Gérer l'evenement de déconnexion
                await this.HubConnection.StopAsync();
                await this.HubConnection.DisposeAsync();
                this.HubConnection = null;
            }
        }

        protected async Task HubConnection_Reconnecting(Exception arg)
        {
            StateChanged?.Invoke(this, new StateChangedEventArgs(HubConnectionState.Reconnecting, arg?.Message));
        }

        protected async Task HubConnection_Reconnected(string arg)
        {
            StateChanged?.Invoke(this, new StateChangedEventArgs(HubConnectionState.Connected, arg));
        }

        protected async Task HubConnection_Closed(Exception arg)
        {
            StateChanged?.Invoke(this, new StateChangedEventArgs(HubConnectionState.Disconnected, arg?.Message));
        }
        public async ValueTask DisposeAsync()
        {
            await StopAsync();
        }
    }
}
