using CitadellesDotIO.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitadellesDotIO.Engine.Passives
{
    public sealed class IncreasePoolSize : Passive
    {
        private readonly int PoolSizeBonus;
        public IncreasePoolSize(Player player, int poolSizeBonus)
        {
            this.Player = player;
            this.PoolSizeBonus = poolSizeBonus;
        }
        public override void Apply()
        {
            this.Player.ResetPoolSize();
            this.Player.PoolSize += this.PoolSizeBonus;
        }
    }
}
