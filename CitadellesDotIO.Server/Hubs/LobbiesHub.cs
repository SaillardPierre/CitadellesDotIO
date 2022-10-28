using CitadellesDotIO.Engine;
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
            if (await this.lobbiesService.RegisterPlayerAsync(player))
            {
                await this.BroadcastPlayersAsync();
            }
            else await Task.FromException(new Exception("Failure in Register Player"));
        }

        public async Task StartGameAsync(string lobbyId)
        {
            await this.lobbiesService.CreateGameAsync(lobbyId);
        }

        //private async Task BroadcastGameAsync(string lobbyId)
        //{
        //    await this.Clients.Group(lobbyId);
        //}

        public async Task CreateLobbyAsync(string newLobbyName)
        {
            Lobby newLobby = new(newLobbyName);
            if (await this.lobbiesService.CreateLobbyAsync(newLobby))
            {
                await this.JoinLobbyAsync(newLobby.Id, this.Context.ConnectionId);
            }
            else await Task.FromException(new Exception("Failure in Create lobby"));
        }

        public async Task JoinLobbyAsync(string lobbyId, string playerId)
        {
            if (await this.lobbiesService.AddPlayerToLobby(lobbyId, playerId))
            {
                await this.Groups.AddToGroupAsync(this.Context.ConnectionId, lobbyId);                
                await this.Clients.Caller.PullLobbyId(lobbyId);
                await this.BroadcastLobbiesAsync();
                await this.BroadcastPlayersAsync();
            }
        }

        public async Task LeaveLobbyAsync(string lobbyId, string playerId)
        {
            if (await this.lobbiesService.RemovePlayerFromLobby(lobbyId, playerId))
            {
                await this.Groups.RemoveFromGroupAsync(this.Context.ConnectionId, lobbyId);
                await this.Clients.Caller.PullLobbyId(string.Empty);
                await this.BroadcastLobbiesAsync();
                await this.BroadcastPlayersAsync();
            }
        }

        public async Task UnregisterPlayerAsync(string playerId)
        {
            if (await this.lobbiesService.RemovePlayerFromPlayers(playerId))
            {
                await this.BroadcastPlayersAsync();
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
