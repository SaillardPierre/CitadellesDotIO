using CitadellesDotIO.Server.Client.CustomEventArgs;
using Microsoft.AspNetCore.SignalR.Client;

namespace CitadellesDotIO.Server.Client
{
    public abstract class Client
    {
        protected HubConnection HubConnection;
        protected string HubUrl;

        public delegate void StateChangedEventHandler(object sender, StateChangedEventArgs e);
        public event StateChangedEventHandler StateChanged;

        public bool IsConnected => HubConnection?.State == HubConnectionState.Connected;
        public string ConnectionId => HubConnection?.ConnectionId;

        public virtual async Task StartAsync()
        {
            if (this.HubConnection == null)
            {
                HubConnection = new HubConnectionBuilder().WithUrl(this.HubUrl).Build();
                HubConnection.Closed += HubConnection_Closed;
                HubConnection.Reconnected += HubConnection_Reconnected;
                HubConnection.Reconnecting += HubConnection_Reconnecting;
                await this.HubConnection.StartAsync();
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
