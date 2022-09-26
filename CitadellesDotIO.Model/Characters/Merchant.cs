using System;
using System.Collections.Generic;
using System.Text;
using CitadellesDotIO.Enums;

namespace CitadellesDotIO.Model.Characters
{
    public class Merchant : Character
    {
        public Merchant(int order) : base(order)
        {
        }

        public new DistrictType? AssociatedDistrictType => DistrictType.Trading;
    }
}
