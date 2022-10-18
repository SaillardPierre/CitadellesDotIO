using CitadellesDotIO.Engine.Targets;
using CitadellesDotIO.Engine.Districts;
using System;

namespace CitadellesDotIO.Engine.Spells
{
    public class Revive : Spell
    {
        public override Type TargetType => typeof(District);

        public override void Cast(ITarget target)
        {
            throw new NotImplementedException();
        }
    }
}
