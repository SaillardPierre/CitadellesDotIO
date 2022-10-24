using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitadellesDotIO.Engine
{
    public class GameParameters
    {
        public string DistrictsDeckName { get; set; }
        public string CharactersListName { get; set; }
        public int DistrictThreshold { get; set; }
        public bool ApplyKingShuffleRule { get; set; }
    }
}
