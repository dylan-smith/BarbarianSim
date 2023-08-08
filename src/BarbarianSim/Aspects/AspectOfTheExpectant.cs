using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Aspects;

public class AspectOfTheExpectant : Aspect
{
    // Attacking enemies with a Basic Skill increases the damage of your next Core Skill cast by [5 - 10]%[x], up to 30%[x].
    public const double MAX_DAMAGE = 30.0;
    public double Damage { get; set; }

    public AspectOfTheExpectant(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public virtual double GetDamageBonus(SimulationState state, SkillType skillType)
    {
        if (IsAspectEquipped(state) && skillType == SkillType.Core)
        {
            var lastCoreSkill = state.ProcessedEvents.OfType<DirectDamageEvent>().OrderBy(e => e.Timestamp).LastOrDefault(e => e.SkillType == skillType);
            var lastCoreSkillTimestamp = lastCoreSkill == null ? 0.0 : lastCoreSkill.Timestamp;

            var basicCount = state.ProcessedEvents.OfType<DirectDamageEvent>().OrderBy(e => e.Timestamp).Count(e => e.SkillType == SkillType.Basic && e.Timestamp > lastCoreSkillTimestamp);

            var result = 1 + (Math.Min(MAX_DAMAGE, basicCount * Damage) / 100.0);

            if (result > 1.0)
            {
                _log.Verbose($"Damage bonus from Aspect of the Expectant = {result:F2}x");
            }

            return result;
        }

        return 1.0;
    }
}
