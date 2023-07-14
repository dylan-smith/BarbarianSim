using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.Skills;

namespace BarbarianSim.StatCalculators;

public class ResourceGenerationCalculator
{
    public ResourceGenerationCalculator(WillpowerCalculator willpowerCalculator) => _willpowerCalculator = willpowerCalculator;

    private readonly WillpowerCalculator _willpowerCalculator;

    public double Calculate(SimulationState state)
    {
        var resourceGeneration = state.Config.Gear.GetStatTotal(g => g.ResourceGeneration);
        resourceGeneration += _willpowerCalculator.Calculate(state) * 0.03;

        var result = 1.0 + (resourceGeneration / 100.0);

        if (state.Player.Auras.Contains(Aura.RallyingCry))
        {
            result *= RallyingCry.GetResourceGeneration(state);

            if (state.Config.Skills.ContainsKey(Skill.TacticalRallyingCry))
            {
                result *= RallyingCry.RESOURCE_GENERATION_FROM_TACTICAL_RALLYING_CRY;
            }
        }

        result *= ProlificFury.GetFuryGeneration(state);

        if (state.Config.Skills.ContainsKey(Skill.PrimeWrathOfTheBerserker) && state.Player.Auras.Contains(Aura.WrathOfTheBerserker))
        {
            result *= WrathOfTheBerserker.RESOURCE_GENERATION_FROM_PRIME;
        }

        return result;
    }
}
