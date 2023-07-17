using BarbarianSim.Abilities;
using BarbarianSim.Enums;

namespace BarbarianSim.StatCalculators;

public class ThornsCalculator
{
    public ThornsCalculator(MaxLifeCalculator maxLifeCalculator) => _maxLifeCalculator = maxLifeCalculator;

    private readonly MaxLifeCalculator _maxLifeCalculator;

    public virtual double Calculate(SimulationState state)
    {
        var thorns = state.Config.Gear.GetStatTotal(g => g.Thorns);

        if (state.Player.Auras.Contains(Aura.ChallengingShout) && state.Config.Skills.ContainsKey(Skill.StrategicChallengingShout))
        {
            thorns += _maxLifeCalculator.Calculate(state) * ChallengingShout.THORNS_BONUS_FROM_STRATEGIC;
        }

        return thorns;
    }
}
