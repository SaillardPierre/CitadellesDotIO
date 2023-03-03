using CitadellesDotIO.Engine.DTOs;
using CitadellesDotIO.Enums;
using System.Threading.Tasks;

namespace CitadellesDotIO.Engine.HubsClient
{
    public interface IGameHubClient
    {
        public Task RegisterPlayer();
        public Task UpdateGame(GameDto game);
    }
}