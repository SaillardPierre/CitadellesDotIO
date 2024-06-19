using CitadellesDotIO.Enums;
using CitadellesDotIO.Engine.Passives;
using CitadellesDotIO.Engine.Spells;
using CitadellesDotIO.Engine.DTOs;

namespace CitadellesDotIO.Engine.Characters
{
    public sealed class Architect : Character
    {        
        public Architect(int order) : base(order)
        {
            this.Spell = new Draw(this.Player);
            this.Passive = new IncreaseTurnBuildingCap(this.Player, 2);
        }
        public override DistrictType? AssociatedDistrictType => null;

        public override CharacterDto ToCharacterDto()
        {
            return new CharacterDto(this.Order, nameof(Architect), this.AssociatedDistrictType, new SpellDto()
            {
                Description = "Pick Two District Cards"
            },
            new PassiveDto()
            {
                Description = "Increase Turn Building Cap by 2"
            });
        }
    }
}
