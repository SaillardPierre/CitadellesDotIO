using CitadellesDotIO.Enums;
using CitadellesDotIO.Engine.DTOs;

namespace CitadellesDotIO.Engine.Characters
{
    public sealed class King : Character
    {
        public King(int order) : base(order)
        {
        }
        public override DistrictType? AssociatedDistrictType => DistrictType.Noble;

        public override CharacterDto ToCharacterDto()
        {
            return new CharacterDto(this.Order, nameof(King), this.AssociatedDistrictType, passive: new PassiveDto()
            {
                Description = "If he lives, Player gets the crown"
            });

        }
    }
}
