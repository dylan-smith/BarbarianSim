using BarbarianSim.Config;
using BarbarianSim.Enums;

namespace BarbarianSim.Gems;

public class RoyalDiamond : Gem
{
    public override bool IsMaxLevel() => true;

    public RoyalDiamond(GearSlot gearSlot)
    {
        if (gearSlot.IsArmor())
        {
            BarrierGeneration = 5.0;
        }

        if (gearSlot.IsWeapon())
        {
            UltimateSkillDamage = 15.0;
        }

        if (gearSlot.IsJewelry())
        {
            ResistanceToAll = 8.0;
        }
    }
}
