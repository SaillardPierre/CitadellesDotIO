using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitadellesDotIO.Model.Passives
{
    public class IncreasePickSize : Passive
    {
        private readonly int PickSizeBonus;
        public IncreasePickSize(Player player, int pickSizeBonus)
        {
            this.Player = player;
            this.PickSizeBonus = pickSizeBonus;
        }
        public override void Apply()
        {
            this.Player.PickSize += this.PickSizeBonus;
        }
    }
}
