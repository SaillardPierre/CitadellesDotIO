﻿using CitadellesDotIO.Enums;
using CitadellesDotIO.Exceptions;
using CitadellesDotIO.Model.Spells;

namespace CitadellesDotIO.Model.Characters
{
    public class Thief : Character
    {        
        public Thief() : base()
        {
            this.Spell = new Murder(this.Player);
        }
        public Thief(int order) : base(order)
        {
            this.Spell = new Steal(this.Player);
        }

        #pragma warning disable CA1822 // Marquer les membres comme étant static
        public new bool IsStolen => false;
        #pragma warning restore CA1822 // Marquer les membres comme étant static        

        public override DistrictType? AssociatedDistrictType => null;

        public override Spell Spell { get; set; }

    }
}
