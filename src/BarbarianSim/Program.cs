﻿using BarbarianSim.Abilities;
using BarbarianSim.Aspects;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.Gems;

namespace BarbarianSim;

internal class Program
{
    private static void Main()
    {
        var config = CreateConfig();

        RandomGenerator.Seed(123);

        var sim = new Simulation(config);
        var state = sim.Run();

        ReportResults(state);
    }

    private static SimulationConfig CreateConfig()
    {
        var config = new SimulationConfig();

        config.EnemySettings.Life = 2000000;
        config.EnemySettings.NumberOfEnemies = 3;
        config.EnemySettings.Level = 100;
        config.EnemySettings.IsElite = true;

        config.PlayerSettings.Level = 100;
        config.PlayerSettings.ExpertiseTechnique = Expertise.TwoHandedAxe;

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
        config.Gear.Helm.Aspect = new AspectOfTheProtector(2000);
        config.Gear.Helm.Gems.Add(new RoyalSapphire());

        config.Gear.Chest.Armor = 1195;
        config.Gear.Chest.PhysicalDamage = 10.5;
        config.Gear.Chest.CritChancePhysicalAgainstElites = 6.0;
        config.Gear.Chest.DamageReductionFromBleeding = 14.6;
        config.Gear.Chest.Thorns = 403;
        config.Gear.Chest.Aspect = new RageOfHarrogath(26);
        config.Gear.Chest.Gems.Add(new RoyalSapphire());
        config.Gear.Chest.Gems.Add(new RoyalSapphire());

        config.Gear.Gloves.Armor = 354;
        config.Gear.Gloves.AttackSpeed = 10.6;
        config.Gear.Gloves.LuckyHitChance = 10.2;
        config.Gear.Gloves.NonPhysicalDamage = 18.0;
        config.Gear.Gloves.Whirlwind = 3;
        config.Gear.Gloves.Aspect = new GohrsDevastatingGrips(24);

        config.Gear.Pants.Armor = 729;
        config.Gear.Pants.PotionSpeedWhileInjured = 45;
        config.Gear.Pants.WarCry = 4;
        config.Gear.Pants.Dodge = 7.4;
        config.Gear.Pants.DamageReductionFromClose = 23.0;
        config.Gear.Pants.DamageReductionWhileInjured = 39.5;
        config.Gear.Pants.Aspect = new AspectOfNumbingWraith(12);
        config.Gear.Pants.Gems.Add(new RoyalSapphire());
        config.Gear.Pants.Gems.Add(new RoyalSapphire());

        config.Gear.Boots.Armor = 344;
        config.Gear.Boots.AttacksReduceEvadeCooldown = 1.2;
        config.Gear.Boots.MovementAfterKillingElite = 22.5;
        config.Gear.Boots.MovementSpeed = 15.8;
        config.Gear.Boots.DodgeAgainstDistant = 6.4;
        config.Gear.Boots.Dexterity = 51;
        config.Gear.Boots.Aspect = new GhostwalkerAspect(10);

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
        config.Gear.TwoHandBludgeoning.Aspect = new AspectOfLimitlessRage
        {
            Damage = 4,
            MaxDamage = 60
        };
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
        config.Gear.OneHandLeft.Aspect = new ConceitedAspect(25);
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
        config.Gear.OneHandRight.Aspect = new AspectOfBerserkRipping(30);
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
        config.Gear.TwoHandSlashing.Aspect = new AspectOfTheDireWhirlwind
        {
            CritChance = 14,
            MaxCritChance = 42
        };
        config.Gear.TwoHandSlashing.Gems.Add(new RoyalEmerald());
        config.Gear.TwoHandSlashing.Gems.Add(new RoyalEmerald());

        config.Gear.Amulet.ResistanceToAll = 24.1;
        config.Gear.Amulet.DamageReductionFromDistant = 19.6;
        config.Gear.Amulet.HealingReceived = 12.8;
        config.Gear.Amulet.DamageReductionFromClose = 12.5;
        config.Gear.Amulet.FuryCostReduction = 16.7;
        config.Gear.Amulet.Aspect = new EdgemastersAspect(29);
        config.Gear.Amulet.Gems.Add(new RoyalSkull());

        config.Gear.Ring1.LightningResistance = 35.0;
        config.Gear.Ring1.FireResistance = 35.0;
        config.Gear.Ring1.ResourceGeneration = 15.4;
        config.Gear.Ring1.DamageToSlowed = 31.5;
        config.Gear.Ring1.CritChance = 6.9;
        config.Gear.Ring1.CritDamage = 23.3;
        config.Gear.Ring1.Aspect = new BoldChieftainsAspect(1.9);
        config.Gear.Ring1.Gems.Add(new RoyalSkull());

        config.Gear.Ring2.ColdResistance = 32.2;
        config.Gear.Ring2.LightningResistance = 32.2;
        config.Gear.Ring2.DamageToClose = 18.8;
        config.Gear.Ring2.CritChance = 6.0;
        config.Gear.Ring2.DamageToSlowed = 20.3;
        config.Gear.Ring2.CritDamage = 16.5;
        config.Gear.Ring2.Aspect = new AspectOfEchoingFury(4);
        config.Gear.Ring2.Gems.Add(new RoyalSkull());

        LungingStrike.Weapon = config.Gear.TwoHandSlashing;
        Whirlwind.Weapon = config.Gear.TwoHandSlashing;

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

        var hits = state.ProcessedEvents.Where(x => x is DamageEvent).Cast<DamageEvent>().Where(x => x.DamageType == DamageType.Direct);
        var crits = state.ProcessedEvents.Where(x => x is DamageEvent).Cast<DamageEvent>().Where(x => x.DamageType == DamageType.DirectCrit);

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

        var vulnerableEvents = state.ProcessedEvents.Where(x => x is VulnerableAppliedEvent || (x is AuraExpiredEvent expiredEvent && expiredEvent.Aura == Aura.Vulnerable)).ToList();
        var endOfFight = state.ProcessedEvents.Max(x => x.Timestamp);

        foreach (var enemy in state.Enemies)
        {
            var count = vulnerableEvents.Where(x => x is VulnerableAppliedEvent && (x as VulnerableAppliedEvent).Target == enemy).Count();

            var enemyEvents = vulnerableEvents.Where(x => (x is VulnerableAppliedEvent appliedEvent && appliedEvent.Target == enemy) ||
                                                           (x is AuraExpiredEvent expiredEvent && expiredEvent.Target == enemy));

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

                if (e is VulnerableAppliedEvent)
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
