using System;
using System.Collections.Generic;
using System.Text;
using CitadellesDotIO.Enums;

namespace CitadellesDotIO.Model.Characters
{
    public class King : Character
    {
        public King(int order) : base(order)
        {
        }

        public new DistrictType? AssociatedDistrictType => DistrictType.Noble;
    }
}
