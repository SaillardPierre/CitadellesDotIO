using CitadellesDotIO.Model.Districts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CitadellesDotIO.Config
{
    public static class DistrictLists
    {
        // A peupler avec les district de la boite
        public static List<District> VanillaDistrictList => new List<District>();

        public static List<District> TestDistrictList()
        {
            List<District> districts = new List<District>();
            for(int i=0; i < 5000; i++)
            {
                districts.Add(new TestDistrict());
            }
            return districts;
        }
    }
}
