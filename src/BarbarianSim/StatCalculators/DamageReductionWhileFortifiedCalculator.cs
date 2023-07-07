namespace BarbarianSim.StatCalculators;

public class DamageReductionWhileFortifiedCalculator : BaseStatCalculator
{
    public static double Calculate(SimulationState state) => Calculate<DamageReductionWhileFortifiedCalculator>(state);

    private const double BASE_FORTIFY_DAMAGE_REDUCTION = 10;

    protected override double InstanceCalculate(SimulationState state)
    {
        var damageReduction = 1 - (BASE_FORTIFY_DAMAGE_REDUCTION / 100);
        damageReduction *= state.Config.Gear.GetStatTotalMultiplied(g => 1 - (g.DamageReductionWhileFortified / 100));

        return state.Player.IsFortified() ? damageReduction : 1;
    }
}
