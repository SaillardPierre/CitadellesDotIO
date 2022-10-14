namespace CitadellesDotIO.Model.Districts
{
    public class University : PrestigeDistrict
    {
        public University()
        {
            this.Name = "University";
            this.BuildingCost = 6; 
        }

        public override int ScoreValue => 8;  
    }
}
