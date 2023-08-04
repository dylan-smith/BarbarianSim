using BarbarianSim.Config;
using BarbarianSim.Enums;

namespace BarbarianSim.Gems;

public class CrudeRuby : Gem
{
    public CrudeRuby(GearSlot gearSlot)
    {
        if (gearSlot.IsArmor())
        {
            MaxLifePercent = 2;
        }

        if (gearSlot.IsWeapon())
        {
            OverpowerDamage = 12;
        }

        if (gearSlot.IsJewelry())
        {
            FireResistance = 11.5;
        }
    }
}
