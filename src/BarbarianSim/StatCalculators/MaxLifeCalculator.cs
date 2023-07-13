using BarbarianSim.Abilities;
using BarbarianSim.Enums;

namespace BarbarianSim.StatCalculators;

public class MaxLifeCalculator
{
    public double Calculate(SimulationState state)
    {
        var maxLife = state.Player.BaseLife;
        maxLife += state.Config.Gear.GetStatTotal(g => g.MaxLife);

        if (state.Player.Auras.Contains(Aura.ChallengingShout) && state.Config.Skills.ContainsKey(Skill.EnhancedChallengingShout))
        {
            maxLife *= ChallengingShout.MAX_LIFE_BONUS_FROM_ENHANCED;
        }

        return maxLife;
    }
}
