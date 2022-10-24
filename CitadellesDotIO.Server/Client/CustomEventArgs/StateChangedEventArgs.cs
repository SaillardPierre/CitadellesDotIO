using Microsoft.AspNetCore.SignalR.Client;

namespace CitadellesDotIO.Server.Client.CustomEventArgs
{
    public class StateChangedEventArgs : EventArgs
    {
        public HubConnectionState State { get; set; }
        public string Message { get; set; }
        public StateChangedEventArgs(HubConnectionState state, string message)
        {
            State = state;
            Message = message;
        }
    }
}
