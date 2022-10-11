using CitadellesDotIO.Model;

namespace CitadellesDotIO.Hubs
{
    public class Lobby
    {
        public Lobby(string name)
        {
            Name = name;
            this.players = new List<Player>();
        }

        public string Name { get; set; }
        public List<Player> players { get; set; }
    }
}
