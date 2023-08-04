using BarbarianSim.Config;
using BarbarianSim.Enums;

namespace BarbarianSim.Gems;

public class CrudeEmerald : Gem
{
    public CrudeEmerald(GearSlot gearSlot)
    {
        if (gearSlot.IsArmor())
        {
            Thorns = 11;
        }

        if (gearSlot.IsWeapon())
        {
            CritDamageVulnerable = 6.0;
        }

        if (gearSlot.IsJewelry())
        {
            PoisonResistance = 11.5;
        }
    }
}
