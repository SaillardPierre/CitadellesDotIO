using CitadellesDotIO.Engine.Characters;
using System;

namespace CitadellesDotIO.Engine.Spells
{
    public abstract class CharacterTargetSpell : Spell
    {
        public override Type TargetType => typeof(Character);
        public override bool HasToPickTargets => true;
    }
}
