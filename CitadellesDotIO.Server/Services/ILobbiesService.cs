using CitadellesDotIO.Server.Models;

namespace CitadellesDotIO.Server.Services
{
    public interface ILobbiesService
    {
        public Task<List<Lobby>> GetLobbiesAsync();
        public Task<bool> CreateLobbyAsync(Lobby newLobby);
    }
}
