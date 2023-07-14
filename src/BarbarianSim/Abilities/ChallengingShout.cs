using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Abilities;

public class ChallengingShout
{
    public const double DURATION = 6.0;
    public const double COOLDOWN = 25.0;
    public const double MAX_LIFE_BONUS_FROM_ENHANCED = 1.2;
    public const double FURY_BONUS_FROM_TACTICAL = 3;
    public const double THORNS_BONUS_FROM_STRATEGIC = 0.3;

    // Taunt nearby enemies and gain 40% Damage Reduction for 6 seconds (Cooldown: 25 seconds)
    public bool CanUse(SimulationState state) => !state.Player.Auras.Contains(Aura.ChallengingShoutCooldown);

    public void Use(SimulationState state) => state.Events.Add(new ChallengingShoutEvent(state.CurrentTime));

    public double GetDamageReduction(SimulationState state)
    {
        var skillPoints = state.Config.Gear.AllGear.Sum(g => g.ChallengingShout);

        if (state.Config.Skills.TryGetValue(Skill.ChallengingShout, out var pointsSpent))
        {
            skillPoints += pointsSpent;
        }

        return skillPoints switch
        {
            1 => 40,
            2 => 42,
            3 => 44,
            4 => 46,
            >= 5 => 48,
            _ => 0,
        };
    }
}
