using CitadellesDotIO.Exceptions;
using CitadellesDotIO.Model.Characters;
using CitadellesDotIO.Model.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CitadellesDotIO.Model.Spells
{
    public class Murder: CharacterTargetSpell
    {
        public Murder(Player player)
        {
            this.Caster = player;
        }

        public override void Cast(ITarget target)
        {
            base.Cast(target);
            if (target is Character character)
            {
                character.IsMurdered = true;
            }
            else throw new SpellTargetException("La cible à assassiner n'est pas un personnage");
        }

        public override void GetAvailableTargets(List<ITarget> targets)
        {
            base.GetAvailableTargets(targets);
            targets.RemoveAll(t => t is Assassin || (t is Character c &&  c.IsMurdered));
            this.Targets = targets;
        }
    }
}
