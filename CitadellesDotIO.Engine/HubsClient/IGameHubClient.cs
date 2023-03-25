using CitadellesDotIO.Engine.DTOs;
using CitadellesDotIO.Enums;
using System.Threading.Tasks;

namespace CitadellesDotIO.Engine.HubsClient
{
    public interface IGameHubClient
    {
        public Task<bool> RegisterPlayer();
        public Task UpdateGame(GameDto game);
    }
}