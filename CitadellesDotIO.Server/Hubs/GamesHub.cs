using CitadellesDotIO.Engine;
using CitadellesDotIO.Server.HubsClients;
using CitadellesDotIO.Server.Services;
using Microsoft.AspNetCore.SignalR;

namespace CitadellesDotIO.Server.Hubs
{
    public class GamesHub : Hub<IGamesHubClient>
    {
        private readonly GamesService gamesService;

        public GamesHub(GamesService gamesService)
        {
            this.gamesService = gamesService;
        }

        public async Task SendGamesAsync()
            => await this.Clients.Caller.PullGames(await this.gamesService.GetGamesAsync());

        public async Task BroadcastGamesAsync()
            => await this.Clients.All.PullGames(await this.gamesService.GetGamesAsync());
           

        public async Task CreateGameAsync(GameParameters gameParameters, Player gameCreator)
            => await gamesService.CreateGameAsync(this, gameParameters, gameCreator);

        //public async Task PlayerJoinedAsync(Player player, string gameId)
        //    => await gamesService.PlayerJoinedAsync(this, player, gameId);
    }
}
