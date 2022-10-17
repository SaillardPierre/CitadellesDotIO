using CitadellesDotIO.Model.Districts;
using CitadellesDotIO.Model.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitadellesDotIO.Model.Spells
{
    public class Draw : TableDeckTargetSpell
    {
        public Draw(Player player)
        {
            this.Caster = player;
        }
        public override void Cast()
        {
            base.Cast();
            this.Caster.PickDistricts(this.TableDeck.PickCards(2).ToList());
        }
    }
}
