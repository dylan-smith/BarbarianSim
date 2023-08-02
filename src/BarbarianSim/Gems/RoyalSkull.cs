using BarbarianSim.Config;
using BarbarianSim.Enums;

namespace BarbarianSim.Gems;

public class RoyalSkull : Gem
{
    public override bool IsMaxLevel() => true;

    public RoyalSkull(GearSlot gearSlot)
    {
        if (gearSlot.IsArmor())
        {
            HealingReceived = 5.0;
        }

        if (gearSlot.IsWeapon())
        {
            LifeOnKill = 24.0;
        }

        if (gearSlot.IsJewelry())
        {
            Armor = 250;
        }
    }
}
