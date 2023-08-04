using BarbarianSim.Config;
using BarbarianSim.Enums;

namespace BarbarianSim.Gems;

public class ChippedSapphire : Gem
{
    public ChippedSapphire(GearSlot gearSlot)
    {
        if (gearSlot.IsArmor())
        {
            DamageReductionWhileFortified = 1.5;
        }

        if (gearSlot.IsWeapon())
        {
            CritDamageCrowdControlled = 7.5;
        }

        if (gearSlot.IsJewelry())
        {
            ColdResistance = 14.3;
        }
    }
}
