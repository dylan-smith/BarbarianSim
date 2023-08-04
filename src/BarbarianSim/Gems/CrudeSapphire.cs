using BarbarianSim.Config;
using BarbarianSim.Enums;

namespace BarbarianSim.Gems;

public class CrudeSapphire : Gem
{
    public CrudeSapphire(GearSlot gearSlot)
    {
        if (gearSlot.IsArmor())
        {
            DamageReductionWhileFortified = 1.0;
        }

        if (gearSlot.IsWeapon())
        {
            CritDamageCrowdControlled = 6.0;
        }

        if (gearSlot.IsJewelry())
        {
            ColdResistance = 11.5;
        }
    }
}
