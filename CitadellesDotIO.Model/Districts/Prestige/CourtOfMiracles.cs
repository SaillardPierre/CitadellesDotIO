using CitadellesDotIO.Model.Spells;

namespace CitadellesDotIO.Model.Districts
{
    public class CourtOfMiracles : PrestigeDistrict
    {
        public CourtOfMiracles()
        {
            this.Name = "Court of miracles";
            this.BuildingCost = 2;
            this.Spell = new ColorShift();
        }
        // TODO Gérer l'histoire de pas utilisable pendant ce tour de jeu
    }
}
