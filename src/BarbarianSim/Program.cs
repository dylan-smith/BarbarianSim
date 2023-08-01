using BarbarianSim.Abilities;
using BarbarianSim.Aspects;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.Gems;
using BarbarianSim.Rotations;
using BarbarianSim.Skills;
using BarbarianSim.StatCalculators;
using Microsoft.Extensions.DependencyInjection;

namespace BarbarianSim;

internal class Program
{
    private static void Main()
    {
        var serviceCollection = new ServiceCollection();

        // Abilities
        serviceCollection.AddSingleton<ChallengingShout>();
        serviceCollection.AddSingleton<IronSkin>();
        serviceCollection.AddSingleton<LungingStrike>();
        serviceCollection.AddSingleton<RallyingCry>();
        serviceCollection.AddSingleton<WarCry>();
        serviceCollection.AddSingleton<Whirlwind>();
        serviceCollection.AddSingleton<WrathOfTheBerserker>();

        // Aspects
        serviceCollection.AddSingleton<AspectOfAnemia>();
        serviceCollection.AddSingleton<AspectOfBerserkRipping>();
        serviceCollection.AddSingleton<AspectOfDisobedience>();
        serviceCollection.AddSingleton<AspectOfEchoingFury>();
        serviceCollection.AddSingleton<AspectOfGraspingWhirlwind>();
        serviceCollection.AddSingleton<AspectOfLimitlessRage>();
        serviceCollection.AddSingleton<AspectOfNumbingWraith>();
        serviceCollection.AddSingleton<AspectOfTheDireWhirlwind>();
        serviceCollection.AddSingleton<AspectOfTheExpectant>();
        serviceCollection.AddSingleton<AspectOfTheIronWarrior>();
        serviceCollection.AddSingleton<AspectOfTheProtector>();
        serviceCollection.AddSingleton<BoldChieftainsAspect>();
        serviceCollection.AddSingleton<ConceitedAspect>();
        serviceCollection.AddSingleton<EdgemastersAspect>();
        serviceCollection.AddSingleton<ExploitersAspect>();
        serviceCollection.AddSingleton<GhostwalkerAspect>();
        serviceCollection.AddSingleton<GohrsDevastatingGrips>();
        serviceCollection.AddSingleton<PenitentGreaves>();
        serviceCollection.AddSingleton<RamaladnisMagnumOpus>();
        serviceCollection.AddSingleton<RageOfHarrogath>();
        serviceCollection.AddSingleton<SmitingAspect>();

        // Event Handlers
        serviceCollection.AddSingleton<EventHandlers.EventHandler<AspectOfEchoingFuryProcEvent>, EventHandlers.AspectOfEchoingFuryProcEventHandler>();
        serviceCollection.AddSingleton<EventHandlers.EventHandler<AspectOfTheProtectorProcEvent>, EventHandlers.AspectOfTheProtectorProcEventHandler>();
        serviceCollection.AddSingleton<EventHandlers.EventHandler<AuraAppliedEvent>, EventHandlers.AuraAppliedEventHandler>();
        serviceCollection.AddSingleton<EventHandlers.EventHandler<AuraExpiredEvent>, EventHandlers.AuraExpiredEventHandler>();
        serviceCollection.AddSingleton<EventHandlers.EventHandler<BarrierAppliedEvent>, EventHandlers.BarrierAppliedEventHandler>();
        serviceCollection.AddSingleton<EventHandlers.EventHandler<BarrierExpiredEvent>, EventHandlers.BarrierExpiredEventHandler>();
        serviceCollection.AddSingleton<EventHandlers.EventHandler<BleedAppliedEvent>, EventHandlers.BleedAppliedEventHandler>();
        serviceCollection.AddSingleton<EventHandlers.EventHandler<BleedCompletedEvent>, EventHandlers.BleedCompletedEventHandler>();
        serviceCollection.AddSingleton<EventHandlers.EventHandler<ChallengingShoutEvent>, EventHandlers.ChallengingShoutEventHandler>();
        serviceCollection.AddSingleton<EventHandlers.EventHandler<DamageEvent>, EventHandlers.DamageEventHandler>();
        serviceCollection.AddSingleton<EventHandlers.EventHandler<DirectDamageEvent>, EventHandlers.DirectDamageEventHandler>();
        serviceCollection.AddSingleton<EventHandlers.EventHandler<FortifyGeneratedEvent>, EventHandlers.FortifyGeneratedEventHandler>();
        serviceCollection.AddSingleton<EventHandlers.EventHandler<FuryGeneratedEvent>, EventHandlers.FuryGeneratedEventHandler>();
        serviceCollection.AddSingleton<EventHandlers.EventHandler<FurySpentEvent>, EventHandlers.FurySpentEventHandler>();
        serviceCollection.AddSingleton<EventHandlers.EventHandler<GohrsDevastatingGripsProcEvent>, EventHandlers.GohrsDevastatingGripsProcEventHandler>();
        serviceCollection.AddSingleton<EventHandlers.EventHandler<GutteralYellProcEvent>, EventHandlers.GutteralYellProcEventHandler>();
        serviceCollection.AddSingleton<EventHandlers.EventHandler<HealingEvent>, EventHandlers.HealingEventHandler>();
        serviceCollection.AddSingleton<EventHandlers.EventHandler<IronSkinEvent>, EventHandlers.IronSkinEventHandler>();
        serviceCollection.AddSingleton<EventHandlers.EventHandler<LuckyHitEvent>, EventHandlers.LuckyHitEventHandler>();
        serviceCollection.AddSingleton<EventHandlers.EventHandler<LungingStrikeEvent>, EventHandlers.LungingStrikeEventHandler>();
        serviceCollection.AddSingleton<EventHandlers.EventHandler<PressurePointProcEvent>, EventHandlers.PressurePointProcEventHandler>();
        serviceCollection.AddSingleton<EventHandlers.EventHandler<RaidLeaderProcEvent>, EventHandlers.RaidLeaderProcEventHandler>();
        serviceCollection.AddSingleton<EventHandlers.EventHandler<RallyingCryEvent>, EventHandlers.RallyingCryEventHandler>();
        serviceCollection.AddSingleton<EventHandlers.EventHandler<SimulationStartedEvent>, EventHandlers.SimulationStartedEventHandler>();
        serviceCollection.AddSingleton<EventHandlers.EventHandler<WarCryEvent>, EventHandlers.WarCryEventHandler>();
        serviceCollection.AddSingleton<EventHandlers.EventHandler<WhirlwindRefreshEvent>, EventHandlers.WhirlwindRefreshEventHandler>();
        serviceCollection.AddSingleton<EventHandlers.EventHandler<WhirlwindSpinEvent>, EventHandlers.WhirlwindSpinEventHandler>();
        serviceCollection.AddSingleton<EventHandlers.EventHandler<WhirlwindStoppedEvent>, EventHandlers.WhirlwindStoppedEventHandler>();
        serviceCollection.AddSingleton<EventHandlers.EventHandler<WrathOfTheBerserkerEvent>, EventHandlers.WrathOfTheBerserkerEventHandler>();

        // Rotations
        serviceCollection.AddSingleton<SpinToWin>();

        // Skills
        serviceCollection.AddSingleton<AggressiveResistance>();
        serviceCollection.AddSingleton<BattleLungingStrike>();
        serviceCollection.AddSingleton<BoomingVoice>();
        serviceCollection.AddSingleton<CombatLungingStrike>();
        serviceCollection.AddSingleton<EnhancedChallengingShout>();
        serviceCollection.AddSingleton<EnhancedLungingStrike>();
        serviceCollection.AddSingleton<EnhancedRallyingCry>();
        serviceCollection.AddSingleton<EnhancedWarCry>();
        serviceCollection.AddSingleton<EnhancedWhirlwind>();
        serviceCollection.AddSingleton<FuriousWhirlwind>();
        serviceCollection.AddSingleton<GutteralYell>();
        serviceCollection.AddSingleton<Hamstring>();
        serviceCollection.AddSingleton<HeavyHanded>();
        serviceCollection.AddSingleton<InvigoratingFury>();
        serviceCollection.AddSingleton<MightyWarCry>();
        serviceCollection.AddSingleton<PitFighter>();
        serviceCollection.AddSingleton<PowerWarCry>();
        serviceCollection.AddSingleton<PressurePoint>();
        serviceCollection.AddSingleton<PrimeWrathOfTheBerserker>();
        serviceCollection.AddSingleton<ProlificFury>();
        serviceCollection.AddSingleton<RaidLeader>();
        serviceCollection.AddSingleton<StrategicChallengingShout>();
        serviceCollection.AddSingleton<StrategicIronSkin>();
        serviceCollection.AddSingleton<StrategicRallyingCry>();
        serviceCollection.AddSingleton<SupremeWrathOfTheBerserker>();
        serviceCollection.AddSingleton<TacticalIronSkin>();
        serviceCollection.AddSingleton<TacticalRallyingCry>();
        serviceCollection.AddSingleton<TemperedFury>();
        serviceCollection.AddSingleton<UnbridledRage>();
        serviceCollection.AddSingleton<ViolentWhirlwind>();

        // Stat Calculators
        serviceCollection.AddSingleton<AdditiveDamageBonusCalculator>();
        serviceCollection.AddSingleton<ArmorCalculator>();
        serviceCollection.AddSingleton<AttackSpeedCalculator>();
        serviceCollection.AddSingleton<BerserkingDamageCalculator>();
        serviceCollection.AddSingleton<CritChanceCalculator>();
        serviceCollection.AddSingleton<CritChancePhysicalAgainstElitesCalculator>();
        serviceCollection.AddSingleton<CritDamageCalculator>();
        serviceCollection.AddSingleton<CrowdControlDurationCalculator>();
        serviceCollection.AddSingleton<DamageReductionCalculator>();
        serviceCollection.AddSingleton<DamageReductionFromBleedingCalculator>();
        serviceCollection.AddSingleton<DamageReductionFromCloseCalculator>();
        serviceCollection.AddSingleton<DamageReductionWhileFortifiedCalculator>();
        serviceCollection.AddSingleton<DamageReductionWhileInjuredCalculator>();
        serviceCollection.AddSingleton<DamageToCloseCalculator>();
        serviceCollection.AddSingleton<DamageToCrowdControlledCalculator>();
        serviceCollection.AddSingleton<DamageToInjuredCalculator>();
        serviceCollection.AddSingleton<DamageToSlowedCalculator>();
        serviceCollection.AddSingleton<DexterityCalculator>();
        serviceCollection.AddSingleton<DodgeCalculator>();
        serviceCollection.AddSingleton<FuryCostReductionCalculator>();
        serviceCollection.AddSingleton<HealingReceivedCalculator>();
        serviceCollection.AddSingleton<IntelligenceCalculator>();
        serviceCollection.AddSingleton<LuckyHitChanceCalculator>();
        serviceCollection.AddSingleton<MaxFuryCalculator>();
        serviceCollection.AddSingleton<MaxLifeCalculator>();
        serviceCollection.AddSingleton<MovementSpeedCalculator>();
        serviceCollection.AddSingleton<OverpowerDamageCalculator>();
        serviceCollection.AddSingleton<PhysicalDamageCalculator>();
        serviceCollection.AddSingleton<ResistanceToAllCalculator>();
        serviceCollection.AddSingleton<ResourceGenerationCalculator>();
        serviceCollection.AddSingleton<StrengthCalculator>();
        serviceCollection.AddSingleton<ThornsCalculator>();
        serviceCollection.AddSingleton<TotalDamageMultiplierCalculator>();
        serviceCollection.AddSingleton<TwoHandedWeaponDamageMultiplicativeCalculator>();
        serviceCollection.AddSingleton<VulnerableDamageBonusCalculator>();
        serviceCollection.AddSingleton<WillpowerCalculator>();

        // Other
        serviceCollection.AddSingleton(new RandomGenerator(123));
        serviceCollection.AddSingleton<EventPublisher>();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        var config = CreateConfig(serviceProvider);

        var sim = new Simulation(config, serviceProvider.GetRequiredService<EventPublisher>());
        var state = sim.Run();

        ReportResults(state);
    }

    private static T CreateAspect<T>(IServiceProvider serviceProvider) where T : Aspect => serviceProvider.GetRequiredService<T>();

    private static SimulationConfig CreateConfig(IServiceProvider sp)
    {
        var config = new SimulationConfig
        {
            Rotation = sp.GetRequiredService<SpinToWin>()
        };

        config.EnemySettings.Life = 40000000;
        config.EnemySettings.NumberOfEnemies = 3;
        config.EnemySettings.Level = 100;
        config.EnemySettings.IsElite = true;

        config.PlayerSettings.Level = 100;
        config.PlayerSettings.ExpertiseTechnique = Expertise.TwoHandedAxe;

        config.PlayerSettings.SkillWeapons.Add(Skill.LungingStrike, config.Gear.TwoHandSlashing);
        config.PlayerSettings.SkillWeapons.Add(Skill.Whirlwind, config.Gear.TwoHandSlashing);

        config.Skills.Add(Skill.LungingStrike, 1);
        config.Skills.Add(Skill.EnhancedLungingStrike, 1);
        config.Skills.Add(Skill.CombatLungingStrike, 1);
        config.Skills.Add(Skill.BattleLungingStrike, 1);
        config.Skills.Add(Skill.Whirlwind, 5);
        config.Skills.Add(Skill.EnhancedWhirlwind, 1);
        config.Skills.Add(Skill.FuriousWhirlwind, 1);
        config.Skills.Add(Skill.ViolentWhirlwind, 1);
        config.Skills.Add(Skill.PressurePoint, 3);
        config.Skills.Add(Skill.RallyingCry, 5);
        config.Skills.Add(Skill.EnhancedRallyingCry, 1);
        config.Skills.Add(Skill.TacticalRallyingCry, 1);
        config.Skills.Add(Skill.StrategicRallyingCry, 1);
        config.Skills.Add(Skill.ChallengingShout, 5);
        config.Skills.Add(Skill.EnhancedChallengingShout, 1);
        config.Skills.Add(Skill.TacticalChallengingShout, 1);
        config.Skills.Add(Skill.StrategicChallengingShout, 1);
        config.Skills.Add(Skill.WarCry, 5);
        config.Skills.Add(Skill.EnhancedWarCry, 1);
        config.Skills.Add(Skill.MightyWarCry, 1);
        config.Skills.Add(Skill.PowerWarCry, 1);
        config.Skills.Add(Skill.BoomingVoice, 3);
        config.Skills.Add(Skill.GutteralYell, 3);
        config.Skills.Add(Skill.RaidLeader, 3);
        config.Skills.Add(Skill.UnbridledRage, 1);
        config.Skills.Add(Skill.Hamstring, 1);
        config.Skills.Add(Skill.InvigoratingFury, 3);
        config.Skills.Add(Skill.TemperedFury, 3);
        config.Skills.Add(Skill.ProlificFury, 3);
        config.Skills.Add(Skill.AggressiveResistance, 3);
        config.Skills.Add(Skill.HeavyHanded, 3);
        config.Skills.Add(Skill.PitFighter, 3);
        config.Skills.Add(Skill.WrathOfTheBerserker, 1);
        config.Skills.Add(Skill.PrimeWrathOfTheBerserker, 1);
        config.Skills.Add(Skill.SupremeWrathOfTheBerserker, 1);

        config.Gear.Helm.Armor = 904;
        config.Gear.Helm.CooldownReduction = 11.0;
        config.Gear.Helm.PoisonResistance = 45.2;
        config.Gear.Helm.TotalArmor = 6.8;
        config.Gear.Helm.MaxLife = 472;
        config.Gear.Helm.Aspect = CreateAspect<AspectOfTheProtector>(sp);
        (config.Gear.Helm.Aspect as AspectOfTheProtector).BarrierAmount = 2000;
        config.Gear.Helm.Gems.Add(new RoyalSapphire());

        config.Gear.Chest.Armor = 1195;
        config.Gear.Chest.PhysicalDamage = 10.5;
        config.Gear.Chest.CritChancePhysicalAgainstElites = 6.0;
        config.Gear.Chest.DamageReductionFromBleeding = 14.6;
        config.Gear.Chest.Thorns = 403;
        config.Gear.Chest.Aspect = CreateAspect<RageOfHarrogath>(sp);
        (config.Gear.Chest.Aspect as RageOfHarrogath).Chance = 26;
        config.Gear.Chest.Gems.Add(new RoyalSapphire());
        config.Gear.Chest.Gems.Add(new RoyalSapphire());

        config.Gear.Gloves.Armor = 354;
        config.Gear.Gloves.AttackSpeed = 10.6;
        config.Gear.Gloves.LuckyHitChance = 10.2;
        config.Gear.Gloves.NonPhysicalDamage = 18.0;
        config.Gear.Gloves.Whirlwind = 3;
        config.Gear.Gloves.Aspect = CreateAspect<GohrsDevastatingGrips>(sp);
        (config.Gear.Gloves.Aspect as GohrsDevastatingGrips).DamagePercent = 24; ;

        config.Gear.Pants.Armor = 729;
        config.Gear.Pants.PotionSpeedWhileInjured = 45;
        config.Gear.Pants.WarCry = 4;
        config.Gear.Pants.Dodge = 7.4;
        config.Gear.Pants.DamageReductionFromClose = 23.0;
        config.Gear.Pants.DamageReductionWhileInjured = 39.5;
        config.Gear.Pants.Aspect = CreateAspect<AspectOfNumbingWraith>(sp);
        (config.Gear.Pants.Aspect as AspectOfNumbingWraith).Fortify = 12;
        config.Gear.Pants.Gems.Add(new RoyalSapphire());
        config.Gear.Pants.Gems.Add(new RoyalSapphire());

        config.Gear.Boots.Armor = 344;
        config.Gear.Boots.AttacksReduceEvadeCooldown = 1.2;
        config.Gear.Boots.MovementAfterKillingElite = 22.5;
        config.Gear.Boots.MovementSpeed = 15.8;
        config.Gear.Boots.DodgeAgainstDistant = 6.4;
        config.Gear.Boots.Dexterity = 51;
        config.Gear.Boots.Aspect = CreateAspect<GhostwalkerAspect>(sp);
        (config.Gear.Boots.Aspect as GhostwalkerAspect).Speed = 10;

        config.Gear.TwoHandBludgeoning.DPS = 2199;
        config.Gear.TwoHandBludgeoning.Expertise = Expertise.TwoHandedMace;
        config.Gear.TwoHandBludgeoning.MinDamage = 1955;
        config.Gear.TwoHandBludgeoning.MaxDamage = 2933;
        config.Gear.TwoHandBludgeoning.AttacksPerSecond = 0.9;
        config.Gear.TwoHandBludgeoning.OverpowerDamage = 94.5;
        config.Gear.TwoHandBludgeoning.DamageToClose = 57.0;
        config.Gear.TwoHandBludgeoning.CoreDamage = 58.5;
        config.Gear.TwoHandBludgeoning.Strength = 156;
        config.Gear.TwoHandBludgeoning.CritDamage = 57.0;
        config.Gear.TwoHandBludgeoning.Aspect = CreateAspect<AspectOfLimitlessRage>(sp);
        (config.Gear.TwoHandBludgeoning.Aspect as AspectOfLimitlessRage).Damage = 4;
        (config.Gear.TwoHandBludgeoning.Aspect as AspectOfLimitlessRage).MaxDamage = 60;
        config.Gear.TwoHandBludgeoning.Gems.Add(new RoyalEmerald());
        config.Gear.TwoHandBludgeoning.Gems.Add(new RoyalEmerald());

        config.Gear.OneHandLeft.DPS = 645;
        config.Gear.OneHandLeft.Expertise = Expertise.OneHandedSword;
        config.Gear.OneHandLeft.MinDamage = 470;
        config.Gear.OneHandLeft.MaxDamage = 704;
        config.Gear.OneHandLeft.AttacksPerSecond = 1.1;
        config.Gear.OneHandLeft.CritDamage = 18.8;
        config.Gear.OneHandLeft.VulnerableDamage = 21.0;
        config.Gear.OneHandLeft.Strength = 77;
        config.Gear.OneHandLeft.DamageToInjured = 34.5;
        config.Gear.OneHandLeft.CoreDamage = 18.8;
        config.Gear.OneHandLeft.Aspect = CreateAspect<ConceitedAspect>(sp);
        (config.Gear.OneHandLeft.Aspect as ConceitedAspect).Damage = 25;
        config.Gear.OneHandLeft.Gems.Add(new RoyalEmerald());

        config.Gear.OneHandRight.DPS = 650;
        config.Gear.OneHandRight.Expertise = Expertise.OneHandedSword;
        config.Gear.OneHandRight.MinDamage = 473;
        config.Gear.OneHandRight.MaxDamage = 709;
        config.Gear.OneHandRight.AttacksPerSecond = 1.1;
        config.Gear.OneHandRight.CritDamage = 18.8;
        config.Gear.OneHandRight.DamageToClose = 24.8;
        config.Gear.OneHandRight.AllStats = 30;
        config.Gear.OneHandRight.DamageToSlowed = 22.5;
        config.Gear.OneHandRight.DamageToCrowdControlled = 13.5;
        config.Gear.OneHandRight.Aspect = CreateAspect<AspectOfBerserkRipping>(sp);
        (config.Gear.OneHandRight.Aspect as AspectOfBerserkRipping).Damage = 30;
        config.Gear.OneHandRight.Gems.Add(new RoyalEmerald());

        config.Gear.TwoHandSlashing.DPS = 2465;
        config.Gear.TwoHandSlashing.Expertise = Expertise.TwoHandedSword;
        config.Gear.TwoHandSlashing.MinDamage = 1972;
        config.Gear.TwoHandSlashing.MaxDamage = 2958;
        config.Gear.TwoHandSlashing.AttacksPerSecond = 1.0;
        config.Gear.TwoHandSlashing.CritDamage = 52.5;
        config.Gear.TwoHandSlashing.VulnerableDamage = 69.0;
        config.Gear.TwoHandSlashing.DamageToClose = 57.0;
        config.Gear.TwoHandSlashing.Strength = 174;
        config.Gear.TwoHandSlashing.DamageToInjured = 93.0;
        config.Gear.TwoHandSlashing.Aspect = CreateAspect<AspectOfTheDireWhirlwind>(sp);
        (config.Gear.TwoHandSlashing.Aspect as AspectOfTheDireWhirlwind).CritChance = 14;
        (config.Gear.TwoHandSlashing.Aspect as AspectOfTheDireWhirlwind).MaxCritChance = 42;
        config.Gear.TwoHandSlashing.Gems.Add(new RoyalEmerald());
        config.Gear.TwoHandSlashing.Gems.Add(new RoyalEmerald());

        config.Gear.Amulet.ResistanceToAll = 24.1;
        config.Gear.Amulet.DamageReductionFromDistant = 19.6;
        config.Gear.Amulet.HealingReceived = 12.8;
        config.Gear.Amulet.DamageReductionFromClose = 12.5;
        config.Gear.Amulet.FuryCostReduction = 16.7;
        config.Gear.Amulet.Aspect = CreateAspect<EdgemastersAspect>(sp);
        (config.Gear.Amulet.Aspect as EdgemastersAspect).Damage = 29;
        config.Gear.Amulet.Gems.Add(new RoyalSkull());

        config.Gear.Ring1.LightningResistance = 35.0;
        config.Gear.Ring1.FireResistance = 35.0;
        config.Gear.Ring1.ResourceGeneration = 15.4;
        config.Gear.Ring1.DamageToSlowed = 31.5;
        config.Gear.Ring1.CritChance = 6.9;
        config.Gear.Ring1.CritDamage = 23.3;
        config.Gear.Ring1.Aspect = CreateAspect<BoldChieftainsAspect>(sp);
        (config.Gear.Ring1.Aspect as BoldChieftainsAspect).CooldownReduction = 1.9;
        config.Gear.Ring1.Gems.Add(new RoyalSkull());

        config.Gear.Ring2.ColdResistance = 32.2;
        config.Gear.Ring2.LightningResistance = 32.2;
        config.Gear.Ring2.DamageToClose = 18.8;
        config.Gear.Ring2.CritChance = 6.0;
        config.Gear.Ring2.DamageToSlowed = 20.3;
        config.Gear.Ring2.CritDamage = 16.5;
        config.Gear.Ring2.Aspect = CreateAspect<AspectOfEchoingFury>(sp);
        (config.Gear.Ring2.Aspect as AspectOfEchoingFury).Fury = 4;
        config.Gear.Ring2.Gems.Add(new RoyalSkull());

        // Paragon Board 1 (Starter)
        config.Paragon.Dexterity += 36;
        config.Paragon.Willpower += 15;
        config.Paragon.Strength += 60;
        config.Paragon.Intelligence += 5;
        config.Paragon.Armor += 738;
        config.Paragon.MaxLifePercent += 20;
        config.Paragon.PhysicalDamage += 84;
        config.Paragon.TwoHandWeaponDamageMultiplicative += 8;

        // Paragon Board 2 (Warbringer)
        config.Paragon.Dexterity += 74;
        config.Paragon.Willpower += 45;
        config.Paragon.Strength += 100;
        config.Paragon.Intelligence += 10;
        config.Paragon.PhysicalDamage += 35;
        config.Paragon.FuryOnKill += 4;
        config.Paragon.MaxFury += 24;
        config.ParagonNodes.Add(ParagonNode.Warbringer);
        config.ParagonNodes.Add(ParagonNode.Exploit);

        // Paragon Board 3 (Flawless Technique)
        config.Paragon.Dexterity += 22;
        config.Paragon.Willpower += 69;
        config.Paragon.Strength += 65;
        config.Paragon.Intelligence += 15;
        config.Paragon.DamageReductionWhileFortified += 387.1;
        config.Paragon.PhysicalDamage += 20;
        config.Paragon.CritDamage += 52.5;
        config.Paragon.DamageReductionFromClose += 15.9;
        config.ParagonNodes.Add(ParagonNode.Undaunted);

        // Paragon Board 4 (Decimator)
        config.Paragon.Dexterity += 15;
        config.Paragon.Willpower += 45;
        config.Paragon.Strength += 100;
        config.Paragon.Intelligence += 25;
        config.Paragon.DamageReductionFromVulnerable += 21.5;
        config.Paragon.VulnerableDamage += 88.75;
        config.Paragon.Armor += 200;
        config.Paragon.PhysicalDamage += 35;
        config.Paragon.MaxLifePercent += 8;
        config.ParagonNodes.Add(ParagonNode.Marshal);

        // Paragon Board 5 (Bone Breaker)
        config.Paragon.Dexterity += 59;
        config.Paragon.Willpower += 20;
        config.Paragon.Strength += 45;
        config.Paragon.Intelligence += 5;
        config.Paragon.CritDamage += 362.6;
        config.Paragon.DamageReductionWhileHealthy += 17.5;
        config.ParagonNodes.Add(ParagonNode.Wrath);

        // Paragon Board 6 (Weapons Master)
        config.Paragon.Dexterity += 34;
        config.Paragon.Willpower += 25;
        config.Paragon.Strength += 45;
        config.Paragon.Intelligence += 5;
        config.Paragon.PhysicalDamage += 35;
        config.Paragon.Armor += 350;
        config.Paragon.DamageToClose += 287.1;
        config.Paragon.DamageReductionFromClose += 10;

        return config;
    }

    private static void ReportResults(SimulationState state)
    {
        foreach (var e in state.ProcessedEvents)
        {
            Console.WriteLine(e);
        }

        Console.WriteLine("");
        Console.WriteLine($"Total Events: {state.ProcessedEvents.Count}");

        var hits = state.ProcessedEvents.OfType<DamageEvent>().Where(x => x.DamageType.HasFlag(DamageType.Direct) && !x.DamageType.HasFlag(DamageType.CriticalStrike));
        var crits = state.ProcessedEvents.OfType<DamageEvent>().Where(x => x.DamageType.HasFlag(DamageType.Direct) & x.DamageType.HasFlag(DamageType.CriticalStrike));

        var avgHit = hits.Average(x => x.Damage);
        var avgCrit = crits.Average(x => x.Damage);

        var critBonus = (avgCrit - avgHit) / avgHit * 100;

        var totalTime = state.ProcessedEvents.Max(x => x.Timestamp);
        var totalDamage = state.ProcessedEvents.Where(x => x is DamageEvent).Cast<DamageEvent>().Sum(x => x.Damage);
        var dps = totalDamage / totalTime;

        Console.WriteLine("");
        Console.WriteLine($"Total Time: {totalTime:F1} seconds");
        Console.WriteLine($"Total Damage: {totalDamage:N0}");
        Console.WriteLine($"DPS: {dps:N0}");

        Console.WriteLine("");
        Console.WriteLine($"Hits: {hits.Count()}");
        Console.WriteLine($"Crits: {crits.Count()}");
        Console.WriteLine($"Avg Hit: {avgHit:N0}");
        Console.WriteLine($"Avg Crit: {avgCrit:N0}");
        Console.WriteLine($"Crit Bonus: {critBonus:F1}%");

        Console.WriteLine("");
        var (count, uptime, percentage) = GetBarrierStats(state.ProcessedEvents);
        Console.WriteLine($"Barriers: Applied {count} times for {uptime:F1} seconds ({percentage:F1}%)");

        (count, uptime, percentage) = GetVulnerableStats(state);
        Console.WriteLine($"Vulnerable: Applied {count} times across {state.Enemies.Count} enemies, for a average uptime of {uptime:F1} seconds ({percentage:F1}%)");

        (count, uptime, percentage) = GetBerserkingStats(state.ProcessedEvents);
        Console.WriteLine($"Berserking: Applied {count} times for {uptime:F1} seconds ({percentage:F1}%)");

        (count, uptime, percentage) = GetBleedingStats(state);
        Console.WriteLine($"Bleeding: Applied {count} times across {state.Enemies.Count} enemies, for a average uptime of {uptime:F1} seconds ({percentage:F1}%)");

        (count, uptime, percentage) = GetGutteralYellStats(state.ProcessedEvents);
        Console.WriteLine($"Gutteral Yell: Applied {count} times for {uptime:F1} seconds ({percentage:F1}%)");

        var lungingStrikeCount = state.ProcessedEvents.Count(e => e is LungingStrikeEvent);
        var whirlwindCount = state.ProcessedEvents.Count(e => e is WhirlwindSpinEvent);
        var rallyingCryCount = state.ProcessedEvents.Count(e => e is RallyingCryEvent);
        var warCryCount = state.ProcessedEvents.Count(e => e is WarCryEvent);
        var challengingShoutCount = state.ProcessedEvents.Count(e => e is ChallengingShoutEvent);

        var aspectOfTheProtectorCount = state.ProcessedEvents.Count(e => e is AspectOfTheProtectorProcEvent);
        var gohrsCount = state.ProcessedEvents.Count(e => e is GohrsDevastatingGripsProcEvent);
        var pressurePointCount = state.ProcessedEvents.Count(e => e is PressurePointProcEvent);

        Console.WriteLine("");
        Console.WriteLine($"Lunging Strikes: {lungingStrikeCount}");
        Console.WriteLine($"Whirlwinds: {whirlwindCount}");
        Console.WriteLine($"Rallying Cries: {rallyingCryCount}");
        Console.WriteLine($"War Cries: {warCryCount}");
        Console.WriteLine($"Challenging Shouts: {challengingShoutCount}");

        Console.WriteLine("");
        Console.WriteLine($"Aspect of the Protector Procs: {aspectOfTheProtectorCount}");
        Console.WriteLine($"Gohrs Devastating Grips Procs: {gohrsCount}");
        Console.WriteLine($"Pressure Point Procs: {pressurePointCount}");

        var lungingStrikeDamage = state.ProcessedEvents.OfType<DamageEvent>().Where(e => e.DamageSource == DamageSource.LungingStrike).Sum(e => e.Damage);
        var whirlwindDamage = state.ProcessedEvents.OfType<DamageEvent>().Where(e => e.DamageSource == DamageSource.Whirlwind).Sum(e => e.Damage);
        var gohrsDamage = state.ProcessedEvents.OfType<DamageEvent>().Where(e => e.DamageSource == DamageSource.GohrsDevastatingGrips).Sum(e => e.Damage);
        var bleedingDamage = state.ProcessedEvents.OfType<DamageEvent>().Where(e => e.DamageSource == DamageSource.Bleeding).Sum(e => e.Damage);

        Console.WriteLine("");
        Console.WriteLine($"Lunging Strike Damage: {lungingStrikeDamage:N0} [{100 * lungingStrikeDamage / totalDamage:F1}%]");
        Console.WriteLine($"Whirlwind Damage: {whirlwindDamage:N0} [{100 * whirlwindDamage / totalDamage:F1}%]");
        Console.WriteLine($"Gohrs Devastating Grips Damage: {gohrsDamage:N0} [{100 * gohrsDamage / totalDamage:F1}%]");
        Console.WriteLine($"Bleeding Damage: {bleedingDamage:N0} [{100 * bleedingDamage / totalDamage:F1}%]");

        var furyGenerated = state.ProcessedEvents.OfType<FuryGeneratedEvent>().Sum(e => e.FuryGenerated);
        var furySpent = state.ProcessedEvents.OfType<FurySpentEvent>().Sum(e => e.FurySpent);

        Console.WriteLine("");
        Console.WriteLine($"Total Fury Generated: {furyGenerated:N0}");
        Console.WriteLine($"Total Fury Spent: {furySpent:N0}");
    }

    private static (int count, double uptime, double percentage) GetBarrierStats(IEnumerable<EventInfo> events)
    {
        var count = events.Where(x => x is BarrierAppliedEvent).Count();

        var barrierEvents = events.Where(x => x is BarrierAppliedEvent or BarrierExpiredEvent);

        var timestamp = 0.0;
        var uptime = 0.0;
        var activeCount = 0;

        foreach (var e in barrierEvents)
        {
            if (activeCount > 0)
            {
                uptime += e.Timestamp - timestamp;
            }

            timestamp = e.Timestamp;

            if (e is BarrierAppliedEvent)
            {
                activeCount++;
            }

            if (e is BarrierExpiredEvent)
            {
                activeCount--;
            }
        }

        if (activeCount > 0)
        {
            uptime += events.Max(x => x.Timestamp) - timestamp;
        }

        var percentage = uptime / events.Max(x => x.Timestamp) * 100;

        return (count, uptime, percentage);
    }

    private static (int count, double uptime, double percentage) GetVulnerableStats(SimulationState state)
    {
        var totalCount = 0;
        var totalUptime = 0.0;
        var totalPercentage = 0.0;

        var vulnerableEvents = state.ProcessedEvents.Where(x => (x is AuraAppliedEvent appliedEvent && appliedEvent.Aura == Aura.Vulnerable) || (x is AuraExpiredEvent expiredEvent && expiredEvent.Aura == Aura.Vulnerable)).ToList();
        var endOfFight = state.ProcessedEvents.Max(x => x.Timestamp);

        foreach (var enemy in state.Enemies)
        {
            var count = vulnerableEvents.Where(x => x is AuraAppliedEvent appliedEvent && appliedEvent.Aura == Aura.Vulnerable && appliedEvent.Target == enemy).Count();

            var enemyEvents = vulnerableEvents.Where(x => (x is AuraAppliedEvent appliedEvent && appliedEvent.Aura == Aura.Vulnerable && appliedEvent.Target == enemy) ||
                                                           (x is AuraExpiredEvent expiredEvent && expiredEvent.Aura == Aura.Vulnerable && expiredEvent.Target == enemy));

            var timestamp = 0.0;
            var uptime = 0.0;
            var active = false;

            foreach (var e in enemyEvents)
            {
                if (active)
                {
                    uptime += e.Timestamp - timestamp;
                }

                timestamp = e.Timestamp;

                if (e is AuraAppliedEvent)
                {
                    active = true;
                }

                if (e is AuraExpiredEvent)
                {
                    active = false;
                }
            }

            if (active)
            {
                uptime += endOfFight - timestamp;
            }

            var percentage = uptime / endOfFight * 100;

            totalCount += count;
            totalUptime += uptime; ;
            totalPercentage += percentage;
        }

        return (totalCount, totalUptime / state.Enemies.Count, totalPercentage / state.Enemies.Count);
    }

    private static (int count, double uptime, double percentage) GetBerserkingStats(IEnumerable<EventInfo> events)
    {
        var count = events.Where(x => x is AuraAppliedEvent appliedEvent && appliedEvent.Aura == Aura.Berserking).Count();

        var berserkingEvents = events.Where(x => (x is AuraAppliedEvent appliedEvent && appliedEvent.Aura == Aura.Berserking) || (x is AuraExpiredEvent expiredEvent && expiredEvent.Aura == Aura.Berserking));
        var endOfFight = events.Max(x => x.Timestamp);

        var timestamp = 0.0;
        var uptime = 0.0;
        var active = false;

        foreach (var e in berserkingEvents)
        {
            if (active)
            {
                uptime += e.Timestamp - timestamp;
            }

            timestamp = e.Timestamp;

            if (e is AuraAppliedEvent)
            {
                active = true;
            }

            if (e is AuraExpiredEvent)
            {
                active = false;
            }
        }

        if (active)
        {
            uptime += endOfFight - timestamp;
        }

        var percentage = uptime / endOfFight * 100;

        return (count, uptime, percentage);
    }

    private static (int count, double uptime, double percentage) GetGutteralYellStats(IEnumerable<EventInfo> events)
    {
        var count = events.Where(x => x is GutteralYellProcEvent).Count();

        var gutteralYellEvents = events.Where(x => x is GutteralYellProcEvent || (x is AuraExpiredEvent expiredEvent && expiredEvent.Aura == Aura.GutteralYell));
        var endOfFight = events.Max(x => x.Timestamp);

        var timestamp = 0.0;
        var uptime = 0.0;
        var active = false;

        foreach (var e in gutteralYellEvents)
        {
            if (active)
            {
                uptime += e.Timestamp - timestamp;
            }

            timestamp = e.Timestamp;

            if (e is GutteralYellProcEvent)
            {
                active = true;
            }

            if (e is AuraExpiredEvent)
            {
                active = false;
            }
        }

        if (active)
        {
            uptime += endOfFight - timestamp;
        }

        var percentage = uptime / endOfFight * 100;

        return (count, uptime, percentage);
    }

    private static (int count, double uptime, double percentage) GetBleedingStats(SimulationState state)
    {
        var totalCount = 0;
        var totalUptime = 0.0;
        var totalPercentage = 0.0;

        var vulnerableEvents = state.ProcessedEvents.Where(x => x is BleedAppliedEvent or BleedCompletedEvent).ToList();
        var endOfFight = state.ProcessedEvents.Max(x => x.Timestamp);

        foreach (var enemy in state.Enemies)
        {
            var count = vulnerableEvents.Where(x => x is BleedAppliedEvent && (x as BleedAppliedEvent).Target == enemy).Count();

            var enemyEvents = vulnerableEvents.Where(x => (x is BleedAppliedEvent && (x as BleedAppliedEvent).Target == enemy) ||
                                                           (x is BleedCompletedEvent && (x as BleedCompletedEvent).Target == enemy));

            var timestamp = 0.0;
            var uptime = 0.0;
            var activeCount = 0;

            foreach (var e in enemyEvents)
            {
                if (activeCount > 0)
                {
                    uptime += e.Timestamp - timestamp;
                }

                timestamp = e.Timestamp;

                if (e is BleedAppliedEvent)
                {
                    activeCount++;
                }

                if (e is BleedCompletedEvent)
                {
                    activeCount--;
                }
            }

            if (activeCount > 0)
            {
                uptime += endOfFight - timestamp;
            }

            var percentage = uptime / endOfFight * 100;

            totalCount += count;
            totalUptime += uptime; ;
            totalPercentage += percentage;
        }

        return (totalCount, totalUptime / state.Enemies.Count, totalPercentage / state.Enemies.Count);
    }
}
