using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Aspects;

public class BoldChieftainsAspect : Aspect, IHandlesEvent<ChallengingShoutEvent>, IHandlesEvent<RallyingCryEvent>, IHandlesEvent<WarCryEvent>
{
    // Whenever you cast a Shout Skill, it's Cooldown is reduced by 1.0-1.9 seconds per Nearby enemy, up to a maximum of 6 seconds
    public const double MAX_COOLDOWN_REDUCTION = 6.0;
    public double CooldownReduction { get; set; }

    public void ProcessEvent(ChallengingShoutEvent e, SimulationState state)
    {
        if (IsAspectEquipped(state))
        {
            var cooldownReduction = state.Enemies.Count * CooldownReduction;
            cooldownReduction = Math.Min(MAX_COOLDOWN_REDUCTION, cooldownReduction);

            state.Events
                 .OfType<AuraAppliedEvent>()
                 .Single(x => x is AuraAppliedEvent auraAppliedEvent && auraAppliedEvent.Aura == Aura.ChallengingShoutCooldown)
                 .Duration -= cooldownReduction;
        }
    }

    public void ProcessEvent(RallyingCryEvent e, SimulationState state)
    {
        if (IsAspectEquipped(state))
        {
            var cooldownReduction = state.Enemies.Count * CooldownReduction;
            cooldownReduction = Math.Min(MAX_COOLDOWN_REDUCTION, cooldownReduction);

            state.Events
                 .OfType<AuraAppliedEvent>()
                 .Single(x => x is AuraAppliedEvent auraAppliedEvent && auraAppliedEvent.Aura == Aura.RallyingCryCooldown)
                 .Duration -= cooldownReduction;
        }
    }
    public void ProcessEvent(WarCryEvent e, SimulationState state)
    {
        if (IsAspectEquipped(state))
        {
            var cooldownReduction = state.Enemies.Count * CooldownReduction;
            cooldownReduction = Math.Min(MAX_COOLDOWN_REDUCTION, cooldownReduction);

            state.Events
                 .OfType<AuraAppliedEvent>()
                 .Single(x => x is AuraAppliedEvent auraAppliedEvent && auraAppliedEvent.Aura == Aura.WarCryCooldown)
                 .Duration -= cooldownReduction;
        }
    }
}
