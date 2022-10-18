using CitadellesDotIO.Engine.Passives;

namespace CitadellesDotIO.Engine.Districts
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
