using CitadellesDotIO.Enums;
using CitadellesDotIO.Model.Districts;
using CitadellesDotIO.Model.Spells;
using System;

namespace CitadellesDotIO.Model.Characters
{
    public class Condottiere : Character
    {
        public Condottiere(int order) : base(order)
        {
        }
        public override DistrictType? AssociatedDistrictType => DistrictType.Warfare;
        public override ISpell<ITarget> Spell => new Demolish<District>() as ISpell<ITarget>;
    }
}
