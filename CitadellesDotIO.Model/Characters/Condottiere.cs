using System;
using System.Collections.Generic;
using System.Text;
using CitadellesDotIO.Enums;

namespace CitadellesDotIO.Model.Characters
{
    public class Condottiere : Character
    {
        public Condottiere(int order) : base(order)
        {
        }

        public override DistrictType? AssociatedDistrictType => DistrictType.Warfare;

        public override Spell Spell => new Spell();
    }
}
