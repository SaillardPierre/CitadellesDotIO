using CitadellesDotIO.Enums;

namespace CitadellesDotIO.Model.Districts
{
    public abstract class District
    {
        public int BuildingCost {get;set;}
        public int DestructionCost {get;set;}

        public DistrictType Type {get;set;}
    }
}