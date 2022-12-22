using System.Threading.Tasks;

namespace CitadellesDotIO.Engine.HubsClient
{
    public interface IGameHubClient
    {
        public Task RegisterPlayer();

        public Task SendTest(string message);
    }
}