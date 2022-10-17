﻿using CitadellesDotIO.Model.Districts;
using CitadellesDotIO.Model.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitadellesDotIO.Model.Spells
{
    public class Craft : TableDeckTargetSpell
    {
        public Craft(Player player)
        {
            this.Caster = player;
        }
        public override bool HasTargets => base.HasTargets && this.Caster.Gold >= 3;
        public override void Cast()
        {
            base.Cast();
            this.Caster.PickDistricts(this.TableDeck.PickCards(3).ToList());
            this.Caster.Gold -= 3;
        }
    }
}
