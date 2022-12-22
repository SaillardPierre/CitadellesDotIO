using CitadellesDotIO.Enums;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitadellesDotIO.Client.CustomEventArgs
{
    public class LobbyStateChangedEventArgs : EventArgs
    {
        public LobbyState State { get; set; }
        public string Message { get; set; }
        public LobbyStateChangedEventArgs(LobbyState state, string message)
        {
            State = state;
            Message = message;
        }
    }
}
