using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Skills;

public class FuriousWhirlwind : IHandlesEvent<WhirlwindSpinEvent>
{
    // While using a slashing weapon Whirlwind also inflicts 40% of it's Base damage as Bleeding damage over 5 seconds
    public const double BLEED_DAMAGE = 0.4;
    public const double BLEED_DURATION = 5.0;

    public FuriousWhirlwind(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public void ProcessEvent(WhirlwindSpinEvent e, SimulationState state)
    {
        if (state.Config.Skills.ContainsKey(Skill.FuriousWhirlwind) && state.Config.PlayerSettings.SkillWeapons[Skill.Whirlwind] == state.Config.Gear.TwoHandSlashing)
        {
            foreach (var enemy in state.Enemies)
            {
                var bleedDamage = e.BaseDamage * BLEED_DAMAGE;
                var bleedAppliedEvent = new BleedAppliedEvent(e.Timestamp, "Furious Whirlwind", bleedDamage, BLEED_DURATION, enemy);
                state.Events.Add(bleedAppliedEvent);
                _log.Verbose($"Furious Whirlwind created BleedAppliedEvent for {bleedDamage:F2} damage over {BLEED_DURATION:F2} seconds on Enemy #{enemy.Id}");
            }
        }
    }
}
