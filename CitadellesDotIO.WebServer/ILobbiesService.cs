using CitadellesDotIO.WebServer.Models;

namespace CitadellesDotIO.WebServer
{
    public interface ILobbiesService 
    {
        public IList<Lobby> GetLobbies();
        public bool CreateLobby(Lobby newLobbyName);
    }
}
