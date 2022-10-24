using CitadellesDotIO.Engine;
using CitadellesDotIO.Server.Models;
using System.Collections.Concurrent;

namespace CitadellesDotIO.Server.Services
{
    public class LobbiesService : ILobbiesService
    {
        private readonly ConcurrentDictionary<string, Lobby> Lobbies = new();
        public LobbiesService()
        {
            // population à la con pour l'instant
            Lobby dummy = new Lobby("Dummy 3 players lobby")
            {
                Players = new List<Player>() { new("Sam"), new("Clover"), new("Alex") }
            };
            this.Lobbies.AddOrUpdate(Guid.NewGuid().ToString(), dummy, (key, value) => value);
        }


        public async Task<List<Lobby>> GetLobbiesAsync()
            => await Task.FromResult(this.Lobbies.Values.ToList());

        public async Task<bool> CreateLobbyAsync(Lobby newLobby)
        {           
            this.Lobbies.AddOrUpdate(Guid.NewGuid().ToString(), newLobby, (key, value)=>value);
            return await Task.FromResult(true);
        }
    }
}
