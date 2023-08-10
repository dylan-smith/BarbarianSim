using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.Skills;

public class InvigoratingFury : IHandlesEvent<FurySpentEvent>
{
    // Heal for 3% of your Maximum Life for each 100 Fury spent
    public InvigoratingFury(MaxLifeCalculator maxLifeCalculator, SimLogger log)
    {
        _maxLifeCalculator = maxLifeCalculator;
        _log = log;
    }

    private readonly MaxLifeCalculator _maxLifeCalculator;
    private readonly SimLogger _log;

    public void ProcessEvent(FurySpentEvent e, SimulationState state)
    {
        if (state.Config.Skills.ContainsKey(Skill.InvigoratingFury))
        {
            var totalFurySpent = state.ProcessedEvents.OfType<FurySpentEvent>().Sum(x => x.FurySpent);

            // If we spend more than 100 fury at once this will only fire once (which is probably wrong)
            // but I don't think there's any way to spend more than 100 fury at once
            if (Math.Floor(totalFurySpent / 100) != Math.Floor((totalFurySpent - e.FurySpent) / 100))
            {
                var healingAmount = _maxLifeCalculator.Calculate(state) * GetHealingPercentage(state);
                state.Events.Add(new HealingEvent(e.Timestamp, "Invigorating Fury", healingAmount));
                _log.Verbose($"Invigorating Fury created HealingEvent for {healingAmount:F2}");
            }
        }
    }

    public virtual double GetHealingPercentage(SimulationState state)
    {
        var skillPoints = state.Config.GetSkillPoints(Skill.InvigoratingFury);

        var result = skillPoints switch
        {
            1 => 0.03,
            2 => 0.06,
            >= 3 => 0.09,
            _ => 0,
        };

        if (result > 0)
        {
            _log.Verbose($"Healing Percentage from Invigorating Fury = {result:F2}x with {skillPoints} skill points");
        }

        return result;
    }
}
