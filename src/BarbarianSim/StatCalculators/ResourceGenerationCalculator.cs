using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.Skills;

namespace BarbarianSim.StatCalculators;

public class ResourceGenerationCalculator
{
    public ResourceGenerationCalculator(WillpowerCalculator willpowerCalculator,
                                        ProlificFury prolificFury)
    {
        _willpowerCalculator = willpowerCalculator;
        _prolificFury = prolificFury;
    }

    private readonly WillpowerCalculator _willpowerCalculator;
    private readonly ProlificFury _prolificFury;

    public double Calculate(SimulationState state)
    {
        var resourceGeneration = state.Config.Gear.GetStatTotal(g => g.ResourceGeneration);
        resourceGeneration += _willpowerCalculator.Calculate(state) * 0.03;

        var result = 1.0 + (resourceGeneration / 100.0);

        if (state.Player.Auras.Contains(Aura.RallyingCry))
        {
            result *= GetRallyingCryResourceGeneration(state);

            if (state.Config.Skills.ContainsKey(Skill.TacticalRallyingCry))
            {
                result *= RallyingCry.RESOURCE_GENERATION_FROM_TACTICAL_RALLYING_CRY;
            }
        }

        result *= _prolificFury.GetFuryGeneration(state);

        if (state.Config.Skills.ContainsKey(Skill.PrimeWrathOfTheBerserker) && state.Player.Auras.Contains(Aura.WrathOfTheBerserker))
        {
            result *= WrathOfTheBerserker.RESOURCE_GENERATION_FROM_PRIME;
        }

        return result;
    }

    private double GetRallyingCryResourceGeneration(SimulationState state)
    {
        var skillPoints = state.Config.Gear.AllGear.Sum(g => g.RallyingCry);

        if (state.Config.Skills.TryGetValue(Skill.RallyingCry, out var pointsSpent))
        {
            skillPoints += pointsSpent;
        }

        return skillPoints switch
        {
            1 => 1.40,
            2 => 1.44,
            3 => 1.48,
            4 => 1.52,
            >= 5 => 1.56,
            _ => 1.0,
        };
    }
}
