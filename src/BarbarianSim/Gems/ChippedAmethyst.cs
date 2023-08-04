using BarbarianSim.Config;
using BarbarianSim.Enums;

namespace BarbarianSim.Gems;

public class ChippedAmethyst : Gem
{
    public ChippedAmethyst(GearSlot gearSlot)
    {
        if (gearSlot.IsArmor())
        {
            DamageTakenOverTimeReduction = 5.0;
        }

        if (gearSlot.IsWeapon())
        {
            DamageOverTime = 5.0;
        }

        if (gearSlot.IsJewelry())
        {
            ShadowResistance = 14.3;
        }
    }
}
