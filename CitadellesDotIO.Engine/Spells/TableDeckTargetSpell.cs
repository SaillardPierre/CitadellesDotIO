using CitadellesDotIO.Engine.Targets;
using CitadellesDotIO.Engine.Districts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CitadellesDotIO.Engine.Spells
{
    public abstract class TableDeckTargetSpell : Spell
    {
        protected Deck<District> TableDeck;
        public override Type TargetType => typeof(IDeck);
        public override bool HasToPickTargets => false;
        public override bool HasTargets =>
            HasToPickTargets ? base.HasTargets && TableDeck != null : TableDeck != null;

        public override void GetAvailableTargets(List<ITarget> targets)
        {
            base.GetAvailableTargets(targets);
            Deck<District> tableDeck = targets.SingleOrDefault(t => t is Deck<District>) as Deck<District>;
            TableDeck = tableDeck;
            targets.Remove(tableDeck);
        }
    }
}
