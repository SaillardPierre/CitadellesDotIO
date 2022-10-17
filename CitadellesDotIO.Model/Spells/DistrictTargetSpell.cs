using CitadellesDotIO.Model.Districts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitadellesDotIO.Model.Spells
{
    public abstract class DistrictTargetSpell : Spell
    {
        public override Type TargetType => typeof(District);
    }
}
