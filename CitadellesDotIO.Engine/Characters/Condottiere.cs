using CitadellesDotIO.Engine.DTOs;
using CitadellesDotIO.Engine.Spells;
using CitadellesDotIO.Enums;

namespace CitadellesDotIO.Engine.Characters
{
    public sealed class Condottiere : Character
    {
        public Condottiere(int order) : base(order)
        {
            this.Spell = new Demolish(this.Player);
        }
        public override DistrictType? AssociatedDistrictType => DistrictType.Warfare;
        public override Spell Spell { get; set; }

        public override CharacterDto ToCharacterDto()
        {
            return new CharacterDto(this.Order, nameof(Condottiere), this.AssociatedDistrictType, new SpellDto()
            {
                Description = "Pay one Gold less that a target District's Construction Cost to demolish it"
            });
        }
    }
}
