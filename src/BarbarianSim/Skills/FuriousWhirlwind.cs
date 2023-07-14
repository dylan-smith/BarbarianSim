using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.EventFactories;
using BarbarianSim.Events;

namespace BarbarianSim.Skills;

public class FuriousWhirlwind
{
    // While using a slashing weapon Whirlwind also inflicts 40% of it's Base damage as Bleeding damage over 5 seconds
    public const double DELAY = 2.0;
    public const double DAMAGE_MULTIPLIER = 1.3;

    public FuriousWhirlwind(Whirlwind whirlwind, BleedAppliedEventFactory bleedAppliedEventFactory)
    {
        _whirlwind = whirlwind;
        _bleedAppliedEventFactory = bleedAppliedEventFactory;
    }

    private readonly Whirlwind _whirlwind;
    private readonly BleedAppliedEventFactory _bleedAppliedEventFactory;

    public void ProcessEvent(WhirlwindSpinEvent whirlwindSpinEvent, SimulationState state)
    {
        if (state.Config.Skills.ContainsKey(Skill.FuriousWhirlwind) && state.Config.PlayerSettings.SkillWeapons[Skill.Whirlwind] == state.Config.Gear.TwoHandSlashing)
        {
            foreach (var enemy in state.Enemies)
            {
                var bleedAppliedEvent = _bleedAppliedEventFactory.Create(whirlwindSpinEvent.Timestamp, whirlwindSpinEvent.BaseDamage * 0.4, 5, enemy);
                state.Events.Add(bleedAppliedEvent);
            }
        }
    }
}
