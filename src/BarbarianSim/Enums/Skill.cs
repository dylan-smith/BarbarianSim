namespace BarbarianSim.Enums;

public enum Skill
{
    // Basic
    LungingStrike, // Lunge forward and strike an enemy for 33% damage
    EnhancedLungingStrike, // Lunging strike deals 30%[x] increased damage and Heals you for 2% Maximum Life when it damages a Healthy enemy
    BattleLungingStrike, // Lunging strike also inflicts 20% bleeding damage over 5 seconds
    CombatLungingStrike, // Critical strikes with Lunging Strike grant you Berserking for 1.5 seconds
    Bash,
    EnhancedBash,
    BattleBash,
    CombatBash,
    Frenzy,
    EnhancedFrenzy,
    CombatFrenzy,
    BattleFrenzy,
    Flay,
    EnhancedFlay,
    BattleFlay,
    CombatFlay,

    // Core
    Whirlwind,
    EnhancedWhirlwind, // Gain 1 Fury each time Whirlwind deals damage to an enemy, 4 Fury against Elite enemies
    ViolentWhirlwind, // After using Whirlwind for 2 seconds, Whirlwind deals 30%[x] increased damage until cancelled
    FuriousWhirlwind, // While using a slashing weapon Whirlwind also inflicts 40% of it's Base damage as Bleeding damage over 5 seconds
    HammerOfTheAncients,
    EnhancedHammerOfTheAncients,
    ViolentHammerOfTheAncients,
    FuriousHammerOfTheAncients,
    PressurePoint, // Lucky Hit: Your Core skills have up to a 30% chance to make enemies Vulnerable for 2 seconds
    Upheaval,
    EnhancedUpheaval,
    ViolentUpheaval,
    FuriousUpheaval,
    DoubleSwing,
    EnhancedDoubleSwing,
    ViolentDoubleSwing,
    FuriousDoubleSwing,
    EndlessFury,
    Rend,
    EnhancedRend,
    ViolentRend,
    FuriousRend,

    // Defensive
    GroundStomp,
    EnhancedGroundStomp,
    TacticalGroundStomp,
    StrategicGroundStomp,
    ImposingPresence,
    MartialVigor,
    RallyingCry, // Bellow a Rallying Cry, increasing your Movement Speed by 30%[+] and Resource Generation by 40%[x] for 6.0 seconds, and Nearby allies for 3.0 seconds (Cooldown: 25 seconds)
    EnhancedRallyingCry, // Rallying Cry grants you Unstoppable while active
    TacticalRallyingCry, // Rallying Cry generates 20 fury, and grants you an additional 20%[x] Resource Generation
    StrategicRallyingCry, // Rallying Cry grants you 10% Base Life (10%[x] HP) as Fortify. While Rallying Cry is active, you gain an additional 2% Base Life (2%[x] HP) as Fortify each time you take or deal Direct Damage
    IronSkin,
    EnhancedIronSkin,
    TacticalIronSkin,
    StrategicIronSkin,
    Outburst,
    ToughAsNails,
    ChallengingShout, // Taunt nearby enemies and gain 40% Damage Reduction for 6 seconds (Cooldown: 25 seconds)
    EnhancedChallengingShout, // While Challenging Shout is active, gain 20%[x] bonus Max Life
    TacticalChallengingShout, // While Challenging Shout is active, you gain 3 Fury each time you take damage
    StrategicChallengingShout, // While Challenging Shout is active, gain Thorns equal to 30% of your Maximum Life

    // Brawling
    Kick,
    EnhancedKick,
    MightyKick,
    PowerKick,
    WarCry,
    EnhancedWarCry,
    MightyWarCry,
    PowerWarCry,
    BoomingVoice,
    RaidLeader,
    GutteralYell,
    Charge,
    EnhancedCharge,
    MightyCharge,
    PowerCharge,
    Leap,
    EnhancedLeap,
    MightyLeap,
    PowerLeap,
    AggressiveResistance,
    BattleFervor,
    ProlificFury,
    Swiftness,
    QuickImpulses,

    // Weapon Mastery
    PitFighter,
    NomMercy,
    SlayingStrike,
    ExposeVulnerability,
    Rupture,
    EnhancedRupture,
    WarriorsRupture,
    FightersRupture,
    Hamstring,
    CutToTheBone,
    SteelGrasp,
    EnhancedSteelGrasp,
    WarriorsSteelGrasp,
    FightersSteelGrasp,
    ThickSkin,
    DefensiveStance,
    Counteroffensive,
    DeathBlow,
    EnhancedDeathBlow,
    WarriorsDeathBlow,
    FightersDeathBlow,

    // Ultimate
    CallOfTheAncients,
    PrimeCallOfTheAncients,
    SupremeCallOfTheAncients,
    Duelist,
    IronMaelstrom,
    PrimeIronMaelstrom,
    SupremeIronMaelstrom,
    TemperedFury,
    FuriousImpulse,
    InvigoratingFury,
    WrathOfTheBerserker,
    PrimeWrathOfTheBerserker,
    SupremeWrathOfTheBerserker,
    Wallop,
    HeavyHanded,
    BruteForce,
    Concussion,

    // Key Passives
    GushingWounds,
    UnbridledRage,
    WalkingArsenal,
    Unconstrained,
}
