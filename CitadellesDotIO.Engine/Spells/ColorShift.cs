using CitadellesDotIO.Enums;
using CitadellesDotIO.Engine.Districts;
using System.Collections.Generic;
using CitadellesDotIO.Engine.Factories;
using CitadellesDotIO.Engine.Targets;

namespace CitadellesDotIO.Engine.Spells
{
    public class ColorShift : DistrictTargetSpell
    {
        public District SpellSource { get; set; }
        public ColorShift(District district)
        {
            SpellSource = district;
        }
        public override void Cast(ITarget target)
        {
            base.Cast(target);
            SpellSource = target as District;
        }
        public override void GetAvailableTargets()
        {
            if (Caster.HasReachedDistrictThreshold)
            {
                Targets = new List<ITarget>
                {
                    DistrictsFactory.ToHollow(SpellSource, DistrictType.Religious),
                    DistrictsFactory.ToHollow(SpellSource, DistrictType.Noble),
                    DistrictsFactory.ToHollow(SpellSource, DistrictType.Trading),
                    DistrictsFactory.ToHollow(SpellSource, DistrictType.Warfare)
                };
            }
        }
    }
}
