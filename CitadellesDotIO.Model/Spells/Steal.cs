using CitadellesDotIO.Exceptions;
using CitadellesDotIO.Model.Characters;
using System;
using System.Collections.Generic;
using System.Text;

namespace CitadellesDotIO.Model.Spells
{
    public class Steal : Spell
    {
        public override Type TargetType => typeof(Character);

        public Steal(Player player)
        {
            this.Caster = player;
            this.Targets = new List<ITarget>();
        }

        public override void Cast(ITarget target)
        {
            if (target is Character character)
            { character.IsStolen = true; }
            else throw new SpellTargetException("La cible à voler n'est pas un personnage");

        }

        public override void GetAvailableTargets(List<ITarget> targets)
        {
            base.GetAvailableTargets(targets);
            this.Targets.RemoveAll(t => t is Thief || t is Assassin || (t is Character c && c.IsMurdered));
        }
    }
}
