﻿using CitadellesDotIO.Engine.Spells;
using CitadellesDotIO.Enums;

namespace CitadellesDotIO.Engine.Characters
{
    public sealed class Condottiere : Character
    {
        public Condottiere() : base() {
            this.Spell = new Demolish(this.Player);
        }
        public Condottiere(int order) : base(order)
        {
            this.Spell = new Demolish(this.Player);
        }
        public override DistrictType? AssociatedDistrictType => DistrictType.Warfare;
        public override Spell Spell { get; set; }
    }
}
