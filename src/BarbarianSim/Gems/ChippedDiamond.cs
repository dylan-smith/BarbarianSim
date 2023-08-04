using BarbarianSim.Config;
using BarbarianSim.Enums;

namespace BarbarianSim.Gems;

public class ChippedDiamond : Gem
{
    public ChippedDiamond(GearSlot gearSlot)
    {
        if (gearSlot.IsArmor())
        {
            BarrierGeneration = 3.5;
        }

        if (gearSlot.IsWeapon())
        {
            UltimateSkillDamage = 9.0;
        }

        if (gearSlot.IsJewelry())
        {
            ResistanceToAll = 5.0;
        }
    }
}
