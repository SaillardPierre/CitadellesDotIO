using CitadellesDotIO.Enums;
using CitadellesDotIO.Engine.DTOs;

namespace CitadellesDotIO.Engine.Characters
{
    public sealed class Merchant : Character
    {
        public Merchant(int order) : base(order)
        {
        }
        public override DistrictType? AssociatedDistrictType => DistrictType.Trading;

        public override CharacterDto ToCharacterDto()
        {
            return new CharacterDto(this.Order, nameof(Merchant), this.AssociatedDistrictType, passive: new PassiveDto()
            {
                Description = "Yields one Bonus Gold"
            });
        }
    }
}
