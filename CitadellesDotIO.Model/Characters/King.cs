﻿using System;
using System.Collections.Generic;
using System.Text;
using CitadellesDotIO.Enums;
using CitadellesDotIO.Model.Spells;

namespace CitadellesDotIO.Model.Characters
{
    public class King : Character
    {
        public King() : base() { }
        public King(int order) : base(order)
        {
        }
        public override DistrictType? AssociatedDistrictType => DistrictType.Noble;
    }
}
