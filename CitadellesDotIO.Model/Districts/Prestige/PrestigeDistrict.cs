using CitadellesDotIO.Enums;

namespace CitadellesDotIO.Model.Districts
{
    public abstract class PrestigeDistrict : District
    {
        public override DistrictType DistrictType => DistrictType.Prestige;
    }
}
