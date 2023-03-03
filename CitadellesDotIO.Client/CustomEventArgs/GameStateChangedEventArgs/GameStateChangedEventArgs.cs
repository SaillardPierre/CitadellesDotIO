using CitadellesDotIO.Engine.DTOs;
using CitadellesDotIO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitadellesDotIO.Client.CustomEventArgs
{
    public class GameStateChangedEventArgs : EventArgs
    {
        public GameState State { get; set; }
        public string Message { get; set; }
        public GameDto Game { get; set; }
        public GameStateChangedEventArgs(GameState state, string message, GameDto game)
        {
            State = state;
            Message = message;
            Game = game;
        }
    }
}
