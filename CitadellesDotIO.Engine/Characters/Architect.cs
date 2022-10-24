using CitadellesDotIO.Enums;
using CitadellesDotIO.Engine.Passives;
using CitadellesDotIO.Engine.Spells;

namespace CitadellesDotIO.Engine.Characters
{
    public sealed class Architect : Character
    {
        public Architect() : base()
        {
            this.Spell = new Draw(this.Player);
            this.Passive = new IncreaseTurnBuildingCap(this.Player, 2);
        }
        public Architect(int order) : base(order)
        {
            this.Spell = new Draw(this.Player);
        }
        public override DistrictType? AssociatedDistrictType => null;
    }
}
