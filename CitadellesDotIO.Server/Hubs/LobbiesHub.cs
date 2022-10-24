using CitadellesDotIO.Engine;
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

        public async Task RegisterPlayerAsync(Player player)
        {
            if(await this.lobbiesService.RegisterPlayerAsync(player))
            {
                await this.BroadcastPlayersAsync();
            }
            else await Task.FromException(new Exception("Failure in Register Player"));
        }

        public async Task CreateLobbyAsync(Lobby newLobby)
        {
            if (await this.lobbiesService.CreateLobbyAsync(newLobby))
            {                
                await this.Groups.AddToGroupAsync(this.Context.ConnectionId, newLobby.Name);
                await this.BroadcastLobbiesAsync();
            }
            else await Task.FromException(new Exception("Failure in Create lobby"));
        }

        public async Task JoinLobbyAsync(string lobbyId, string playerId)
        {
            if(await this.lobbiesService.AddPlayerToLobby(lobbyId, playerId))
            {
                await this.Groups.AddToGroupAsync(this.Context.ConnectionId, lobbyId);

                // Temporaire
                await this.Clients.Groups(lobbyId).PullLobbies(await this.lobbiesService.GetLobbiesAsync());
                // Dans la réalité aprés avoir mis à jour l'interface du joueur pour montrer son lobby,
                // Broadcast le lobby a tous les membres du groupe
            }
        }

        public async Task SendLobbiesAsync()
        => await this.Clients.Caller.PullLobbies(await this.lobbiesService.GetLobbiesAsync());

        public async Task SendPlayersAsync()
        => await this.Clients.Caller.PullPlayers(await this.lobbiesService.GetPlayersAsync());

        public async Task BroadcastLobbiesAsync()
        => await this.Clients.All.PullLobbies(await this.lobbiesService.GetLobbiesAsync());

        public async Task BroadcastPlayersAsync()
        => await this.Clients.All.PullPlayers(await this.lobbiesService.GetPlayersAsync());
        
    }
}
