using CitadellesDotIO.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitadellesDotIO.Engine.Passives
{
    public abstract class Passive
    {
        public Player Player { get; set; }
        public abstract void Apply();
    }
}
