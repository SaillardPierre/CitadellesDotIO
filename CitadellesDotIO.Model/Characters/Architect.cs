using CitadellesDotIO.Enums;
using CitadellesDotIO.Model.Spells;

namespace CitadellesDotIO.Model.Characters
{
    public class Architect : Character
    {
        public Architect(int order) : base(order)
        {
        }
        public override DistrictType? AssociatedDistrictType => null;

        public override Spell Spell { get => null; set { value = null; } }
    }
}
