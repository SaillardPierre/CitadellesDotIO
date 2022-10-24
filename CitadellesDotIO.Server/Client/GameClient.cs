using CitadellesDotIO.Engine;
using CitadellesDotIO.Server.Data;
using Microsoft.AspNetCore.SignalR.Client;
using System.Data;

namespace CitadellesDotIO.Server.Client
{
    public class GameClient
    {
        private HubConnection HubConnection;
        private readonly string GameId;
        private readonly Player Player;
        private readonly string HubUrl;

        public delegate void StateChangedEventHandler(object sender, StateChangedEventArgs e);
        public event StateChangedEventHandler StateChanged;

        public bool IsConnected => HubConnection?.State == HubConnectionState.Connected;
        public string ConnectionId => HubConnection?.ConnectionId;

        public List<GameDto> Games { get; set; }

        public GameClient(string gameId, Player player, string siteUrl)
        {
            if (string.IsNullOrWhiteSpace(gameId))
                throw new ArgumentNullException(nameof(gameId));
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            GameId = gameId;
            this.Player = player;
            this.Games = new();
            HubUrl = siteUrl.TrimEnd('/') + "/gameshub";
        }

        public async Task StartAsync()
        {
            if (this.HubConnection == null)
            {
                HubConnection = new HubConnectionBuilder().WithUrl(this.HubUrl).Build();
                HubConnection.Closed += HubConnection_Closed;
                HubConnection.Reconnected += HubConnection_Reconnected;
                HubConnection.Reconnecting += HubConnection_Reconnecting;

                // Ici ajouter les event handlers

                HubConnection.On<List<GameDto>>("PullGames", (games) =>
                {
                    this.Games = games;
                });

                await this.HubConnection.StartAsync();
                
                //await this.HubConnection.
            }
            else if (this.HubConnection.State == HubConnectionState.Disconnected)
            {
                await this.HubConnection.StartAsync();
            }

            // Gérer l'evenement de connexion
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

        public async Task GetGamesAsync()
            => await this.HubConnection.InvokeAsync("SendGamesAsync");
             
        public async Task CreateGameAsync(GameParameters gameParameters)
        {
            await this.HubConnection.InvokeAsync("CreateGameAsync", arg1: gameParameters, arg2: this.Player);
        }

        public async ValueTask DisposeAsync()
        {
            await StopAsync();
        }

        private async Task HubConnection_Reconnecting(Exception arg)
        {
            StateChanged?.Invoke(this, new StateChangedEventArgs(HubConnectionState.Reconnecting, arg?.Message));
        }

        private async Task HubConnection_Reconnected(string arg)
        {
            StateChanged?.Invoke(this, new StateChangedEventArgs(HubConnectionState.Connected, arg));
        }

        private async Task HubConnection_Closed(Exception arg)
        {
            StateChanged?.Invoke(this, new StateChangedEventArgs(HubConnectionState.Disconnected, arg?.Message));
        }
    }
}
