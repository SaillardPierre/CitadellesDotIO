using CitadellesDotIO.Engine.Passives;

namespace CitadellesDotIO.Engine.Districts
{
    public sealed class Library : PrestigeDistrict
    {
        public Library()
        {
            this.Name = "Library";
            this.BuildingCost = 6;
            this.Passive = new IncreasePickSize(this.Owner, 1);
        }
    }
}
