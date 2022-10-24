using CitadellesDotIO.Engine;
using CitadellesDotIO.Engine.Factories;

namespace CitadellesDotIO.Server.Models
{
    public class Lobby
    {
        public string Name { get; set; }
        public List<Player> Players { get; set; }

        //public GameParameters Parameters { get; set; }
        
        public Lobby(string name, GameParameters? parameters = null)
        {
            this.Name = name;
            this.Players = new List<Player>();
            //this.Parameters = parameters ?? new GameParameters()
            //{
            //    DistrictsDeckName = nameof(DeckFactory.VanillaDistrictsDeck),
            //    CharactersListName = nameof(CharactersFactory.VanillaCharactersList),
            //    DistrictThreshold = 7,
            //    ApplyKingShuffleRule = true
            //};
        }
    }
}
