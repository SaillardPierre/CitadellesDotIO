using CitadellesDotIO.Engine.DTOs;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CitadellesDotIO.Engine.HubsClient
{
    public interface ILobbyHubClient
    {
        public Task PullGameId(string gameId);
        public Task ConfirmConnection();
        public Task PullGamesAsync(IEnumerable<GameDto> games);
        public Task GameNotFound();
    }
}
