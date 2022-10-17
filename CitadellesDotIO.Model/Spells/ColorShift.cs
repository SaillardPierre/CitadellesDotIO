using CitadellesDotIO.Enums;
using CitadellesDotIO.Model.Districts;
using System.Collections.Generic;
using CitadellesDotIO.Model.Factories;
using CitadellesDotIO.Model.Targets;

namespace CitadellesDotIO.Model.Spells
{
    public class ColorShift : DistrictTargetSpell
    {
        public District SpellSource { get; set; }
        public ColorShift(District district)
        {
            this.SpellSource = district;
        }
        public override void Cast(ITarget target)
        {
            base.Cast(target);
            this.SpellSource = target as District;
        }
        public override void GetAvailableTargets()
        {
            if (this.Caster.HasReachedDistrictThreshold)
            {
                this.Targets = new List<ITarget>
                {
                    DistrictsFactory.ToHollow(this.SpellSource, DistrictType.Religious),
                    DistrictsFactory.ToHollow(this.SpellSource, DistrictType.Noble),
                    DistrictsFactory.ToHollow(this.SpellSource, DistrictType.Trading),
                    DistrictsFactory.ToHollow(this.SpellSource, DistrictType.Warfare)
                };
            }
        }
    }
}
