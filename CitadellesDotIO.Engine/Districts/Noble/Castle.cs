using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitadellesDotIO.Engine.Districts
{
    public sealed class Castle : NobleDistrict
    {
        public Castle()
        {
            this.Name = "Castle";
            this.BuildingCost = 4;
        }
    }
}
