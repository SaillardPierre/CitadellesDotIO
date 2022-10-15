using CitadellesDotIO.Exceptions;
using CitadellesDotIO.Model.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CitadellesDotIO.Model.Spells
{

    public class Murder: Spell
    {
        public override Type TargetType => typeof(Character);

        public Murder(Player player)
        {
            this.Caster = player;
        }

        public override void Cast(ITarget target)
        {
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
