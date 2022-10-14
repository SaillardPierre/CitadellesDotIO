using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitadellesDotIO.Model.Districts
{
    public class Cathedral : ReligiousDistrict
    {
        public Cathedral()
        {
            this.Name = "Cathedral";
            this.BuildingCost = 5;
        }
    }
}
