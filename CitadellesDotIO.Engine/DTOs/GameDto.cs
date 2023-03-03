using CitadellesDotIO.Engine.Characters;
using CitadellesDotIO.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitadellesDotIO.Engine.DTOs
{
    public class GameDto
    {
        public string Id { get; set; }
        public GameState GameState { get; set; }
        public string Name { get; set; }
        // TODO : Remplacer par les Players DTO et gérer isHost d'ici 
        public ReadOnlyCollection<PlayerDto> Players { get; set; }
        public ReadOnlyCollection<CharacterDto> Characters { get; set; }
    }
}
