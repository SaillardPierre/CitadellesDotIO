using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitadellesDotIO.Model.Districts
{
    public class Castle : NobleDistrict
    {
        public Castle()
        {
            this.Name = "Castle";
            this.BuildingCost = 4;
        }
    }
}
