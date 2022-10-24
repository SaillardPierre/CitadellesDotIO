using System.Linq;

namespace CitadellesDotIO.Engine.Spells
{
    public sealed class Craft : TableDeckTargetSpell
    {
        public Craft(Player player)
        {
            Caster = player;
        }
        public override bool HasTargets => base.HasTargets && Caster.Gold >= 3;
        public override void Cast()
        {
            base.Cast();
            Caster.PickDistricts(TableDeck.PickCards(3).ToList());
            Caster.Gold -= 3;
        }
    }
}
