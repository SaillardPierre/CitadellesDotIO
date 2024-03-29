﻿using System;
using System.Collections.Generic;
using System.Text;
using CitadellesDotIO.Enums;
using CitadellesDotIO.Engine.Spells;

namespace CitadellesDotIO.Engine.Characters
{
    public sealed class King : Character
    {
        public King() : base() { }
        public King(int order) : base(order)
        {
        }
        public override DistrictType? AssociatedDistrictType => DistrictType.Noble;
    }
}
