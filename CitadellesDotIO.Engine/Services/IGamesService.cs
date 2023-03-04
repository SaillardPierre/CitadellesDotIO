using CitadellesDotIO.Engine.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CitadellesDotIO.Engine.Services
{
    public interface IGamesService
    {
        public IEnumerable<GameDto> GetGames();
        public string CreateGame(string gameName, string playerName);
        public Tuple<string, GameDto> GetGameWithSecret(string gameId);
        public bool TryAddPlayerToGame(string gameId, string playerName, out string gameSecret, bool IsHost = false);
        public bool AddPlayerToGameByGameName(string gameName, string playerName, out string gameId);
        public bool TrySetPlayerConnection(string gameId, string playerName, string connectionId, out GameDto gameDto);
        public bool TrySetReadyState(string gameId, string connectionId, bool isReady, out GameDto gameDto);
    }
}
