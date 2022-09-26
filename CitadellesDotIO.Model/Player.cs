using CitadellesDotIO.Model.Characters;
using CitadellesDotIO.Model.Districts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CitadellesDotIO.Model
{
    public class Player
    {
        public string Name { get; set; }
        public int Gold { get; set; }
        public Character Character { get; set; }
        public List<District> Districts { get; set; }        
    }
}
