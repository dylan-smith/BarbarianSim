using BarbarianSim.Events;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.EventHandlers;

public class HealingEventHandler : EventHandler<HealingEvent>
{
    public HealingEventHandler(HealingReceivedCalculator healingReceivedCalculator, MaxLifeCalculator maxLifeCalculator)
    {
        _healingReceivedCalculator = healingReceivedCalculator;
        _maxLifeCalculator = maxLifeCalculator;
    }

    private readonly HealingReceivedCalculator _healingReceivedCalculator;
    private readonly MaxLifeCalculator _maxLifeCalculator;

    public override void ProcessEvent(HealingEvent e, SimulationState state)
    {
        e.AmountHealed = e.BaseAmountHealed * _healingReceivedCalculator.Calculate(state);

        if (e.AmountHealed + state.Player.Life > _maxLifeCalculator.Calculate(state))
        {
            e.OverHeal = state.Player.Life + e.AmountHealed - _maxLifeCalculator.Calculate(state);
            e.AmountHealed -= e.OverHeal;
        }

        state.Player.Life += e.AmountHealed;
    }
}
