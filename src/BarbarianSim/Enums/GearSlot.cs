namespace BarbarianSim.Enums;

public enum GearSlot
{
    None,
    Helm,
    Chest,
    Gloves,
    Pants,
    Boots,
    TwoHandBludgeoning,
    OneHandLeft,
    OneHandRight,
    TwoHandSlashing,
    Amulet,
    Ring1,
    Ring2,
}

public static class GearSlotExtensions
{
    public static bool IsArmor(this GearSlot gearSlot) => gearSlot is GearSlot.Helm or GearSlot.Chest or GearSlot.Gloves or GearSlot.Pants or GearSlot.Boots;
    public static bool IsWeapon(this GearSlot gearSlot) => gearSlot is GearSlot.TwoHandBludgeoning or GearSlot.OneHandLeft or GearSlot.OneHandRight or GearSlot.TwoHandSlashing;
    public static bool IsJewelry(this GearSlot gearSlot) => gearSlot is GearSlot.Amulet or GearSlot.Ring1 or GearSlot.Ring2;
}
