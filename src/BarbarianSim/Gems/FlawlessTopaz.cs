using BarbarianSim.Config;
using BarbarianSim.Enums;

namespace BarbarianSim.Gems;

public class FlawlessTopaz : Gem
{
    public FlawlessTopaz(GearSlot gearSlot)
    {
        if (gearSlot.IsArmor())
        {
            DamageReductionWhileControlImpaired = 9;
        }

        if (gearSlot.IsWeapon())
        {
            BasicSkillDamage = 17.5;
        }

        if (gearSlot.IsJewelry())
        {
            LightningResistance = 19.6;
        }
    }
}
