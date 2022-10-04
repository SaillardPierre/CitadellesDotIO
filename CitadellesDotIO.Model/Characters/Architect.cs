using CitadellesDotIO.Enums;

namespace CitadellesDotIO.Model.Characters
{
    public class Architect : Character
    {
        public Architect(int order) : base(order)
        {
        }

        public override DistrictType? AssociatedDistrictType => null;

        public override Spell Spell => new Spell();
    }
}
