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
            IEnumerable<GameDto> games = this.gamesService.GetGames();
            await this.Clients.Caller.PullGamesAsync(games);
        }        

        public async Task CreateGameAsync(string gameName, string playerName)
        {
            string gameId = this.gamesService.CreateGame(gameName, playerName);
            await this.JoinGameAsync(gameId, playerName, true);
        }

        public async Task JoinGameAsync(string gameId, string playerName, bool isHost = false)
        {
            if (this.gamesService.AddPlayerToGame(gameId, playerName, isHost))
            {
                await this.Clients.Caller.PullGameId(gameId);
                _ = this.UpdateJoinedGame();
            }
            else await this.Clients.Caller.GameNotFound();
        }

        public async Task JoinGameByNameAsync(string gameName, string playerName)
        {
            if (this.gamesService.AddPlayerToGameByGameName(gameName, playerName, out string gameId))
            {
                await this.Clients.Caller.PullGameId(gameId);
                _ = this.UpdateJoinedGame();
            }
            else await this.Clients.Caller.GameNotFound();
        }

        private async Task UpdateJoinedGame()
        {
            string callerId = this.Context.ConnectionId;            
            IEnumerable<GameDto> games = this.gamesService.GetGames();
            await this.Clients.AllExcept(new string[] { callerId }).PullGamesAsync(games);
        }
    }
}
