﻿using CitadellesDotIO.Engine.Spells;

namespace CitadellesDotIO.Engine.Districts
{
    public sealed class CourtOfMiracles : PrestigeDistrict
    {
        public CourtOfMiracles()
        {
            this.Name = "Court of miracles";
            this.BuildingCost = 2;
            this.Spell = new ColorShift(this);
        }
    }
}
