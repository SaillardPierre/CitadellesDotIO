using System;
using System.Collections.Generic;
using System.Text;
using CitadellesDotIO.Enums;

namespace CitadellesDotIO.Model.Characters
{
    public class Bishop : Character
    {
        public Bishop(int order) : base(order)
        {
        }

        public new DistrictType? AssociatedDistrictType => DistrictType.Religious;
    }
}
