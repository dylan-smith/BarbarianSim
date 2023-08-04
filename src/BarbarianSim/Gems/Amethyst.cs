using BarbarianSim.Config;
using BarbarianSim.Enums;

namespace BarbarianSim.Gems;

public class Amethyst : Gem
{
    public Amethyst(GearSlot gearSlot)
    {
        if (gearSlot.IsArmor())
        {
            DamageTakenOverTimeReduction = 6.0;
        }

        if (gearSlot.IsWeapon())
        {
            DamageOverTime = 6.0;
        }

        if (gearSlot.IsJewelry())
        {
            ShadowResistance = 16.9;
        }
    }
}
