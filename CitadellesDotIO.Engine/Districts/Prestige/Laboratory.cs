using CitadellesDotIO.Engine.Spells;

namespace CitadellesDotIO.Engine.Districts
{
    public sealed class Laboratory : PrestigeDistrict
    {
        public Laboratory()
        {
            this.Name = "Laboratory";
            this.BuildingCost = 5;
            this.Spell = new Discard(this.Owner);
        }
    }
}
