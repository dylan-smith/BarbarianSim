namespace BarbarianSim.StatCalculators;

public class DamageReductionWhileInjuredCalculator : BaseStatCalculator
{
    public static double Calculate(SimulationState state) => Calculate<DamageReductionWhileInjuredCalculator>(state);

    protected override double InstanceCalculate(SimulationState state)
    {
        var damageReduction = state.Config.Gear.GetStatTotalMultiplied(g => 1 - (g.DamageReductionWhileInjured / 100.0));

        return state.Player.IsInjured(MaxLifeCalculator.Calculate(state)) ? damageReduction : 1;
    }
}
