using BarbarianSim.Config;
using BarbarianSim.Enums;

namespace BarbarianSim.Gems;

public class RoyalAmethyst : Gem
{
    public override bool IsMaxLevel() => true;

    public RoyalAmethyst(GearSlot gearSlot)
    {
        if (gearSlot.IsArmor())
        {
            DamageTakenOverTimeReduction = 8.0;
        }

        if (gearSlot.IsWeapon())
        {
            DamageOverTime = 8.0;
        }

        if (gearSlot.IsJewelry())
        {
            ShadowResistance = 22.1;
        }
    }
}
