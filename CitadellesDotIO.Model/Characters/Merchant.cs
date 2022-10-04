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

        public override DistrictType? AssociatedDistrictType => DistrictType.Trading;
        public override Spell Spell => null;
    }
}
