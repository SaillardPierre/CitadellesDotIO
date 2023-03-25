using CitadellesDotIO.Engine.DTOs;
using CitadellesDotIO.Engine.HubsClient;
using CitadellesDotIO.Engine.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
            await this.Clients.Caller.PullGamesAsync(this.gamesService.GetGames());
        }

        public Task<Tuple<string, string>> CreateGameAsync(string gameName, string playerName)
        {
            string gameId = this.gamesService.CreateGame(gameName, playerName);
            if (this.gamesService.TryAddPlayerToGame(gameId, playerName, out string gameSecret, true))
            {
                _ = this.UpdateJoinedGame();
            }
            return Task.FromResult(Tuple.Create(gameId, gameSecret));
        }

        public Task<string> JoinGameAsync(string gameId, string playerName)
        {
            if (this.gamesService.TryAddPlayerToGame(gameId, playerName, out string gameSecret, false))
            {
                _ = this.UpdateJoinedGame();
            }
            return Task.FromResult(gameSecret);
        }

        private async Task UpdateJoinedGame()
        {
            string callerId = this.Context.ConnectionId;
            IEnumerable<GameDto> games = this.gamesService.GetGames();
            await this.Clients.AllExcept(new string[] { callerId }).PullGamesAsync(games);
        }
    }
}
