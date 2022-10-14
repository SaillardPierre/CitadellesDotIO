using CitadellesDotIO.Model.Spells;

namespace CitadellesDotIO.Model.Districts
{
    public class Manufacture : PrestigeDistrict
    {
        public Manufacture()
        {
            this.Name = "Manufacture";
            this.BuildingCost = 5;
            this.Spell = new Craft();
        }
    }
}
