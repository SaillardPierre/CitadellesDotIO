using CitadellesDotIO.Engine.HubsClient;
using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CitadellesDotIO.Engine.Hubs
{
    public class GameHub : Hub<IGameHubClient>
    {
        public async Task OnConnectedAsync(string gameId)
        {
            await this.Groups.AddToGroupAsync(this.Context.ConnectionId, gameId);
        }
    }
}
