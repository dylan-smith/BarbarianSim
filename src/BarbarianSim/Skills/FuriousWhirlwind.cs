using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Skills;

public class FuriousWhirlwind
{
    // While using a slashing weapon Whirlwind also inflicts 40% of it's Base damage as Bleeding damage over 5 seconds
    public const double BLEED_DAMAGE = 0.4;
    public const double BLEED_DURATION = 5.0;

    public void ProcessEvent(WhirlwindSpinEvent whirlwindSpinEvent, SimulationState state)
    {
        if (state.Config.Skills.ContainsKey(Skill.FuriousWhirlwind) && state.Config.PlayerSettings.SkillWeapons[Skill.Whirlwind] == state.Config.Gear.TwoHandSlashing)
        {
            foreach (var enemy in state.Enemies)
            {
                var bleedAppliedEvent = new BleedAppliedEvent(whirlwindSpinEvent.Timestamp, whirlwindSpinEvent.BaseDamage * BLEED_DAMAGE, BLEED_DURATION, enemy);
                state.Events.Add(bleedAppliedEvent);
            }
        }
    }
}
