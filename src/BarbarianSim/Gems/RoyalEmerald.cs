using BarbarianSim.Config;

namespace BarbarianSim.Gems
{
    public class RoyalEmerald : Gem
    {
        public override bool IsMaxLevel() => true;

        public RoyalEmerald() => CritDamageVulnerable = 3.0;
    }
}
