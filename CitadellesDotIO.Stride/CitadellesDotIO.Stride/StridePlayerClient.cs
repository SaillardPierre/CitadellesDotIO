using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;
using CitadellesDotIO.Client;
using CitadellesDotIO.Engine.View;
using CitadellesDotIO.Engine;

namespace CitadellesDotIO.Stride
{
    public class StridePlayerClient : StartupScript
    {
        // Declared public member fields and properties will show in the game studio
        public PlayerClient Client { get; set; }
        public string LobbyId => this.Client.LobbiesConnection?.LobbyId;

        public async override void Start()
        {
         
            this.Client = await PlayerClient.BuildPlayerClientAsync("Pierre", new ConsoleView());
            await this.Client.LobbiesConnection.CreateLobbyAsync("Console Test Lobby");

            Log.Info(this.LobbyId);
        }
    }
}
