using CitadellesDotIO.Engine.DTOs;
using CitadellesDotIO.Engine.HubsClient;
using CitadellesDotIO.Engine.Services;
using Microsoft.AspNetCore.SignalR;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitadellesDotIO.Engine.Hubs
{
    public class LobbyHub : Hub<ILobbyHubClient>
    {
        private readonly IGamesService gamesService; 
        public LobbyHub(IGamesService gamesService)
        {
            this.gamesService = gamesService;
        }

        public override async Task OnConnectedAsync()
        {
            await this.Clients.Caller.ConfirmConnection();
            await this.GetGamesAsync();            
        }

        public async Task GetGamesAsync()
        {
            IEnumerable<GameDto> games = await this.gamesService.GetGames();
            await this.Clients.Caller.PullGamesAsync(games);
        }
        

        public async Task CreateGameAsync(string gameName, string playerName)
        {
            string gameId = this.gamesService.CreateGame(gameName, playerName);
            await this.JoinGameAsync(gameId, playerName);
        }

        public async Task JoinGameAsync(string gameId, string playerName)
        {
            if (this.gamesService.AddPlayerToGame(gameId, playerName))
            {
                string callerId = this.Context.ConnectionId;                
                await this.Clients.Caller.PullGameId(gameId);
                IEnumerable<GameDto> games = await this.gamesService.GetGames();
                await this.Clients.AllExcept(new List<string>(){callerId}).PullGamesAsync(games);
            }
            else await this.Clients.Caller.GameNotFound();
        }

    }
}
