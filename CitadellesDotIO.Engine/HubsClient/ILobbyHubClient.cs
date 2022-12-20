using System.Threading.Tasks;

namespace CitadellesDotIO.Engine.HubsClient
{
    public interface ILobbyHubClient
    {
        public Task PullGameId(string gameId);

        public Task PullMsg(string msg);

        public Task ConnectToGameHub(string gameId);
    }
}
