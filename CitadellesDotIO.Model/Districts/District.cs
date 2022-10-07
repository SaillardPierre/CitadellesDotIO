using CitadellesDotIO.Enums;

namespace CitadellesDotIO.Model.Districts
{
    public abstract class District : ITarget
    {
        public string Name { get; set; }
        public int BuildingCost { get; set; }
        public int ScoreValue => this.BuildingCost;
        public int DestructionCost => this.BuildingCost - 1;
        public bool CanBeDestroyed => IsBuilt && Owner != null;
        public bool IsBuilt { get; set; }
        public Player Owner { get; set; }
        public DistrictType DistrictType { get; set; }
    }
}