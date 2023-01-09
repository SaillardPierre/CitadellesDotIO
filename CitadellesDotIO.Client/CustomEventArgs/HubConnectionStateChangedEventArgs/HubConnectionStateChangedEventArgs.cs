using Microsoft.AspNetCore.SignalR.Client;

namespace CitadellesDotIO.Client.CustomEventArgs
{
    public class HubConnectionStateChangedEventArgs : EventArgs
    {
        public HubConnectionState State { get; set; }
        public string Message { get; set; }
        public HubConnectionStateChangedEventArgs(HubConnectionState state, string message)
        {
            State = state;
            Message = message;
        }
    }
}
