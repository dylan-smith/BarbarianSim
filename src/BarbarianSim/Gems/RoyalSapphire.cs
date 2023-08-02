using BarbarianSim.Config;
using BarbarianSim.Enums;

namespace BarbarianSim.Gems;

public class RoyalSapphire : Gem
{
    public override bool IsMaxLevel() => true;

    public RoyalSapphire(GearSlot gearSlot)
    {
        if (gearSlot.IsArmor())
        {
            DamageReductionWhileFortified = 3.0;
        }

        if (gearSlot.IsWeapon())
        {
            CritDamageCrowdControlled = 12.0;
        }

        if (gearSlot.IsJewelry())
        {
            ColdResistance = 22.1;
        }
    }
}
