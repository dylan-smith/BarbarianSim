using BarbarianSim.Config;
using BarbarianSim.Enums;

namespace BarbarianSim.Gems;

public class CrudeDiamond : Gem
{
    public CrudeDiamond(GearSlot gearSlot)
    {
        if (gearSlot.IsArmor())
        {
            BarrierGeneration = 3.0;
        }

        if (gearSlot.IsWeapon())
        {
            UltimateSkillDamage = 7.0;
        }

        if (gearSlot.IsJewelry())
        {
            ResistanceToAll = 4.0;
        }
    }
}
