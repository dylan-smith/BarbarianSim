using BarbarianSim.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarbarianSim.Gear.Gems
{
    public class RoyalSapphire : Gem
    {
        public override bool IsMaxLevel() => true;

        public RoyalSapphire() => DamageReductionWhileFortified = 3.0;
    }
}
