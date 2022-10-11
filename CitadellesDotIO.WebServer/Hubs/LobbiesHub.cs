using CitadellesDotIO.WebServer.Models;
using Microsoft.AspNetCore.SignalR;
namespace CitadellesDotIO.WebServer.Hubs
{
    public class LobbiesHub : Hub<ILobbiesHub>
    {
        private readonly ILobbiesService lobbiesService;
        public LobbiesHub(ILobbiesService lobbiesService)
        {
            this.lobbiesService = lobbiesService;
        }

        // Méthodes appellables par connection.invoke("NomDeMethode", monParametre = {})
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
            =>this.Clients.All.PullLobbies(this.lobbiesService.GetLobbies());
        
    }
}
