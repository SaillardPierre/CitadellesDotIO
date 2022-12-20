using CitadellesDotIO.Client.CustomEventArgs;
using Microsoft.AspNet.SignalR.Client;

namespace CitadellesDotIO.Client
{
    internal class GameConnection
    {
        private HubConnection GameHubConnection;
        public delegate void StateChangedEventHandler(object sender, HubConnectionStateChangedEventArgs e);
        public event StateChangedEventHandler StateChanged;

        public GameConnection() { 

        }
    }
}
