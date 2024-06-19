using CitadellesDotIO.Engine.DTOs;
using CitadellesDotIO.Engine.Spells;
using CitadellesDotIO.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CitadellesDotIO.Engine.Characters
{
    public sealed class Wizard : Character
    {
        public Wizard(int order) : base(order)
        {
            this.Spell = new Swap(this.Player);
        }

        public override DistrictType? AssociatedDistrictType => null;
        public override Spell Spell { get; set; }

        public override CharacterDto ToCharacterDto()
        {
            return new CharacterDto(Order, nameof(Wizard), this.AssociatedDistrictType, new SpellDto()
            {
                Description = "Choose a Player District Deck to switch yours with or discard all your Districts and pick the same number from the table deck"
            });            
        }
    }
}
