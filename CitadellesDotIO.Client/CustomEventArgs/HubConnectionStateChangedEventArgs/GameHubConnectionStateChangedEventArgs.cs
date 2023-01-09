using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitadellesDotIO.Client.CustomEventArgs
{
    public class GameHubConnectionStateChangedEventArgs : HubConnectionStateChangedEventArgs
    {
        public GameHubConnectionStateChangedEventArgs(HubConnectionState state, string message) : base(state, message)
        {
        }
    }
}
