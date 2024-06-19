using CitadellesDotIO.Engine.DTOs;
using CitadellesDotIO.Enums;

namespace CitadellesDotIO.Engine.Characters
{
    public sealed class Bishop : Character
    {
        public Bishop(int order) : base(order)
        {
        }
        public override DistrictType? AssociatedDistrictType => DistrictType.Religious;

        public override CharacterDto ToCharacterDto()
        {
            return new CharacterDto(this.Order, nameof(Bishop), this.AssociatedDistrictType);
        }
    }
}
