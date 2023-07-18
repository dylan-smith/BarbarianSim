using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.Skills;

public class InvigoratingFury : IHandlesEvent<FurySpentEvent>
{
    // Heal for 3% of your Maximum Life for each 100 Fury spent
    public InvigoratingFury(MaxLifeCalculator maxLifeCalculator) => _maxLifeCalculator = maxLifeCalculator;

    private readonly MaxLifeCalculator _maxLifeCalculator;

    public void ProcessEvent(FurySpentEvent furySpentEvent, SimulationState state)
    {
        if (state.Config.Skills.ContainsKey(Skill.InvigoratingFury))
        {
            var totalFurySpent = state.ProcessedEvents.OfType<FurySpentEvent>().Sum(x => x.FurySpent);

            // If we spend more than 100 fury at once this will only fire once (which is probably wrong)
            // but I don't think there's any way to spend more than 100 fury at once
            if (Math.Floor(totalFurySpent / 100) != Math.Floor((totalFurySpent - furySpentEvent.FurySpent) / 100))
            {
                state.Events.Add(new HealingEvent(furySpentEvent.Timestamp, _maxLifeCalculator.Calculate(state) * GetHealingPercentage(state)));
            }
        }
    }

    public virtual double GetHealingPercentage(SimulationState state)
    {
        var skillPoints = 0;

        if (state.Config.Skills.ContainsKey(Skill.InvigoratingFury))
        {
            skillPoints += state.Config.Skills[Skill.InvigoratingFury];
        }

        return skillPoints switch
        {
            1 => 0.03,
            2 => 0.06,
            >= 3 => 0.09,
            _ => 0,
        };
    }
}
