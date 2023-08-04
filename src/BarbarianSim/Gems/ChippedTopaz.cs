using BarbarianSim.Config;
using BarbarianSim.Enums;

namespace BarbarianSim.Gems;

public class ChippedTopaz : Gem
{
    public ChippedTopaz(GearSlot gearSlot)
    {
        if (gearSlot.IsArmor())
        {
            DamageReductionWhileControlImpaired = 7;
        }

        if (gearSlot.IsWeapon())
        {
            BasicSkillDamage = 12.5;
        }

        if (gearSlot.IsJewelry())
        {
            LightningResistance = 14.3;
        }
    }
}
