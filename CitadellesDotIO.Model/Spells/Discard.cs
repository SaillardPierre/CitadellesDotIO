using CitadellesDotIO.Model.Districts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CitadellesDotIO.Model.Spells
{
    public class Discard : Spell
    {
        private Deck<District> TableDeck;
        public Discard(Player player)
        {
            this.Caster = player;
            this.Targets = new List<ITarget>();
        }
        public override Type TargetType => typeof(IDealable);

        public override void Cast(ITarget target)
        {
            District toDiscard = target as District; 
            this.Caster.DistrictsDeck.Remove(toDiscard);
            this.TableDeck.Enqueue(toDiscard);
            this.Caster.Gold += 2;            
        }

        public override void GetAvailableTargets(List<ITarget> targets)
        {
            base.GetAvailableTargets(targets);
            this.TableDeck = targets.SingleOrDefault(t => t is Deck<District>) as Deck<District>;
            targets.Remove(this.TableDeck);
        }
    }
}
