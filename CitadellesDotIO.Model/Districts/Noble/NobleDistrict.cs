using CitadellesDotIO.Enums;

namespace CitadellesDotIO.Model.Districts
{
    public abstract class NobleDistrict : District
    {
        public override DistrictType DistrictType => DistrictType.Noble;
    }
}
