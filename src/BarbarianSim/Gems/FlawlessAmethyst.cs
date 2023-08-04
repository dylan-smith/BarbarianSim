using BarbarianSim.Config;
using BarbarianSim.Enums;

namespace BarbarianSim.Gems;

public class FlawlessAmethyst : Gem
{
    public FlawlessAmethyst(GearSlot gearSlot)
    {
        if (gearSlot.IsArmor())
        {
            DamageTakenOverTimeReduction = 7.0;
        }

        if (gearSlot.IsWeapon())
        {
            DamageOverTime = 7.0;
        }

        if (gearSlot.IsJewelry())
        {
            ShadowResistance = 19.6;
        }
    }
}
