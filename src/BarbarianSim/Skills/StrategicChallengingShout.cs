using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.Skills;

public class StrategicChallengingShout
{
    // While Challenging Shout is active, gain Thorns equal to 30% of your Maximum Life
    public const double THORNS_BONUS = 0.3;

    public StrategicChallengingShout(MaxLifeCalculator maxLifeCalculator, SimLogger log)
    {
        _maxLifeCalculator = maxLifeCalculator;
        _log = log;
    }

    private readonly MaxLifeCalculator _maxLifeCalculator;
    private readonly SimLogger _log;

    public virtual double GetThorns(SimulationState state)
    {
        if (state.Config.HasSkill(Skill.StrategicChallengingShout) && state.Player.Auras.Contains(Aura.ChallengingShout))
        {
            var thorns = THORNS_BONUS * _maxLifeCalculator.Calculate(state);
            _log.Verbose($"Thorns Bonus from Strategic Challenging Shout = {thorns:F2}");
            return thorns;
        }

        return 0;
    }
}
