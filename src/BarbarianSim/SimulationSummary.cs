using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim;

public class SimulationSummary
{
    // Summary
    public int TotalEvents { get; init; }
    public double TotalTime { get; init; }
    public double TotalDamage { get; init; }
    public double DPS { get; init; }
    public int Hits { get; init; }
    public int Crits { get; init; }
    public int Overpowers { get; init; }
    public int OverpowerCrits { get; init; }
    public int LuckyHits { get; init; }
    public double AverageHit { get; init; }
    public double AverageCrit { get; init; }
    public double AverageOverpower { get; init; }
    public double AverageOverpowerCrit { get; init; }
    public double CritBonusPercent { get; init; }
    public double OverpowerBonusPercent { get; init; }

    // Buffs / Debuffs
    public ICollection<(Aura Aura, int Count, double UptimePercentage)> EnemyEffects { get; init; } = new List<(Aura Aura, int Count, double UptimePercentage)>();
    public ICollection<(Aura Aura, int Count, double UptimePercentage)> PlayerEffects { get; init; } = new List<(Aura Aura, int Count, double UptimePercentage)>();

    // Ability Counts
    public int LungingStrikeCount { get; init; }
    public int WhirlwindSpinCount { get; init; }
    public int RallyingCryCount { get; init; }
    public int WarCryCount { get; init; }
    public int ChallengingShoutCount { get; init; }
    public int WrathOfTheBerserkerCount { get; init; }
    public int IronSkinCount { get; init; }

    // Procs
    public int AspectOfEchoingFuryProcCount { get; init; }
    public int AspectOfTheProtectorProcCount { get; init; }
    public int ExploitProcCount { get; init; }
    public int GohrsDevastatingGripsProcCount { get; init; }
    public int GutteralYellProcCount { get; init; }
    public int PressurePointProcCount { get; init; }
    public int RaidLeaderProcCount { get; init; }
    public int TwoHandedMaceExpertiseProcCount { get; init; }
    public int WarbringerProcCount { get; init; }

    // Damage Breakdown
    public double LungingStrikeDamage { get; init; }
    public double WhirlwindSpinDamage { get; init; }
    public double GohrsDevastatingGripsDamage { get; init; }
    public double BleedingDamage { get; init; }

    // Fury Management
    public double FuryGenerated { get; init; }
    public double FurySpent { get; init; }

    public SimulationSummary(SimulationState state)
    {
        TotalEvents = state.ProcessedEvents.Count;
        TotalTime = state.ProcessedEvents.Max(e => e.Timestamp);
        TotalDamage = state.ProcessedEvents.OfType<DamageEvent>().Sum(e => e.Damage);
        DPS = TotalDamage / TotalTime;

        Hits = state.ProcessedEvents.OfType<DamageEvent>().Count(e => e.DamageType.HasFlag(DamageType.Direct) && !e.DamageType.HasFlag(DamageType.CriticalStrike) && !e.DamageType.HasFlag(DamageType.Overpower));
        Crits = state.ProcessedEvents.OfType<DamageEvent>().Count(e => e.DamageType.HasFlag(DamageType.Direct) && e.DamageType.HasFlag(DamageType.CriticalStrike) && !e.DamageType.HasFlag(DamageType.Overpower));
        Overpowers = state.ProcessedEvents.OfType<DamageEvent>().Count(e => e.DamageType.HasFlag(DamageType.Direct) && !e.DamageType.HasFlag(DamageType.CriticalStrike) && e.DamageType.HasFlag(DamageType.Overpower));
        OverpowerCrits = state.ProcessedEvents.OfType<DamageEvent>().Count(e => e.DamageType.HasFlag(DamageType.Direct) && e.DamageType.HasFlag(DamageType.CriticalStrike) && e.DamageType.HasFlag(DamageType.Overpower));

        LuckyHits = state.ProcessedEvents.OfType<LuckyHitEvent>().Count();

        AverageHit = state.ProcessedEvents.OfType<DamageEvent>().Where(e => e.DamageType.HasFlag(DamageType.Direct) && !e.DamageType.HasFlag(DamageType.CriticalStrike) && !e.DamageType.HasFlag(DamageType.Overpower)).Average(e => e.Damage);
        AverageCrit = state.ProcessedEvents.OfType<DamageEvent>().Where(e => e.DamageType.HasFlag(DamageType.Direct) && e.DamageType.HasFlag(DamageType.CriticalStrike) && !e.DamageType.HasFlag(DamageType.Overpower)).Average(e => e.Damage);
        AverageOverpower = state.ProcessedEvents.OfType<DamageEvent>().Where(e => e.DamageType.HasFlag(DamageType.Direct) && !e.DamageType.HasFlag(DamageType.CriticalStrike) && e.DamageType.HasFlag(DamageType.Overpower)).Average(e => e.Damage);
        AverageOverpowerCrit = state.ProcessedEvents.OfType<DamageEvent>().Where(e => e.DamageType.HasFlag(DamageType.Direct) && e.DamageType.HasFlag(DamageType.CriticalStrike) && e.DamageType.HasFlag(DamageType.Overpower)).Average(e => e.Damage);

        CritBonusPercent = (AverageCrit - AverageHit) / AverageHit * 100;
        OverpowerBonusPercent = (AverageOverpower - AverageHit) / AverageHit * 100;

        foreach (var aura in Enum.GetValues<Aura>())
        {
            var (buffCount, buffUptimePercentage) = GetBuffStats(state, aura);

            if (buffCount > 0)
            {
                PlayerEffects.Add((aura, buffCount, buffUptimePercentage));
            }

            var (debuffCount, debuffUptimePercentage) = GetDebuffStats(state, aura);

            if (debuffCount > 0)
            {
                EnemyEffects.Add((aura, debuffCount, debuffUptimePercentage));
            }
        }

        var (Count, UptimePercentage) = GetBleedingStats(state);
        EnemyEffects.Add((Aura.Bleeding, Count, UptimePercentage));

        LungingStrikeCount = state.ProcessedEvents.OfType<LungingStrikeEvent>().Count();
        WhirlwindSpinCount = state.ProcessedEvents.OfType<WhirlwindSpinEvent>().Count();
        RallyingCryCount = state.ProcessedEvents.OfType<RallyingCryEvent>().Count();
        WarCryCount = state.ProcessedEvents.OfType<WarCryEvent>().Count();
        ChallengingShoutCount = state.ProcessedEvents.OfType<ChallengingShoutEvent>().Count();
        WrathOfTheBerserkerCount = state.ProcessedEvents.OfType<WrathOfTheBerserkerEvent>().Count();
        IronSkinCount = state.ProcessedEvents.OfType<IronSkinEvent>().Count();

        AspectOfEchoingFuryProcCount = state.ProcessedEvents.OfType<AspectOfEchoingFuryProcEvent>().Count();
        AspectOfTheProtectorProcCount = state.ProcessedEvents.OfType<AspectOfTheProtectorProcEvent>().Count();
        ExploitProcCount = state.ProcessedEvents.OfType<ExploitProcEvent>().Count();
        GohrsDevastatingGripsProcCount = state.ProcessedEvents.OfType<GohrsDevastatingGripsProcEvent>().Count();
        GutteralYellProcCount = state.ProcessedEvents.OfType<GutteralYellProcEvent>().Count();
        PressurePointProcCount = state.ProcessedEvents.OfType<PressurePointProcEvent>().Count();
        RaidLeaderProcCount = state.ProcessedEvents.OfType<RaidLeaderProcEvent>().Count();
        TwoHandedMaceExpertiseProcCount = state.ProcessedEvents.OfType<TwoHandedMaceExpertiseProcEvent>().Count();
        WarbringerProcCount = state.ProcessedEvents.OfType<WarbringerProcEvent>().Count();

        LungingStrikeDamage = state.ProcessedEvents.OfType<DamageEvent>().Where(e => e.DamageSource == DamageSource.LungingStrike).Sum(e => e.Damage);
        WhirlwindSpinDamage = state.ProcessedEvents.OfType<DamageEvent>().Where(e => e.DamageSource == DamageSource.Whirlwind).Sum(e => e.Damage);
        GohrsDevastatingGripsDamage = state.ProcessedEvents.OfType<DamageEvent>().Where(e => e.DamageSource == DamageSource.GohrsDevastatingGrips).Sum(e => e.Damage);
        BleedingDamage = state.ProcessedEvents.OfType<DamageEvent>().Where(e => e.DamageSource == DamageSource.Bleeding).Sum(e => e.Damage);

        FuryGenerated = state.ProcessedEvents.OfType<FuryGeneratedEvent>().Sum(e => e.FuryGenerated);
        FurySpent = state.ProcessedEvents.OfType<FurySpentEvent>().Sum(e => e.FurySpent);
    }

    public void Print()
    {
        Console.WriteLine("");
        Console.WriteLine($"Total Events: {TotalEvents}");

        Console.WriteLine("");
        Console.WriteLine($"Total Time: {TotalTime:F1} seconds");
        Console.WriteLine($"Total Damage: {TotalDamage:N0}");
        Console.WriteLine($"DPS: {DPS:N0}");

        Console.WriteLine("");
        Console.WriteLine($"Hits: {Hits}");
        Console.WriteLine($"Crits: {Crits}");
        Console.WriteLine($"Overpowers: {Overpowers}");
        Console.WriteLine($"Overpower Crits: {OverpowerCrits}");
        Console.WriteLine("");
        Console.WriteLine($"Avg Hit: {AverageHit:N0}");
        Console.WriteLine($"Avg Crit: {AverageCrit:N0}");
        Console.WriteLine($"Avg Overpower: {AverageOverpower:N0}");
        Console.WriteLine($"Avg Overpower Crit: {AverageOverpowerCrit:N0}");
        Console.WriteLine($"Crit Bonus: {CritBonusPercent:F1}%");
        Console.WriteLine($"Overpower Bonus: {OverpowerBonusPercent:F1}%");

        Console.WriteLine("");
        Console.WriteLine("Buffs");
        Console.WriteLine("========");

        foreach (var buff in PlayerEffects)
        {
            Console.WriteLine($"{buff.Aura}: Applied {buff.Count} times for an uptime of {buff.UptimePercentage:F1}%");
        }

        Console.WriteLine("");
        Console.WriteLine("Debuffs");
        Console.WriteLine("========");

        foreach (var debuff in EnemyEffects)
        {
            Console.WriteLine($"{debuff.Aura}: Applied {debuff.Count} times for an average uptime of {debuff.UptimePercentage:F1}%");
        }

        Console.WriteLine("");
        Console.WriteLine("Abilities Used");
        Console.WriteLine("==============");
        Console.WriteLine($"Lunging Strike: {LungingStrikeCount}");
        Console.WriteLine($"Whirlwind: {WhirlwindSpinCount}");
        Console.WriteLine($"Rallying Cry: {RallyingCryCount}");
        Console.WriteLine($"War Cry: {WarCryCount}");
        Console.WriteLine($"Challenging Shout: {ChallengingShoutCount}");
        Console.WriteLine($"Wrath of the Berserker: {WrathOfTheBerserkerCount}");
        Console.WriteLine($"Iron Skin: {IronSkinCount}");

        Console.WriteLine("");
        Console.WriteLine("Procs");
        Console.WriteLine("=====");
        Console.WriteLine($"Aspect of Echoing Fury: {AspectOfEchoingFuryProcCount}");
        Console.WriteLine($"Aspect of the Protector: {AspectOfTheProtectorProcCount}");
        Console.WriteLine($"Exploit: {ExploitProcCount}");
        Console.WriteLine($"Gohrs Devastating Grips: {GohrsDevastatingGripsProcCount}");
        Console.WriteLine($"Gutteral Yell: {GutteralYellProcCount}");
        Console.WriteLine($"Pressure Point: {PressurePointProcCount}");
        Console.WriteLine($"Raid Leader: {RaidLeaderProcCount}");
        Console.WriteLine($"2-Handed Mace Expertise: {TwoHandedMaceExpertiseProcCount}");
        Console.WriteLine($"Warbringer: {WarbringerProcCount}");

        Console.WriteLine("");
        Console.WriteLine("Damage Breakdown");
        Console.WriteLine("================");
        Console.WriteLine($"Lunging Strike Damage: {LungingStrikeDamage:N0} [{100 * LungingStrikeDamage / TotalDamage:F1}%]");
        Console.WriteLine($"Whirlwind Damage: {WhirlwindSpinDamage:N0} [{100 * WhirlwindSpinDamage / TotalDamage:F1}%]");
        Console.WriteLine($"Gohrs Devastating Grips Damage: {GohrsDevastatingGripsDamage:N0} [{100 * GohrsDevastatingGripsDamage / TotalDamage:F1}%]");
        Console.WriteLine($"Bleeding Damage: {BleedingDamage:N0} [{100 * BleedingDamage / TotalDamage:F1}%]");

        Console.WriteLine("");
        Console.WriteLine("Fury Management");
        Console.WriteLine("===============");
        Console.WriteLine($"Total Fury Generated: {FuryGenerated:N0}");
        Console.WriteLine($"Total Fury Spent: {FurySpent:N0}");
    }

    private (int Count, double UptimePercentage) GetBuffStats(SimulationState state, Aura aura)
    {
        var count = state.ProcessedEvents.OfType<AuraAppliedEvent>().Where(e => e.Target == null && e.Aura == aura).Count();

        var buffEvents = state.ProcessedEvents.Where(e => (e is AuraAppliedEvent appliedEvent && appliedEvent.Aura == aura && appliedEvent.Target == null)
                                                       || (e is AuraExpiredEvent expiredEvent && expiredEvent.Aura == aura && expiredEvent.Target == null))
                                              .OrderBy(e => e.Timestamp);

        var endOfFight = state.ProcessedEvents.Max(e => e.Timestamp);

        var timestamp = 0.0;
        var uptime = 0.0;
        var active = false;

        foreach (var e in buffEvents)
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

        var percentage = uptime / endOfFight * 100.0;

        return (count, percentage);
    }

    private (int Count, double UptimePercentage) GetDebuffStats(SimulationState state, Aura aura)
    {
        var count = state.ProcessedEvents.OfType<AuraAppliedEvent>().Where(e => e.Target != null && e.Aura == aura).Count();

        var totalPercentage = 0.0;

        var endOfFight = state.ProcessedEvents.Max(x => x.Timestamp);

        foreach (var enemy in state.Enemies)
        {
            var enemyEvents = state.ProcessedEvents.Where(e => (e is AuraAppliedEvent appliedEvent && appliedEvent.Aura == aura && appliedEvent.Target == enemy)
                                                            || (e is AuraExpiredEvent expiredEvent && expiredEvent.Aura == aura && expiredEvent.Target == enemy))
                                                   .OrderBy(e => e.Timestamp);

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

            var percentage = uptime / endOfFight * 100.0;

            totalPercentage += percentage;
        }

        return (count, totalPercentage / state.Enemies.Count);
    }

    private (int Count, double UptimePercentage) GetBleedingStats(SimulationState state)
    {
        var count = state.ProcessedEvents.OfType<BleedAppliedEvent>().Where(e => e.Target != null).Count();

        var totalPercentage = 0.0;

        var endOfFight = state.ProcessedEvents.Max(x => x.Timestamp);

        foreach (var enemy in state.Enemies)
        {
            var enemyEvents = state.ProcessedEvents.Where(e => (e is BleedAppliedEvent appliedEvent && appliedEvent.Target == enemy)
                                                            || (e is BleedCompletedEvent completedEvent && completedEvent.Target == enemy))
                                                   .OrderBy(e => e.Timestamp);

            var timestamp = 0.0;
            var uptime = 0.0;
            var active = 0;

            foreach (var e in enemyEvents)
            {
                if (active > 0)
                {
                    uptime += e.Timestamp - timestamp;
                }

                timestamp = e.Timestamp;

                if (e is BleedAppliedEvent)
                {
                    active++;
                }

                if (e is BleedCompletedEvent)
                {
                    active--;
                }
            }

            if (active > 0)
            {
                uptime += endOfFight - timestamp;
            }

            var percentage = uptime / endOfFight * 100.0;

            totalPercentage += percentage;
        }

        return (count, totalPercentage / state.Enemies.Count);
    }
}
