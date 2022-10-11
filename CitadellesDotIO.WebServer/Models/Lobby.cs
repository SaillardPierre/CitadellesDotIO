using CitadellesDotIO.Model;

namespace CitadellesDotIO.WebServer.Models
{
    public class Lobby
    {
        public string Name { get; set; }
        public List<Player>? Players { get; set; }
        
        public Lobby(string name)
        {
            this.Name = name;
            this.Players = new List<Player>();
        }
    }
}
