using CitadellesDotIO.Enums;

namespace CitadellesDotIO.Model.Districts
{
    public abstract class ReligiousDistrict : District
    {
        public override DistrictType DistrictType => DistrictType.Religious;
    }
}
