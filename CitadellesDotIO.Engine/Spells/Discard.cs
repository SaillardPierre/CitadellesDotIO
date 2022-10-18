using CitadellesDotIO.Engine.Targets;
using CitadellesDotIO.Engine.Districts;
using System.Collections.Generic;

namespace CitadellesDotIO.Engine.Spells
{
    public class Discard : TableDeckTargetSpell
    {
        public override bool HasToPickTargets => true;
        public Discard(Player player)
        {
            Caster = player;
        }
        public override void Cast(ITarget target)
        {
            base.Cast(target);
            District toDiscard = target as District;
            toDiscard.Reset();
            Caster.DistrictsDeck.Remove(toDiscard);
            TableDeck.Enqueue(toDiscard);
            Caster.Gold += 2;
        }

        public override void GetAvailableTargets(List<ITarget> targets)
        {
            base.GetAvailableTargets(targets);
            Targets = new List<ITarget>(Caster.DistrictsDeck);
        }
    }
}
