using CitadellesDotIO.Engine.DTOs;
using CitadellesDotIO.Engine.Spells;
using CitadellesDotIO.Enums;

namespace CitadellesDotIO.Engine.Characters
{
    public sealed class Thief : Character
    {        
        public Thief(int order) : base(order)
        {
            this.Spell = new Steal(this.Player);
        }

        #pragma warning disable CA1822 // Marquer les membres comme étant static
        public new bool IsStolen => false;
        #pragma warning restore CA1822 // Marquer les membres comme étant static        

        public override DistrictType? AssociatedDistrictType => null;

        public override Spell Spell { get; set; }

        public override CharacterDto ToCharacterDto()
        {
            return new CharacterDto(this.Order, nameof(Thief), this.AssociatedDistrictType, new SpellDto()
            {
                Description = "Select a Character's pile of Gold to steal at the start of his turn"
            });
        }
    }
}
