using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Abilities;

public class LungingStrike
{
    public const double LUCKY_HIT_CHANCE = 0.5;
    public const double FURY_GENERATED = 10;

    // Lunge forward and strike an enemy for 33% damage

    public LungingStrike(SimLogger log) => _log = log;

    private readonly SimLogger _log;
    public virtual bool CanUse(SimulationState state) =>
        state.Config.HasSkill(Skill.LungingStrike)
        && !state.Player.Auras.Contains(Aura.WeaponCooldown);

    public virtual void Use(SimulationState state, EnemyState target) => state.Events.Add(new LungingStrikeEvent(state.CurrentTime, target));

    public virtual double GetSkillMultiplier(SimulationState state)
    {
        var skillPoints = state?.Config.Skills[Skill.LungingStrike];

        var result = skillPoints switch
        {
            1 => 0.33,
            2 => 0.36,
            3 => 0.39,
            4 => 0.42,
            >= 5 => 0.45,
            _ => 0.0,
        };

        if (result > 0)
        {
            _log.Verbose($"Skill multiplier from Lunging Strike = {result}x with {skillPoints} skill points");
        }

        return result;
    }
}
