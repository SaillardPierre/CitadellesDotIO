using System;
using System.Collections.Generic;
using System.Text;
using CitadellesDotIO.Enums;
using CitadellesDotIO.Model.Spells;

namespace CitadellesDotIO.Model.Characters
{
    public class Bishop : Character
    {
        public Bishop(int order) : base(order)
        {
        }
        public override DistrictType? AssociatedDistrictType => DistrictType.Religious;
        public override ISpell<ITarget> Spell => null;
    }
}
