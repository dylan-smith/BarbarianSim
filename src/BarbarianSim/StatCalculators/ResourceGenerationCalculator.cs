using BarbarianSim.Abilities;
using BarbarianSim.Skills;

namespace BarbarianSim.StatCalculators;

public class ResourceGenerationCalculator
{
    public ResourceGenerationCalculator(WillpowerCalculator willpowerCalculator,
                                        RallyingCry rallyingCry,
                                        ProlificFury prolificFury,
                                        TacticalRallyingCry tacticalRallyingCry,
                                        PrimeWrathOfTheBerserker primeWrathOfTheBerserker)
    {
        _willpowerCalculator = willpowerCalculator;
        _rallyingCry = rallyingCry;
        _prolificFury = prolificFury;
        _tacticalRallyingCry = tacticalRallyingCry;
        _primeWrathOfTheBerserker = primeWrathOfTheBerserker;
    }

    private readonly WillpowerCalculator _willpowerCalculator;
    private readonly RallyingCry _rallyingCry;
    private readonly ProlificFury _prolificFury;
    private readonly TacticalRallyingCry _tacticalRallyingCry;
    private readonly PrimeWrathOfTheBerserker _primeWrathOfTheBerserker;

    public virtual double Calculate(SimulationState state)
    {
        var resourceGeneration = state.Config.GetStatTotal(g => g.ResourceGeneration);
        resourceGeneration += _willpowerCalculator.Calculate(state) * 0.03;

        var result = 1.0 + (resourceGeneration / 100.0);

        result *= _rallyingCry.GetResourceGeneration(state);
        result *= _tacticalRallyingCry.GetResourceGeneration(state);
        result *= _prolificFury.GetFuryGeneration(state);
        result *= _primeWrathOfTheBerserker.GetResourceGeneration(state);

        return result;
    }
}
