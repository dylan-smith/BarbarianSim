using BarbarianSim.Enums;

namespace BarbarianSim.Skills;

public class EnhancedChallengingShout
{
    // While Challenging Shout is active, gain 20%[x] bonus Max Life
    public const double MAX_LIFE_BONUS = 1.2;

    public virtual double GetMaxLifeMultiplier(SimulationState state)
    {
        return state.Player.Auras.Contains(Aura.ChallengingShout) &&
            state.Config.Skills.ContainsKey(Skill.EnhancedChallengingShout) &&
            state.Config.Skills[Skill.EnhancedChallengingShout] > 0
            ? MAX_LIFE_BONUS
            : 1.0;
    }
}
