using CitadellesDotIO.Enums;
using CitadellesDotIO.Model.Districts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitadellesDotIO.Factories
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
