using BarbarianSim.Config;
using BarbarianSim.Enums;

namespace BarbarianSim.Gems;

public class FlawlessSkull : Gem
{
    public FlawlessSkull(GearSlot gearSlot)
    {
        if (gearSlot.IsArmor())
        {
            HealingReceived = 4.5;
        }

        if (gearSlot.IsWeapon())
        {
            LifeOnKill = 12;
        }

        if (gearSlot.IsJewelry())
        {
            Armor = 220;
        }
    }
}
