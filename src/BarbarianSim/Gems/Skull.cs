using BarbarianSim.Config;
using BarbarianSim.Enums;

namespace BarbarianSim.Gems;

public class Skull : Gem
{
    public Skull(GearSlot gearSlot)
    {
        if (gearSlot.IsArmor())
        {
            HealingReceived = 4;
        }

        if (gearSlot.IsWeapon())
        {
            LifeOnKill = 8.06;
        }

        if (gearSlot.IsJewelry())
        {
            Armor = 170;
        }
    }
}
