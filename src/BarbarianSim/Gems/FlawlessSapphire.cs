using BarbarianSim.Config;
using BarbarianSim.Enums;

namespace BarbarianSim.Gems;

public class FlawlessSapphire : Gem
{
    public FlawlessSapphire(GearSlot gearSlot)
    {
        if (gearSlot.IsArmor())
        {
            DamageReductionWhileFortified = 2.5;
        }

        if (gearSlot.IsWeapon())
        {
            CritDamageCrowdControlled = 10.5;
        }

        if (gearSlot.IsJewelry())
        {
            ColdResistance = 19.6;
        }
    }
}
