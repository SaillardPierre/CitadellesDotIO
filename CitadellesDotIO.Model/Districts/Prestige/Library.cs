using CitadellesDotIO.Model.Passives;

namespace CitadellesDotIO.Model.Districts
{
    public class Library : PrestigeDistrict
    {
        public Library()
        {
            this.Name = "Library";
            this.BuildingCost = 6;
            this.Passive = new IncreasePickSize(this.Owner, 1);
        }
    }
}
