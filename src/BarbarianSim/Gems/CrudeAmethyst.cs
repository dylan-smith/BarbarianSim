using BarbarianSim.Config;
using BarbarianSim.Enums;

namespace BarbarianSim.Gems;

public class CrudeAmethyst : Gem
{
    public CrudeAmethyst(GearSlot gearSlot)
    {
        if (gearSlot.IsArmor())
        {
            DamageTakenOverTimeReduction = 4.0;
        }

        if (gearSlot.IsWeapon())
        {
            DamageOverTime = 4.0;
        }

        if (gearSlot.IsJewelry())
        {
            ShadowResistance = 11.5;
        }
    }
}
