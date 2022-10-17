using CitadellesDotIO.Model.Districts;
using CitadellesDotIO.Model.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitadellesDotIO.Model.Spells
{
    public abstract class TableDeckTargetSpell : Spell
    {
        protected Deck<District> TableDeck;
        public override Type TargetType => typeof(IDeck);
        public override bool HasToPickTargets => false;
        public override bool HasTargets => 
            this.HasToPickTargets ? base.HasTargets && this.TableDeck != null : this.TableDeck != null;

        public override void GetAvailableTargets(List<ITarget> targets)
        {
            base.GetAvailableTargets(targets);
            Deck<District> tableDeck = targets.SingleOrDefault(t => t is Deck<District>) as Deck<District>;
            this.TableDeck = tableDeck;
            targets.Remove(tableDeck);
        }
    }
}
