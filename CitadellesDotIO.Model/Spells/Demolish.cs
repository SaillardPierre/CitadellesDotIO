using CitadellesDotIO.Exceptions;
using CitadellesDotIO.Model.Characters;
using CitadellesDotIO.Model.Districts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CitadellesDotIO.Model.Spells
{

    public class Demolish : Spell
    {
        public override Type TargetType => typeof(District);

        public Demolish(Player caster)
        {
            this.Caster = caster;
            this.Targets = new List<ITarget>();
        }

        public override void Cast(ITarget target)
        {
            if (target is District district)
            {
                district.IsBuilt = false;
            }
            else throw new SpellTargetException("La cible à détruire n'est pas un quartier");
        }

        public override void GetAvailableTargets(List<ITarget> targets)
        {
            base.GetAvailableTargets(targets);
            // On écarte les cibles qui sont :
            // des districts et
            // qui appartiennent à l'eveque ou
            // qui ne peuvent être détruits ou
            // qui sont trop chers à détruire
            this.Targets.RemoveAll(t =>
                t is District district &&
                (district.Owner.Character.Name.Equals(nameof(Bishop)) ||
                !district.CanBeDestroyed ||
                district.DestructionCost > this.Caster.Gold));
        }
    }
}
