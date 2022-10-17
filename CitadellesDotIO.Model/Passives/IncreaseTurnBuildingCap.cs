using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitadellesDotIO.Model.Passives
{
    public class IncreaseTurnBuildingCap : Passive
    {
        private readonly int TurnBuildingCapBonus;
        public IncreaseTurnBuildingCap(Player player, int turnBuildingCapBonus)
        {
            this.Player = player;
            this.TurnBuildingCapBonus = turnBuildingCapBonus;
        }

        public override void Apply()
        {
            this.Player.TurnBuildingCap += this.TurnBuildingCapBonus;
        }
    }
}
