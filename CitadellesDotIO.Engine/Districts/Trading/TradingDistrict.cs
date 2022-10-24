using CitadellesDotIO.Enums;

namespace CitadellesDotIO.Engine.Districts
{
    public abstract class TradingDistrict : District
    {
        public override DistrictType DistrictType => DistrictType.Trading;
    }
}
