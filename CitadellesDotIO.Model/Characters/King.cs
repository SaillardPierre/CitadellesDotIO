using System;
using System.Collections.Generic;
using System.Text;
using CitadellesDotIO.Enums;
using CitadellesDotIO.Model.Spells;

namespace CitadellesDotIO.Model.Characters
{
    public class King : Character
    {
        public King(int order) : base(order)
        {
        }
        public override DistrictType? AssociatedDistrictType => DistrictType.Noble;
        public override Spell Spell { get => null; set { value = null; } }
    }
}
