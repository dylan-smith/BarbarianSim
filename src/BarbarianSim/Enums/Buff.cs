namespace HunterSim
{
    public enum Buff
    {
        BattleShout, // 305 Melee AP
        ImprovedBattleShout, // 381 Melee AP
        TrueshotAura, // 125 AP
        LeaderOfThePack, // Ranged + Melee Crit 5%
        MarkOfTheWild, // 340 Armor, all attributes by 14, all resistances by 25 (gift of the wild is the same)
        ImprovedMarkOfTheWild, // 35% more than regular MotW
        BlessingOfKings, // All stats by 10%
        BlessingOfMight, // 220 AP
        ImprovedBlessingOfMight, // 264 AP
        // TODO: WindfuryTotem, // 20% chance on MH hit to grant an extra attack with +445 AP
        // TODO: ImprovedWindfuryTotem, 
        GraceOfAirTotem, // 77 Agility
        ImprovedGraceOfAirTotem, // 88 Agility
        StrengthOfEarthTotem, // 86 Strength
        ImprovedStrengthOfEarthTotem, // 98 Strength
        ArcaneBrilliance, // 40 int
        // TODO: Heroism (how many times) (called Bloodlust for horde)
        // TODO: Drums (Battle and /or War)
        // TODO: Heroic Presence (not yours)
        // TODO: Prayer of Fortitude
        // TODO: Improved Prayer of Fortitude (30% bonus)
        // TODO: Blood Pact
        // TODO: Improved Blood Pact (30% bonus)
        // === Food ===
        // TODO: Blackened Sporefish
        GrilledMudfish, // 20 agi, 20 spi
        // TODO: Grilled Squid
        // TODO: Ravager Dog
        // TODO: Spicy Crawdad
        // TODO: Spicy Hot Talbuk
        // TODO: Talbuk Steak
        // TODO: Warp Burger
        // === Battle Elixirs ===
        // TODO: Elixir of Demonslaying
        ElixirOfMajorAgility, // 35 agi, 20 crit
        // TODO: Elixir of the Mongoose
        // TODO: Fel Strength Elixir
        // TODO: Onslaught Elixir
        // TODO: Elixir of Greater Agility
        // TODO: Elixir of Agility
        // TODO: Elixir of Mastery
        // == Guardian Elixirs ===
        // TODO: Elixir of Major Mageblood
        // TODO: Elixir of Draenic Wisdom
        // TODO: Mageblood Potion
        // TODO: Elixir of Greater Intellect
        // TODO: Elixir of Fortitude
        // === Flasks ===
        // TODO: Flask of Distilled Wisdom
        // TODO: Flask of Fortification
        // TODO: Flask of Major Restoration
        // TODO: Flask of Relentless Assault
        // TODO: Flask of the Titans
        // TODO: Unstable Flask of the Bandit
        // === Scrolls ===
        ScrollOfAgilityV, // 20 agi
        // TODO: Scroll of Agility IV
        ScrollOfStrengthV, // 20 str
        // TODO: Scroll of Strength IV
        // === Potions === [These probably don't belong as buffs, but in the rotation code]
        // TODO: Haste Potion
        // TODO: Fel Mana Potion
        // TODO: Super Mana Potion
        // TODO: Dark Rune
        // TODO: Demonic Rune
        // === Pet Food ===
        // TODO: Kibler's Bits
        // TODO: Sporeling Snack
        // === Pet Scrolls ===
        // TODO: Scroll of Agility V
        // TODO: Scroll of Agility IV
        // TODO: Scroll of Strength V
        // TODO: Scroll of Strength IV
        // TODO: Pet Buffs
        // === Boss Debuffs ===
        HuntersMark, // 110-440 ranged AP
        ImprovedHuntersMark // 110-440 ranged AP + 110 melee AP
        // TODO: Judgement of Wisdom
        // TODO: Judgement of the Crusader
        // TODO: Curse of Elements
        // TODO: Improved Curse of Elements
        // TODO: Expose Weakness (Not You), need to also specify AP and uptime
        // TODO: Sunder Armor
        // TODO: Improved Expose Armor
        // TODO: Curse of Recklessness
        // TODO: Faerie Fire
        // TODO: Improved Faerie Fire
        // TODO: Misery
        // TODO: Blood Frenzy
        // === Party Proc Buffs ===
        // TODO: Unleashed Rage
        // TODO: Ferocious Inspiration (Not You)
    }
}
