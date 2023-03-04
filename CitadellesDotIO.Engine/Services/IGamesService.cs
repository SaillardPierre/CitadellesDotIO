using CitadellesDotIO.Engine.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CitadellesDotIO.Engine.Services
{
    public interface IGamesService
    {
        public IEnumerable<GameDto> GetGames();
        public string CreateGame(string gameName, string playerName);
        public bool AddPlayerToGame(string gameId, string playerName, bool IsHost = false);
        public bool AddPlayerToGameByGameName(string gameName, string playerName, out string gameId);
        public bool TrySetPlayerConnection(string gameId, string playerName, string connectionId, out GameDto gameDto);
    }
}
