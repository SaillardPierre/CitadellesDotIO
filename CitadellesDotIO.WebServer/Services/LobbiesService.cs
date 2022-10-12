using CitadellesDotIO.WebServer.Models;

namespace CitadellesDotIO.WebServer.Services
{
    public class LobbiesService : ILobbiesService
    {
        private List<Lobby> Lobbies { get; set; }
        public LobbiesService()
        {
            Lobbies = new List<Lobby>() { new Lobby(Guid.NewGuid().ToString()) };
        }

        public IList<Lobby> GetLobbies()
            => Lobbies;

        public bool CreateLobby(Lobby newLobby)
        {
            Lobbies.Add(newLobby);
            return true;
        }
    }
}
