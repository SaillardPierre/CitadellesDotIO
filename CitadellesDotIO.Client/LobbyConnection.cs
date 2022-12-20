using CitadellesDotIO.Client.CustomEventArgs;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.Extensions.DependencyInjection;
using CitadellesDotIO.Engine;

namespace CitadellesDotIO.Client
{
    public class LobbyConnection
    {
        private HubConnection LobbyHubConnection;
        public delegate void StateChangedEventHandler(object sender, HubConnectionStateChangedEventArgs e);
        public event StateChangedEventHandler StateChanged;

        public LobbyConnection(string siteUrl, StateChangedEventHandler stateChangedEventHandler)
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
            this.StateChanged += stateChangedEventHandler;
            LobbyHubConnection.On<string>("PullMsg", (msg) =>
            {
                this.StateChanged.Invoke(this, new(HubConnectionState.Connected, msg ));
            });


        }

        public async Task StartAsync()
        {
            await this.LobbyHubConnection.StartAsync();            
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
    }
}
