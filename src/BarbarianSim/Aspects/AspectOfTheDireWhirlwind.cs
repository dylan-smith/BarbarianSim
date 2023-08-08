using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Aspects;

public class AspectOfTheDireWhirlwind : Aspect
{
    // Whirlwind's Critical Strike chance is increased by 3-8%[+] for each second it is channeled, up to 9-24%[+]
    public int CritChance { get; set; }
    public int MaxCritChance { get; set; }

    public AspectOfTheDireWhirlwind(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public virtual double GetCritChanceBonus(SimulationState state)
    {
        if (IsAspectEquipped(state) && state.Player.Auras.Contains(Aura.Whirlwinding))
        {
            var expiredEvent = state.ProcessedEvents.OrderBy(e => e.Timestamp).LastOrDefault(e => e is AuraExpiredEvent auraExpiredEvent && auraExpiredEvent.Aura == Aura.Whirlwinding);
            var expiredTimestamp = expiredEvent == null ? 0.0 : expiredEvent.Timestamp;

            var appliedEvent = state.ProcessedEvents.OrderBy(e => e.Timestamp)
                                                    .First(e => e is AuraAppliedEvent auraAppliedEvent &&
                                                                auraAppliedEvent.Aura == Aura.Whirlwinding &&
                                                                auraAppliedEvent.Timestamp >= expiredTimestamp);

            var startTime = appliedEvent.Timestamp;
            var critChanceBonus = Math.Floor(state.CurrentTime - startTime) * CritChance;
            var result = Math.Min(critChanceBonus, MaxCritChance);

            if (result > 0)
            {
                _log.Verbose($"Aspect of the Dire Whirlwind added Crit Chance bonus of {result:F2}%");
            }

            return result;
        }

        return 0.0;
    }
}
