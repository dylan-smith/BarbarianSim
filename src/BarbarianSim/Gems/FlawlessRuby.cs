using BarbarianSim.Config;
using BarbarianSim.Enums;

namespace BarbarianSim.Gems;

public class FlawlessRuby : Gem
{
    public FlawlessRuby(GearSlot gearSlot)
    {
        if (gearSlot.IsArmor())
        {
            MaxLifePercent = 3.5;
        }

        if (gearSlot.IsWeapon())
        {
            OverpowerDamage = 21;
        }

        if (gearSlot.IsJewelry())
        {
            FireResistance = 19.6;
        }
    }
}
