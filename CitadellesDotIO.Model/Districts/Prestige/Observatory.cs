using CitadellesDotIO.Model.Passives;

namespace CitadellesDotIO.Model.Districts
{
    public class Observatory : PrestigeDistrict
    {
        public Observatory()
        {
            this.Name = "Observatory";
            this.BuildingCost = 5;
            this.Passive = new IncreasePoolSize(this.Owner, 1);
        }
    }
}
