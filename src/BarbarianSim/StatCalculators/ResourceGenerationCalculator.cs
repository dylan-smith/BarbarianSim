using BarbarianSim.Abilities;
using BarbarianSim.Skills;

namespace BarbarianSim.StatCalculators;

public class ResourceGenerationCalculator
{
    public ResourceGenerationCalculator(WillpowerCalculator willpowerCalculator,
                                        RallyingCry rallyingCry,
                                        ProlificFury prolificFury,
                                        TacticalRallyingCry tacticalRallyingCry,
                                        PrimeWrathOfTheBerserker primeWrathOfTheBerserker,
                                        SimLogger log)
    {
        _willpowerCalculator = willpowerCalculator;
        _rallyingCry = rallyingCry;
        _prolificFury = prolificFury;
        _tacticalRallyingCry = tacticalRallyingCry;
        _primeWrathOfTheBerserker = primeWrathOfTheBerserker;
        _log = log;
    }

    private readonly WillpowerCalculator _willpowerCalculator;
    private readonly RallyingCry _rallyingCry;
    private readonly ProlificFury _prolificFury;
    private readonly TacticalRallyingCry _tacticalRallyingCry;
    private readonly PrimeWrathOfTheBerserker _primeWrathOfTheBerserker;
    private readonly SimLogger _log;

    public virtual double Calculate(SimulationState state)
    {
        var resourceGeneration = 0.0;

        var configBonus = state.Config.GetStatTotal(g => g.ResourceGeneration);
        if (configBonus > 0)
        {
            resourceGeneration += configBonus;
            _log.Verbose($"Resource Generation bonus from Config = {resourceGeneration:F2}%");
        }

        var willpowerBonus = _willpowerCalculator.Calculate(state) * 0.03;
        if (willpowerBonus > 0)
        {
            resourceGeneration += willpowerBonus;
            _log.Verbose($"Resource Generation bonus from Willpower = {willpowerBonus:F2}%");
        }

        var result = 1.0 + (resourceGeneration / 100.0);

        result *= _rallyingCry.GetResourceGeneration(state);
        result *= _tacticalRallyingCry.GetResourceGeneration(state);
        result *= _prolificFury.GetFuryGeneration(state);
        result *= _primeWrathOfTheBerserker.GetResourceGeneration(state);

        _log.Verbose($"Total Resource Generation multiplier = {result:F2}x");
        return result;
    }
}
