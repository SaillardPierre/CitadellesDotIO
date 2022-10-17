using CitadellesDotIO.Model.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitadellesDotIO.Model.Spells
{
    public abstract class CharacterTargetSpell : Spell
    {
        public override Type TargetType => typeof(Character);
        public override bool HasToPickTargets => true;
    }
}
