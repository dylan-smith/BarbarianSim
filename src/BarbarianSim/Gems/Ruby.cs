using BarbarianSim.Config;
using BarbarianSim.Enums;

namespace BarbarianSim.Gems;

public class Ruby : Gem
{
    public Ruby(GearSlot gearSlot)
    {
        if (gearSlot.IsArmor())
        {
            MaxLifePercent = 3;
        }

        if (gearSlot.IsWeapon())
        {
            OverpowerDamage = 18;
        }

        if (gearSlot.IsJewelry())
        {
            FireResistance = 16.9;
        }
    }
}
