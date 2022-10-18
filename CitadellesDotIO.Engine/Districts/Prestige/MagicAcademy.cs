using CitadellesDotIO.Enums;

namespace CitadellesDotIO.Engine.Districts
{
    public class MagicAcademy : PrestigeDistrict
    {
        public MagicAcademy()
        {
            this.Name = "Magic Academy";
            this.BuildingCost = 6;
        }

        public override DistrictType DistrictType
        {
            get
            {
                if (this.Owner != null &&
                    this.Owner.Character != null &&
                    this.Owner.Character.HasAssociatedDistrictType)
                {
                    return this.Owner.Character.AssociatedDistrictType.Value;
                }
                else return base.DistrictType;
            }
        }

    }
}
