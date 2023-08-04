using BarbarianSim.Config;
using BarbarianSim.Enums;

namespace BarbarianSim.Gems;

public class FlawlessEmerald : Gem
{
    public FlawlessEmerald(GearSlot gearSlot)
    {
        if (gearSlot.IsArmor())
        {
            Thorns = 150;
        }

        if (gearSlot.IsWeapon())
        {
            CritDamageVulnerable = 10.5;
        }

        if (gearSlot.IsJewelry())
        {
            PoisonResistance = 19.6;
        }
    }
}
