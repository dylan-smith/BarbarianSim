using BarbarianSim.Config;
using BarbarianSim.Enums;

namespace BarbarianSim.Gems;

public class RoyalEmerald : Gem
{
    public override bool IsMaxLevel() => true;

    public RoyalEmerald(GearSlot gearSlot)
    {
        if (gearSlot.IsArmor())
        {
            Thorns = 250;
        }

        if (gearSlot.IsWeapon())
        {
            CritDamageVulnerable = 12.0;
        }

        if (gearSlot.IsJewelry())
        {
            PoisonResistance = 22.1;
        }
    }
}
