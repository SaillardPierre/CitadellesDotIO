using CitadellesDotIO.Engine.Targets;
using CitadellesDotIO.Exceptions;
using CitadellesDotIO.Engine.Characters;
using CitadellesDotIO.Engine.Districts;
using System.Collections.Generic;

namespace CitadellesDotIO.Engine.Spells
{

    public class Demolish : DistrictTargetSpell
    {
        public Demolish(Player caster)
        {
            Caster = caster;
        }

        public override void Cast(ITarget target)
        {
            base.Cast(target);
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
            // qui sont trop chers à détruire ou
            // qui appartiennent à une cité terminée
            targets.RemoveAll(t =>
                t is District district &&
                district.Owner != null &&
                (district.Owner.Character.Name.Equals(nameof(Bishop)) ||
                !district.CanBeDestroyed ||
                district.DestructionCost > Caster.Gold ||
                district.Owner.HasReachedDistrictThreshold));
            Targets = targets;
        }
    }
}
