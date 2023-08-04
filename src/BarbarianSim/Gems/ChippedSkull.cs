using BarbarianSim.Config;
using BarbarianSim.Enums;

namespace BarbarianSim.Gems;

public class ChippedSkull : Gem
{
    public ChippedSkull(GearSlot gearSlot)
    {
        if (gearSlot.IsArmor())
        {
            HealingReceived = 3.5;
        }

        if (gearSlot.IsWeapon())
        {
            LifeOnKill = 4.99;
        }

        if (gearSlot.IsJewelry())
        {
            Armor = 125;
        }
    }
}
