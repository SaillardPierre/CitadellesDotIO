using CitadellesDotIO.Model.Districts;
using CitadellesDotIO.Model.Targets;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CitadellesDotIO.Model.Spells
{
    public class Discard : TableDeckTargetSpell
    {
        public override bool HasToPickTargets => true;
        public Discard(Player player)
        {
            this.Caster = player;
        }
        public override void Cast(ITarget target)
        {
            base.Cast(target);
            District toDiscard = target as District;
            toDiscard.Reset();
            this.Caster.DistrictsDeck.Remove(toDiscard);
            this.TableDeck.Enqueue(toDiscard);
            this.Caster.Gold += 2;            
        }

        public override void GetAvailableTargets(List<ITarget> targets)
        {
            base.GetAvailableTargets(targets);
            this.Targets = new List<ITarget>(this.Caster.DistrictsDeck);
        }
    }
}
