using BarbarianSim.StatCalculators;

namespace BarbarianSim.Events;

public class HealingEvent : EventInfo
{
    public HealingEvent(HealingReceivedCalculator healingReceivedCalculator, MaxLifeCalculator maxLifeCalculator, double timestamp, double baseAmountHealed) : base(timestamp)
    {
        _healingReceivedCalculator = healingReceivedCalculator;
        _maxLifeCalculator = maxLifeCalculator;
        BaseAmountHealed = baseAmountHealed;
    }

    private readonly HealingReceivedCalculator _healingReceivedCalculator;
    private readonly MaxLifeCalculator _maxLifeCalculator;

    public double BaseAmountHealed { get; set; }
    public double AmountHealed { get; set; }
    public double OverHeal { get; set; }

    public override void ProcessEvent(SimulationState state)
    {
        AmountHealed = BaseAmountHealed * _healingReceivedCalculator.Calculate(state);

        if (AmountHealed + state.Player.Life > _maxLifeCalculator.Calculate(state))
        {
            OverHeal = state.Player.Life + AmountHealed - _maxLifeCalculator.Calculate(state);
            AmountHealed -= OverHeal;
        }

        state.Player.Life += AmountHealed;
    }
}
