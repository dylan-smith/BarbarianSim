using BarbarianSim.Config;

namespace BarbarianSim.Gems
{
    public class RoyalSkull : Gem
    {
        public override bool IsMaxLevel() => true;

        public RoyalSkull() => Armor = 250;
    }
}
