using CitadellesDotIO.WebServer.HubsClients;
using CitadellesDotIO.WebServer.Models;
using CitadellesDotIO.WebServer.Services;
using Microsoft.AspNetCore.SignalR;
namespace CitadellesDotIO.WebServer.Hubs
{
    /// <summary>
    /// Classe utilisée pour définir les méthodse appellables par le client SignalR associé
    /// Ces méthodes sont appellables par connection.invoke("NomDeMethode", monParametre = {})
    /// Les méthodes du client (ILobbiesHubClient) sont appelées sur l'objet client concerné
    /// </summary>
    public class LobbiesHub : Hub<ILobbiesHubClient>
    {
        private readonly ILobbiesService lobbiesService;
        public LobbiesHub(ILobbiesService lobbiesService)
        {
            this.lobbiesService = lobbiesService;
        }

        // 
        public Task CreateLobby(Lobby newLobby)
        {
            if (this.lobbiesService.CreateLobby(newLobby))
            {
                return this.BroadcastLobbies();
            }
            else return Task.FromException(new Exception("Failure in create lobby"));
        }

        public Task GetLobbies()
            => this.Clients.Caller.PullLobbies(this.lobbiesService.GetLobbies());

        public Task BroadcastLobbies()
            => this.Clients.All.PullLobbies(this.lobbiesService.GetLobbies());

    }    
}
