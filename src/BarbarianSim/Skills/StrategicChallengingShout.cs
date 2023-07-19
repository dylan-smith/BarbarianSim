using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.Skills;

public class StrategicChallengingShout
{
    // While Challenging Shout is active, gain Thorns equal to 30% of your Maximum Life
    public const double THORNS_BONUS = 0.3;

    public StrategicChallengingShout(MaxLifeCalculator maxLifeCalculator)
    {
        _maxLifeCalculator = maxLifeCalculator;
    }

    private readonly MaxLifeCalculator _maxLifeCalculator;

    public virtual double GetThorns(SimulationState state)
    {
        return state.Config.Skills.TryGetValue(Skill.StrategicChallengingShout, out var skillPoints) &&
               skillPoints > 0 &&
               state.Player.Auras.Contains(Aura.ChallengingShout)
               ? THORNS_BONUS * _maxLifeCalculator.Calculate(state)
               : 0;
    }
}
