using CitadellesDotIO.Enums;
using CitadellesDotIO.Model.Districts;
using System.Collections.Generic;

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

        public static List<District> TestDistrictList(int size)
        {
            List<District> districtList = new List<District>();
            for(int i=0; i<size;i++)
            {
                districtList.Add(new TestDistrict());
            }
            return districtList;
        }
    
    }
}
