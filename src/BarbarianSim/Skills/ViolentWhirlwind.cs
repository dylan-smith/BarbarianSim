using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Skills;

public class ViolentWhirlwind : IHandlesEvent<WhirlwindSpinEvent>
{
    // After using Whirlwind for 2 seconds, Whirlwind deals 30%[x] increased damage until cancelled
    public const double DELAY = 2.0;
    public const double DAMAGE_MULTIPLIER = 1.3;

    public ViolentWhirlwind(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public void ProcessEvent(WhirlwindSpinEvent e, SimulationState state)
    {
        if (state.Config.HasSkill(Skill.ViolentWhirlwind))
        {
            state.Events.Add(new AuraAppliedEvent(e.Timestamp + DELAY, "Violent Whirlwind", 0, Aura.ViolentWhirlwind));
            _log.Verbose($"Violent Whirlwind created AuraAppliedEvent for Violent Whirlwind at Timestamp {e.Timestamp + DELAY:F2}");
        }
    }

    public virtual double GetDamageBonus(SimulationState state, DamageSource damageSource)
    {
        if (damageSource == DamageSource.Whirlwind && state.Player.Auras.Contains(Aura.ViolentWhirlwind))
        {
            _log.Verbose($"Damage Bonus from Violent Whirlwind = {DAMAGE_MULTIPLIER}x");
            return DAMAGE_MULTIPLIER;
        }

        return 1.0;
    }
}
