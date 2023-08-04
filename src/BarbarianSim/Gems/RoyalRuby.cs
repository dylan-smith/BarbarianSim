using BarbarianSim.Config;
using BarbarianSim.Enums;

namespace BarbarianSim.Gems;

public class RoyalRuby : Gem
{
    public override bool IsMaxLevel() => true;

    public RoyalRuby(GearSlot gearSlot)
    {
        if (gearSlot.IsArmor())
        {
            MaxLifePercent = 4.0;
        }

        if (gearSlot.IsWeapon())
        {
            OverpowerDamage = 24.0;
        }

        if (gearSlot.IsJewelry())
        {
            FireResistance = 22.1;
        }
    }
}
