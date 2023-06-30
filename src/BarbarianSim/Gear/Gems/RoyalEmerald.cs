using BarbarianSim.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarbarianSim.Gear.Gems
{
    public class RoyalEmerald : Gem
    {
        public override bool IsMaxLevel() => true;

        public RoyalEmerald() => CritDamageVulnerable = 3.0;
    }
}
