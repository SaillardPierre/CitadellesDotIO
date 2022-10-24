using CitadellesDotIO.Engine;
using CitadellesDotIO.Server.Data;

namespace CitadellesDotIO.Server.HubsClients
{
    public interface IGamesHubClient
    {
        public Task PullGames(List<GameDto> games);
        public Task CreateGameAsync(Game game);
    }
}
