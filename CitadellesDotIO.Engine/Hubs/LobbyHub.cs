using CitadellesDotIO.Engine.HubsClient;
using CitadellesDotIO.Engine.Services;
using Microsoft.AspNetCore.SignalR;
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
            await this.Clients.All.PullMsg("Un message");
        }


        public async Task CreateGameAsync(string gameName, string playerName)
        {
            string gameId = await this.gamesService.CreateGameAsync(gameName, playerName);
            await this.Clients.Caller.PullGameId(gameId);
        }

        // A Appeler par this.HubConnection.InvokeAsync("JoinGameAsync", gameId, string playerName)
        public async Task JoinGameAsync(string gameId, string playerName)
        {
            await this.gamesService.AddPlayerToGameAsync(gameId, playerName);
            await this.Clients.Caller.ConnectToGameHub(gameId);
        }

    }
}
