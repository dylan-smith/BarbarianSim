using BarbarianSim.Config;
using BarbarianSim.Enums;

namespace BarbarianSim.Gems;

public class Emerald : Gem
{
    public Emerald(GearSlot gearSlot)
    {
        if (gearSlot.IsArmor())
        {
            Thorns = 70;
        }

        if (gearSlot.IsWeapon())
        {
            CritDamageVulnerable = 9.0;
        }

        if (gearSlot.IsJewelry())
        {
            PoisonResistance = 16.9;
        }
    }
}
