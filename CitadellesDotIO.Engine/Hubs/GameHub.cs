using CitadellesDotIO.Engine.DTOs;
using CitadellesDotIO.Engine.HubsClient;
using CitadellesDotIO.Engine.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace CitadellesDotIO.Engine.Hubs
{
    public class GameHub : Hub<IGameHubClient>
    {
        private readonly IGamesService gamesService;
        public GameHub(IGamesService gamesService)
        {
            this.gamesService = gamesService;
        }

        public async override Task OnConnectedAsync()
        {
            await this.Clients.Caller.RegisterPlayer();
        }
        public string LastCallerId { get; set; }
        public async Task RegisterPlayer(string gameId, string playerName)
        {
            await this.Groups.AddToGroupAsync(this.Context.ConnectionId, gameId);
            if (!this.gamesService.TrySetPlayerConnection(gameId, playerName, this.Context.ConnectionId, out GameDto game))
            {
                return;
            }
            Console.WriteLine("Player " + playerName + " joined " + gameId + " with connection " + this.Context.ConnectionId);
            await this.Clients.Group(gameId).UpdateGame(game);
        }
    }
}

