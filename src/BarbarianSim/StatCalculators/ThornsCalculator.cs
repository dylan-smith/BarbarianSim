using BarbarianSim.Abilities;
using BarbarianSim.Enums;

namespace BarbarianSim.StatCalculators;

public class ThornsCalculator : BaseStatCalculator
{
    public static double Calculate(SimulationState state) => Calculate<ThornsCalculator>(state);

    protected override double InstanceCalculate(SimulationState state)
    {
        var thorns = state.Config.Gear.GetStatTotal(g => g.Thorns);

        if (state.Player.Auras.Contains(Aura.ChallengingShout) && state.Config.Skills.ContainsKey(Skill.StrategicChallengingShout))
        {
            thorns += MaxLifeCalculator.Calculate(state) * ChallengingShout.THORNS_BONUS_FROM_STRATEGIC;
        }

        return thorns;
    }
}
