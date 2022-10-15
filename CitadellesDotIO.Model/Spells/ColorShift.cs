using CitadellesDotIO.Enums;
using CitadellesDotIO.Model.Districts;
using System.Collections.Generic;
using CitadellesDotIO.Model.Factories;

namespace CitadellesDotIO.Model.Spells
{
    public class ColorShift : Spell
    {
        public District SpellSource { get; set; }
        public ColorShift(District district)
        {
            this.SpellSource = district;
        }

        public override void Cast(ITarget target)
        {
            this.SpellSource = target as District;
        }
        public override void GetAvailableTargets()
        {
            if (this.Caster.HasReachedDistrictThreshold)
            {
                this.Targets = new List<ITarget>
                {
                    DistrictsFactory.ToHollow(this.SpellSource, DistrictType.Religious) as ITarget,
                    DistrictsFactory.ToHollow(this.SpellSource,DistrictType.Noble) as ITarget,
                    DistrictsFactory.ToHollow(this.SpellSource,DistrictType.Trading) as ITarget,
                    DistrictsFactory.ToHollow(this.SpellSource,DistrictType.Warfare) as ITarget
                };
            }
        }
    }
}
