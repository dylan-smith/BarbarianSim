using BarbarianSim.Config;
using BarbarianSim.Enums;

namespace BarbarianSim.Gems;

public class CrudeTopaz : Gem
{
    public CrudeTopaz(GearSlot gearSlot)
    {
        if (gearSlot.IsArmor())
        {
            DamageReductionWhileControlImpaired = 6;
        }

        if (gearSlot.IsWeapon())
        {
            BasicSkillDamage = 10;
        }

        if (gearSlot.IsJewelry())
        {
            LightningResistance = 11.5;
        }
    }
}
