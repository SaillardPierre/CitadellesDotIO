using CitadellesDotIO.Engine;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CitadellesDotIO.Engine.Services
{
    public interface ILobbiesService
    {
        public Task<List<Lobby>> GetLobbiesAsync();
        public Task<List<Player>> GetPlayersAsync();
        public Task<bool> CreateLobbyAsync(Lobby newLobby);
        public Task<bool> AddPlayerToLobby(string lobbyId, string playerId);
        public Task<bool> RemovePlayerFromLobby(string lobbyId, string playerId);
        public Task<bool> RemovePlayerFromPlayers(string playerId);
        public Task<bool> RegisterPlayerAsync(Player player);
        public Task CreateGameAsync(string lobbyId);
    }
}
