namespace CitadellesDotIO.Engine.Districts
{
    public sealed class Dungeon : PrestigeDistrict
    {
        public Dungeon()
        {
            this.Name = "Dungeon";
            this.BuildingCost = 3;
        }

        public override bool CanBeDestroyed => false;
    }
}
