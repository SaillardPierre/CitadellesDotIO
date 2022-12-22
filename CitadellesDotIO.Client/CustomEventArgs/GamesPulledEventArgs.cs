using CitadellesDotIO.Engine.DTOs;
using CitadellesDotIO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitadellesDotIO.Client.CustomEventArgs
{
    public class GamesPulledEventArgs : LobbyStateChangedEventArgs
    {
        public IEnumerable<GameDto> Games { get; }

        public GamesPulledEventArgs(IEnumerable<GameDto> games) 
            : base(LobbyState.GamesPulled, "Pulled "+games.Count()+"games")
        {
            this.Games = games;
        }

    }
}
