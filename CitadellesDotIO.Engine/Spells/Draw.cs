using System.Linq;

namespace CitadellesDotIO.Engine.Spells
{
    public sealed class Draw : TableDeckTargetSpell
    {
        public Draw(Player player)
        {
            Caster = player;
        }
        public override void Cast()
        {
            base.Cast();
            Caster.PickDistricts(TableDeck.PickCards(2).ToList());
        }
    }
}
