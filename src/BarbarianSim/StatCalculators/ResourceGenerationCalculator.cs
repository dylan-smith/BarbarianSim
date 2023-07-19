using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.Skills;

namespace BarbarianSim.StatCalculators;

public class ResourceGenerationCalculator
{
    public ResourceGenerationCalculator(WillpowerCalculator willpowerCalculator, RallyingCry rallyingCry, ProlificFury prolificFury, TacticalRallyingCry tacticalRallyingCry)
    {
        _willpowerCalculator = willpowerCalculator;
        _rallyingCry = rallyingCry;
        _prolificFury = prolificFury;
        _tacticalRallyingCry = tacticalRallyingCry;
    }

    private readonly WillpowerCalculator _willpowerCalculator;
    private readonly RallyingCry _rallyingCry;
    private readonly ProlificFury _prolificFury;
    private readonly TacticalRallyingCry _tacticalRallyingCry;

    public virtual double Calculate(SimulationState state)
    {
        var resourceGeneration = state.Config.Gear.GetStatTotal(g => g.ResourceGeneration);
        resourceGeneration += _willpowerCalculator.Calculate(state) * 0.03;

        var result = 1.0 + (resourceGeneration / 100.0);

        result *= _rallyingCry.GetResourceGeneration(state);
        result *= _tacticalRallyingCry.GetResourceGeneration(state);

        result *= _prolificFury.GetFuryGeneration(state);

        if (state.Config.Skills.ContainsKey(Skill.PrimeWrathOfTheBerserker) && state.Player.Auras.Contains(Aura.WrathOfTheBerserker))
        {
            result *= WrathOfTheBerserker.RESOURCE_GENERATION_FROM_PRIME;
        }

        return result;
    }
}
