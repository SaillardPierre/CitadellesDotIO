using System;
using System.Collections.Generic;
using System.Text;
using CitadellesDotIO.Enums;

namespace CitadellesDotIO.Model.Characters
{
    public class Condottiere : Character
    {
        public Condottiere(int order) : base(order)
        {
        }

        public new DistrictType? AssociatedDistrictType => DistrictType.Warfare;
    }
}
