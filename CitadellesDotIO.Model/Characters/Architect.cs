using CitadellesDotIO.Enums;
using CitadellesDotIO.Model.Passives;
using CitadellesDotIO.Model.Spells;

namespace CitadellesDotIO.Model.Characters
{
    public class Architect : Character
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
