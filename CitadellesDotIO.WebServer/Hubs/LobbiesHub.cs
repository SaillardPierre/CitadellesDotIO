using CitadellesDotIO.WebServer.HubsClients;
using Microsoft.AspNetCore.SignalR;
namespace CitadellesDotIO.WebServer.Hubs
{
    public class LobbiesHub : Hub<ILobbiesHubClient>
    {
    }
}
