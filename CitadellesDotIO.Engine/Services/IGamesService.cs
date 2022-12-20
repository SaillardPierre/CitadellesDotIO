using System.Threading.Tasks;

namespace CitadellesDotIO.Engine.Services
{
    public interface IGamesService
    {
        public Task<string> CreateGameAsync(string gameName, string playerName);
        public Task<bool> AddPlayerToGameAsync(string gameId, string playerName);
    }
}
