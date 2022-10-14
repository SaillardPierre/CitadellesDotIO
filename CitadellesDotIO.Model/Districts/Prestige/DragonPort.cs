namespace CitadellesDotIO.Model.Districts
{
    public class DragonPort : PrestigeDistrict
    {
        public DragonPort()
        {
            this.Name = "DragonPort";
            this.BuildingCost = 6;
        }

        public override int ScoreValue => 8; 
    }
}
