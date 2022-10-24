using Microsoft.AspNetCore.SignalR.Client;

namespace CitadellesDotIO.Server.Client
{
    public class StateChangedEventArgs : EventArgs
    {
        public HubConnectionState State { get; set; }
        public string Message { get; set; }
        public StateChangedEventArgs(HubConnectionState state, string message)
        {
            this.State = state;
            this.Message = message;
        }
    }
}
