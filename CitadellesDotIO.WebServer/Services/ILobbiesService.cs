using CitadellesDotIO.WebServer.Models;

namespace CitadellesDotIO.WebServer.Services
{
    public interface ILobbiesService
    {
        public IList<Lobby> GetLobbies();
        public bool CreateLobby(Lobby newLobby);
    }
}
