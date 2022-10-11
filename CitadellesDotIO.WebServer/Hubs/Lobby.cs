using CitadellesDotIO.Model;

namespace CitadellesDotIO.WebServer.Hubs
{
    public class Lobby
    {
        public string Name { get; set; }
        public List<Player>? Players { get; set; }
    }
}
