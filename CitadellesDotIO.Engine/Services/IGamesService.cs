using CitadellesDotIO.Engine.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CitadellesDotIO.Engine.Services
{
    public interface IGamesService
    {
        public Task<IEnumerable<GameDto>> GetGames();
        public Task<string> CreateGameAsync(string gameName, string playerName);
        public Task<bool> AddPlayerToGameAsync(string gameId, string playerName);
        public bool SetPlayerConnection(string gameId, string playerName, string connectionId);
    }
}
