using CitadellesDotIO.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CitadellesDotIO.Model.Characters
{
    public class Wizard : Character
    {
        public Wizard(int order) : base(order)
        {
        }

        public override DistrictType? AssociatedDistrictType => null;

        public override Spell Spell => new Spell();
    }
}
