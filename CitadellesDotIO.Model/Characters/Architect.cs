using CitadellesDotIO.Enums;
using CitadellesDotIO.Model.Spells;

namespace CitadellesDotIO.Model.Characters
{
    public class Architect : Character
    {
        public Architect() : base() { }
        public Architect(int order) : base(order)
        {
        }
        public override DistrictType? AssociatedDistrictType => null;
    }
}
