using CitadellesDotIO.Engine.Districts;
using System;

namespace CitadellesDotIO.Engine.Spells
{
    public abstract class DistrictTargetSpell : Spell
    {
        public override Type TargetType => typeof(District);
    }
}
