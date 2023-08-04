using BarbarianSim.Config;
using BarbarianSim.Enums;

namespace BarbarianSim.Gems;

public class Topaz : Gem
{
    public Topaz(GearSlot gearSlot)
    {
        if (gearSlot.IsArmor())
        {
            DamageReductionWhileControlImpaired = 8;
        }

        if (gearSlot.IsWeapon())
        {
            BasicSkillDamage = 15;
        }

        if (gearSlot.IsJewelry())
        {
            LightningResistance = 16.9;
        }
    }
}
