using CitadellesDotIO.WebServer.Models;

namespace CitadellesDotIO.WebServer
{
    public class LobbiesService : ILobbiesService
    {
        private List<Lobby> Lobbies { get; set; }
        public LobbiesService()
        {
            this.Lobbies = new List<Lobby>() { new Lobby(Guid.NewGuid().ToString()) };
        }

        public IList<Lobby> GetLobbies()
            => this.Lobbies;

        public bool CreateLobby(Lobby newLobby)
        {
            this.Lobbies.Add(newLobby);
            return true;
        }
    }
}
