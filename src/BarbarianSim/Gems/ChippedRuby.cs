using BarbarianSim.Config;
using BarbarianSim.Enums;

namespace BarbarianSim.Gems;

public class ChippedRuby : Gem
{
    public ChippedRuby(GearSlot gearSlot)
    {
        if (gearSlot.IsArmor())
        {
            MaxLifePercent = 2.5;
        }

        if (gearSlot.IsWeapon())
        {
            OverpowerDamage = 15;
        }

        if (gearSlot.IsJewelry())
        {
            FireResistance = 14.3;
        }
    }
}
