using CitadellesDotIO.Model.Districts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitadellesDotIO.Model.Spells
{
    public class Discard : Spell
    {
        public override Type TargetType => typeof(District);

        public override void Cast(ITarget target)
        {
            throw new NotImplementedException();
        }
    }
}
