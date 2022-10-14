using CitadellesDotIO.Enums;
using CitadellesDotIO.Model.Districts;
using CitadellesDotIO.Model.Spells;
using System;

namespace CitadellesDotIO.Model.Characters
{
    public class Condottiere : Character
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
