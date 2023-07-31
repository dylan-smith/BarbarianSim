using BarbarianSim.Skills;

namespace BarbarianSim.StatCalculators;

public class MaxLifeCalculator
{
    public MaxLifeCalculator(EnhancedChallengingShout enhancedChallengingShout) => _enhancedChallengingShout = enhancedChallengingShout;

    private readonly EnhancedChallengingShout _enhancedChallengingShout;

    public virtual double Calculate(SimulationState state)
    {
        var maxLife = state.Player.BaseLife;
        maxLife += state.Config.GetStatTotal(g => g.MaxLife);

        maxLife *= 1 + (state.Config.GetStatTotal(g => g.MaxLifePercent) / 100.0);
        maxLife *= _enhancedChallengingShout.GetMaxLifeMultiplier(state);

        return maxLife;
    }
}
