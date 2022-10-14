using CitadellesDotIO.Enums;

namespace CitadellesDotIO.Model.Districts
{
    public abstract class WarfareDistrict : District
    {
        public override DistrictType DistrictType => DistrictType.Warfare;
    }
}
