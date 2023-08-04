using BarbarianSim.Config;
using BarbarianSim.Enums;

namespace BarbarianSim.Gems;

public class FlawlessDiamond : Gem
{
    public FlawlessDiamond(GearSlot gearSlot)
    {
        if (gearSlot.IsArmor())
        {
            BarrierGeneration = 4.5;
        }

        if (gearSlot.IsWeapon())
        {
            UltimateSkillDamage = 13.0;
        }

        if (gearSlot.IsJewelry())
        {
            ResistanceToAll = 7.0;
        }
    }
}
