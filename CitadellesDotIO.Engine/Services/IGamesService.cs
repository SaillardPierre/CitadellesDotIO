using CitadellesDotIO.Engine.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CitadellesDotIO.Engine.Services
{
    public interface IGamesService
    {
        public Task<IEnumerable<GameDto>> GetGames();
        public string CreateGame(string gameName, string playerName);
        public bool AddPlayerToGame(string gameId, string playerName);
        public bool SetPlayerConnection(string gameId, string playerName, string connectionId);
    }
}
