using CitadellesDotIO.Engine.Characters;
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
        public string Name { get; set; }
        public ReadOnlyCollection<string> Players { get; set; }
        public ReadOnlyCollection<CharacterDto> Characters { get; set; }
    }
}
