using BarbarianSim.Enums;

namespace BarbarianSim.Skills;

public class EnhancedChallengingShout
{
    // While Challenging Shout is active, gain 20%[x] bonus Max Life
    public const double MAX_LIFE_BONUS = 1.2;

    public EnhancedChallengingShout(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public virtual double GetMaxLifeMultiplier(SimulationState state)
    {
        if (state.Player.Auras.Contains(Aura.ChallengingShout) &&
            state.Config.HasSkill(Skill.EnhancedChallengingShout))
        {
            _log.Verbose($"Max Life Bonus from Enhanced Challenging Shout = {MAX_LIFE_BONUS:F2}x");
            return MAX_LIFE_BONUS;
        }

        return 1.0;
    }
}
