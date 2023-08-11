using BarbarianSim.Skills;

namespace BarbarianSim.StatCalculators;

public class MaxLifeCalculator
{
    public MaxLifeCalculator(EnhancedChallengingShout enhancedChallengingShout, SimLogger log)
    {
        _enhancedChallengingShout = enhancedChallengingShout;
        _log = log;
    }

    private readonly EnhancedChallengingShout _enhancedChallengingShout;
    private readonly SimLogger _log;

    public virtual double Calculate(SimulationState state)
    {
        var baseLife = state.Player.BaseLife;
        _log.Verbose($"Base Life = {baseLife:F2}");

        var lifeFromConfig = state.Config.GetStatTotal(g => g.MaxLife);
        if (lifeFromConfig > 0)
        {
            _log.Verbose($"Max Life from Config = {lifeFromConfig:F2}");
        }

        var maxLifePercent = 1 + (state.Config.GetStatTotal(g => g.MaxLifePercent) / 100.0);
        if (maxLifePercent > 1)
        {
            _log.Verbose($"Max Life Percent from Config = {maxLifePercent:F2}x");
        }

        var lifeFromChallengingShout = _enhancedChallengingShout.GetMaxLifeMultiplier(state);

        var result = (baseLife + lifeFromConfig) * maxLifePercent * lifeFromChallengingShout;
        _log.Verbose($"Total Max Life = {result:F2}");

        return result;
    }
}
