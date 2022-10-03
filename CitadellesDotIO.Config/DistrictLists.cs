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

        public static Queue<District> TestDistrictList()
        {
            Queue<District> districts = new Queue<District>();
            for(int i=0; i < 50; i++)
            {
                districts.Enqueue(new TestDistrict());
            }
            return districts;
        }
    }
}
