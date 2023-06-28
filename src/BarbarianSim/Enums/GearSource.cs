using System;

namespace BarbarianSim
{
    public enum GearSource
    {
        Dungeon,
        Gruul,
        Magtheridon,
        Karazhan,
        Badges,
        Crafting,
        Reputation,
        AuctionHouse,
        Vendor,
        Heroic,
        WorldBoss,
        ZulGurub,
        Naxxramas,
        Honor
    }

    public static class GearSourceExtensions
    {
        public static GearSource ToGearSource(this string value)
        {
            return value switch
            {
                "dungeon" => GearSource.Dungeon,
                "gruul" => GearSource.Gruul,
                "magtheridon" => GearSource.Magtheridon,
                "karazhan" => GearSource.Karazhan,
                "badges" => GearSource.Badges,
                "crafting" => GearSource.Crafting,
                "rep" => GearSource.Reputation,
                "ah" => GearSource.AuctionHouse,
                "vendor" => GearSource.Vendor,
                "heroic" => GearSource.Heroic,
                "worldboss" => GearSource.WorldBoss,
                "zg" => GearSource.ZulGurub,
                "naxx" => GearSource.Naxxramas,
                "honor" => GearSource.Honor,
                _ => throw new ArgumentException($"Unrecognized gear source {value}"),
            };
        }
    }
}
