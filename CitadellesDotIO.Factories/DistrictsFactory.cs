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
        public static List<District> TestDistrictList(int listSize)
        {
            List<District> districts = new List<District>();
            for (int i = 0; i < listSize; i++)
            {
                districts.Add(new TestDistrict());
            }
            return districts;
        }
    }
}
