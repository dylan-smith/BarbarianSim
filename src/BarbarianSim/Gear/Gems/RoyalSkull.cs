using BarbarianSim.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarbarianSim.Gear.Gems
{
    public class RoyalSkull : Gem
    {
        public override bool IsMaxLevel() => true;

        public RoyalSkull() => Armor = 250;
    }
}
