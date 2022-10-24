using System;
using System.Collections.Generic;
using System.Text;
using CitadellesDotIO.Enums;
using CitadellesDotIO.Engine.Spells;

namespace CitadellesDotIO.Engine.Characters
{
    public sealed class Merchant : Character
    {
        public Merchant() : base() { }
        public Merchant(int order) : base(order)
        {
        }
        public override DistrictType? AssociatedDistrictType => DistrictType.Trading; 
    }
}
