﻿using CitadellesDotIO.Engine.Spells;

namespace CitadellesDotIO.Engine.Districts
{
    public sealed class Manufacture : PrestigeDistrict
    {
        public Manufacture()
        {
            this.Name = "Manufacture";
            this.BuildingCost = 5;
            this.Spell = new Craft(this.Owner);
        }
    }
}
