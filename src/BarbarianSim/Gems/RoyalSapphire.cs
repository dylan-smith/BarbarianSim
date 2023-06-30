using BarbarianSim.Config;

namespace BarbarianSim.Gems
{
    public class RoyalSapphire : Gem
    {
        public override bool IsMaxLevel() => true;

        public RoyalSapphire() => DamageReductionWhileFortified = 3.0;
    }
}
