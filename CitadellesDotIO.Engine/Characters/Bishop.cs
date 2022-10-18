using CitadellesDotIO.Enums;

namespace CitadellesDotIO.Engine.Characters
{
    public class Bishop : Character
    {
        public Bishop() : base() { }
        public Bishop(int order) : base(order)
        {
        }
        public override DistrictType? AssociatedDistrictType => DistrictType.Religious;
    }
}
