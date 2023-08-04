using BarbarianSim.Config;
using BarbarianSim.Enums;

namespace BarbarianSim.Gems;

public class Diamond : Gem
{
    public Diamond(GearSlot gearSlot)
    {
        if (gearSlot.IsArmor())
        {
            BarrierGeneration = 4.0;
        }

        if (gearSlot.IsWeapon())
        {
            UltimateSkillDamage = 11.0;
        }

        if (gearSlot.IsJewelry())
        {
            ResistanceToAll = 6.0;
        }
    }
}
