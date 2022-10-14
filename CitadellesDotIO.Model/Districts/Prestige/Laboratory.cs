using CitadellesDotIO.Model.Spells;

namespace CitadellesDotIO.Model.Districts
{
    public class Laboratory : PrestigeDistrict
    {
        public Laboratory()
        {
            this.Name = "Laboratory";
            this.BuildingCost = 5;
            this.Spell = new Discard(this.Owner);
        }
    }
}
