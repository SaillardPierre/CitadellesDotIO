using CitadellesDotIO.Engine.DTOs;
using CitadellesDotIO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitadellesDotIO.Client.CustomEventArgs
{
    public class GameJoinedEventArgs : LobbyStateChangedEventArgs
    {
        public string GameId { get; set; }
        public GameJoinedEventArgs(string gameId)
               : base(LobbyState.GameJoined, "Joined game " + gameId)
        {
            this.GameId = gameId;
        }
    }
}
