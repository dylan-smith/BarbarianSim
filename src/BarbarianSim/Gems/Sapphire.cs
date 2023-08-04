using BarbarianSim.Config;
using BarbarianSim.Enums;

namespace BarbarianSim.Gems;

public class Sapphire : Gem
{
    public Sapphire(GearSlot gearSlot)
    {
        if (gearSlot.IsArmor())
        {
            DamageReductionWhileFortified = 2.0;
        }

        if (gearSlot.IsWeapon())
        {
            CritDamageCrowdControlled = 9.0;
        }

        if (gearSlot.IsJewelry())
        {
            ColdResistance = 16.9;
        }
    }
}
