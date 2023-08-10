using BarbarianSim.Events;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.EventHandlers;

public class HealingEventHandler : EventHandler<HealingEvent>
{
    public HealingEventHandler(HealingReceivedCalculator healingReceivedCalculator, MaxLifeCalculator maxLifeCalculator, SimLogger log)
    {
        _healingReceivedCalculator = healingReceivedCalculator;
        _maxLifeCalculator = maxLifeCalculator;
        _log = log;
    }

    private readonly HealingReceivedCalculator _healingReceivedCalculator;
    private readonly MaxLifeCalculator _maxLifeCalculator;
    private readonly SimLogger _log;

    public override void ProcessEvent(HealingEvent e, SimulationState state)
    {
        _log.Verbose($"Base Amount Healed = {e.BaseAmountHealed:F2}");
        e.AmountHealed = e.BaseAmountHealed * _healingReceivedCalculator.Calculate(state);
        _log.Verbose($"Total Healing = {e.AmountHealed:F2}");

        var maxLife = _maxLifeCalculator.Calculate(state);
        if (e.AmountHealed + state.Player.Life > maxLife)
        {
            e.OverHeal = state.Player.Life + e.AmountHealed - maxLife;
            _log.Verbose($"Overheal = {e.OverHeal:F2}");
            e.AmountHealed -= e.OverHeal;
            _log.Verbose($"Actual Amount Healed = {e.AmountHealed:F2}");
        }

        state.Player.Life += e.AmountHealed;
        _log.Verbose($"New Player Life = {state.Player.Life:F2}");
    }
}
