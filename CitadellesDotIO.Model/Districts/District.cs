using CitadellesDotIO.Enums;

namespace CitadellesDotIO.Model.Districts
{
    public abstract class District
    {
        public string Name { get; set; }
        public int BuildingCost { get; set; }
        public int ScoreValue => this.BuildingCost;
        public int DestructionCost => this.BuildingCost - 1;
        public bool CanBeDestroyed => true;
        public DistrictType DistrictType { get; set; }
    }
}