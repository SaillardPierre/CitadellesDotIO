using CitadellesDotIO.Engine;

namespace CitadellesDotIO.Server.Data
{
    public class GameDto
    {
        public string GameId { get;set; }
        public List<Player> Players { get; set; }
    }
}
