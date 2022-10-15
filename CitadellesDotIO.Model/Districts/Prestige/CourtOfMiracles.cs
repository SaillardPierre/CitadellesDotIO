using CitadellesDotIO.Model.Spells;

namespace CitadellesDotIO.Model.Districts
{
    public class CourtOfMiracles : PrestigeDistrict
    {
        public CourtOfMiracles()
        {
            this.Name = "Court of miracles";
            this.BuildingCost = 2;
            this.Spell = new ColorShift(this);
        }
    }
}
