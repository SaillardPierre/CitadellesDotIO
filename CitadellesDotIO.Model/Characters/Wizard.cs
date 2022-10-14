using CitadellesDotIO.Enums;
using CitadellesDotIO.Model.Spells;
using System;
using System.Collections.Generic;
using System.Text;

namespace CitadellesDotIO.Model.Characters
{
    public class Wizard : Character
    {
        public Wizard() : base()
        {
            this.Spell = new Swap(this.Player);
        }
        public Wizard(int order) : base(order)
        {
            this.Spell = new Swap(this.Player);
        }

        public override DistrictType? AssociatedDistrictType => null;
        public override Spell Spell { get; set; }
    }
}
