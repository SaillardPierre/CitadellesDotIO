using CitadellesDotIO.Enums;
using CitadellesDotIO.Model.Districts;

namespace CitadellesDotIO.Model.Factories
{
    public static class DistrictsFactory
    {
        public static District ToHollow(District oldDistrict, DistrictType newType)
        {
            return new HollowDistrict()
            {
                Name = oldDistrict.Name,
                BuildingCost = oldDistrict.BuildingCost,
                IsBuilt = oldDistrict.IsBuilt,
                Owner = oldDistrict.Owner,
                DistrictType = newType,
                Spell = null
            };
        }
    }
}
