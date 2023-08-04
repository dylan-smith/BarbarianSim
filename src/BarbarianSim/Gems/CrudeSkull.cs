using BarbarianSim.Config;
using BarbarianSim.Enums;

namespace BarbarianSim.Gems;

public class CrudeSkull : Gem
{
    public CrudeSkull(GearSlot gearSlot)
    {
        if (gearSlot.IsArmor())
        {
            HealingReceived = 3;
        }

        if (gearSlot.IsWeapon())
        {
            LifeOnKill = 1.92;
        }

        if (gearSlot.IsJewelry())
        {
            Armor = 75;
        }
    }
}
