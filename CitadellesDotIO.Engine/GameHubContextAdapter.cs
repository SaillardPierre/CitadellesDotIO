using CitadellesDotIO.Engine.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitadellesDotIO.Engine
{
    public class GameHubContextAdapter
    {
        private string GameId { get; set; }
        private readonly IHubContext<GameHub> GameHubContext;
        public GameHubContextAdapter(IHubContext<GameHub> hubContext, string gameId)
        {
            this.GameHubContext = hubContext;
            this.GameId = gameId;
        }

        public async Task SendTest()
        {
            await this.GameHubContext.Clients.Group(this.GameId).SendAsync(nameof(SendTest), "Test thrown from GameHubContextAdapter for game : "+GameId);
        }        
    }
}
