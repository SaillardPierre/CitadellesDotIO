using CitadellesDotIO.Server.Data;
using CitadellesDotIO.Server.HubsClients;
using CitadellesDotIO.Server.Models;
using CitadellesDotIO.Server.Services;
using Microsoft.AspNetCore.SignalR;
namespace CitadellesDotIO.Server.Hubs
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
        public async Task CreateLobbyAsync(Lobby newLobby)
        {
            if (await this.lobbiesService.CreateLobbyAsync(newLobby))
            {
                //await this.BroadcastLobbiesAsync();
            }
            else await Task.FromException(new Exception("Failure in create lobby"));
        }

        [HubMethodName("SendLobbiesAsync")]
        public async Task SendLobbiesAsync()
        {
            await this.Clients.Caller.PullLobbies(await this.lobbiesService.GetLobbiesAsync());
            await this.Clients.Caller.Prout("prout ptin");
        }



        //public async Task BroadcastLobbiesAsync()
        //{
        //    var lobbies = await this.lobbiesService.GetLobbiesAsync();
        //    await this.Clients.All.PullLobbies(lobbies);
        //}

    }
}
