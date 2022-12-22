using CitadellesDotIO.Engine.HubsClient;
using CitadellesDotIO.Engine.Services;
using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CitadellesDotIO.Engine.Hubs
{
    public class GameHub : Hub<IGameHubClient>
    {       
        public GameHub() {
            
        }
        public async override Task OnConnectedAsync()
        {
            await this.Clients.Caller.RegisterPlayer();
        }

        public async Task RegisterPlayer(string gameId)
        {
            await this.Groups.AddToGroupAsync(this.Context.ConnectionId, gameId);
            await this.Clients.Group(gameId).SendTest("Prout");
        }
    }
}
