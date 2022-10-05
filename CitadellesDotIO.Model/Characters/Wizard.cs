using CitadellesDotIO.Enums;
using CitadellesDotIO.Model.Spells;
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

        public override ISpell<ITarget> Spell => new Swap<ITarget>(this.Player);
    }
}
