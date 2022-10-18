using CitadellesDotIO.Engine.Targets;
using CitadellesDotIO.Exceptions;
using CitadellesDotIO.Engine.Characters;
using System.Collections.Generic;

namespace CitadellesDotIO.Engine.Spells
{
    public class Steal : CharacterTargetSpell
    {
        public Steal(Player player)
        {
            Caster = player;
        }
        public override void Cast(ITarget target)
        {
            base.Cast(target);
            if (target is Character character)
            { character.IsStolen = true; }
            else throw new SpellTargetException("La cible à voler n'est pas un personnage");
        }

        public override void GetAvailableTargets(List<ITarget> targets)
        {
            base.GetAvailableTargets(targets);
            targets.RemoveAll(t => t is Thief || t is Assassin || t is Character c && c.IsMurdered);
            Targets = targets;
        }
    }
}
