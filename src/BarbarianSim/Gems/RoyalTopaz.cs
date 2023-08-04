using BarbarianSim.Config;
using BarbarianSim.Enums;

namespace BarbarianSim.Gems;

public class RoyalTopaz : Gem
{
    public override bool IsMaxLevel() => true;

    public RoyalTopaz(GearSlot gearSlot)
    {
        if (gearSlot.IsArmor())
        {
            DamageReductionWhileControlImpaired = 10.0;
        }

        if (gearSlot.IsWeapon())
        {
            BasicSkillDamage = 20.0;
        }

        if (gearSlot.IsJewelry())
        {
            LightningResistance = 22.1;
        }
    }
}
