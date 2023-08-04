using BarbarianSim.Config;
using BarbarianSim.Enums;

namespace BarbarianSim.Gems;

public class ChippedEmerald : Gem
{
    public ChippedEmerald(GearSlot gearSlot)
    {
        if (gearSlot.IsArmor())
        {
            Thorns = 28;
        }

        if (gearSlot.IsWeapon())
        {
            CritDamageVulnerable = 7.5;
        }

        if (gearSlot.IsJewelry())
        {
            PoisonResistance = 14.3;
        }
    }
}
