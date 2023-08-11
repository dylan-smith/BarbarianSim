namespace BarbarianSim.StatCalculators;

public class DamageReductionWhileInjuredCalculator
{
    public DamageReductionWhileInjuredCalculator(MaxLifeCalculator maxLifeCalculator, SimLogger log)
    {
        _maxLifeCalculator = maxLifeCalculator;
        _log = log;
    }

    private readonly MaxLifeCalculator _maxLifeCalculator;
    private readonly SimLogger _log;

    public virtual double Calculate(SimulationState state)
    {
        if (state.Player.IsInjured(_maxLifeCalculator.Calculate(state)))
        {
            var damageReduction = state.Config.GetStatTotalMultiplied(g => 1 - (g.DamageReductionWhileInjured / 100.0));
            _log.Verbose($"Total Damage Reduction while Injured = {damageReduction:F2}x");

            return damageReduction;
        }

        return 1;
    }
}
