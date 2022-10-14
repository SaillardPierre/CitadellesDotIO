using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitadellesDotIO.Model.Spells
{
    public class Craft : Spell
    {
        public override Type TargetType => null;

        public override void Cast(ITarget target)
        {
            throw new NotImplementedException();
        }
    }
}
