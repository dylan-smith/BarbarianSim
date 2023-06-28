using System;

namespace BarbarianSim
{
    public enum WeaponType
    {
        NotSet,
        Bow,
        Crossbow,
        Dagger,
        Fist,
        Gun,
        OneHandedAxe,
        OneHandedMace,
        OneHandedSword,
        Polearm,
        Staff,
        Thrown,
        TwoHandedAxe,
        TwoHandedMace,
        TwoHandedSword,
        Wand
    }

    public static class WeaponTypeExtensions
    {
        public static WeaponType ToWeaponType(this string value)
        {
            return value switch
            {
                "bow" => WeaponType.Bow,
                "crossbow" => WeaponType.Crossbow,
                "dagger" => WeaponType.Dagger,
                "fist" => WeaponType.Fist,
                "gun" => WeaponType.Gun,
                "axe" => WeaponType.OneHandedAxe,
                "mace" => WeaponType.OneHandedMace,
                "sword" => WeaponType.OneHandedSword,
                "polearm" => WeaponType.Polearm,
                "staff" => WeaponType.Staff,
                "thrown" => WeaponType.Thrown,
                "two-handed-axe" => WeaponType.TwoHandedAxe,
                "two-handed-mace" => WeaponType.TwoHandedMace,
                "two-handed-sword" => WeaponType.TwoHandedSword,
                "wand" => WeaponType.Wand,
                _ => throw new Exception("Unrecognized weapon type"),// TODO: Richer exceptions
            };
        }
    }
}
