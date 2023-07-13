using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.EventFactories;

namespace BarbarianSim.Events;

public class WrathOfTheBerserkerEvent : EventInfo
{
    public WrathOfTheBerserkerEvent(AuraAppliedEventFactory auraAppliedEventFactory, double timestamp) : base(timestamp) => _auraAppliedEventFactory = auraAppliedEventFactory;

    private readonly AuraAppliedEventFactory _auraAppliedEventFactory;

    public AuraAppliedEvent WrathOfTheBerserkerAuraAppliedEvent { get; set; }
    public AuraAppliedEvent UnstoppableAuraAppliedEvent { get; set; }
    public AuraAppliedEvent WrathOfTheBerserkerCooldownAuraAppliedEvent { get; set; }
    public AuraAppliedEvent BerserkingAuraAppliedEvent { get; set; }

    public override void ProcessEvent(SimulationState state)
    {
        WrathOfTheBerserkerAuraAppliedEvent = _auraAppliedEventFactory.Create(Timestamp, WrathOfTheBerserker.DURATION, Aura.WrathOfTheBerserker);
        state.Events.Add(WrathOfTheBerserkerAuraAppliedEvent);

        UnstoppableAuraAppliedEvent = _auraAppliedEventFactory.Create(Timestamp, WrathOfTheBerserker.UNSTOPPABLE_DURATION, Aura.Unstoppable);
        state.Events.Add(UnstoppableAuraAppliedEvent);

        BerserkingAuraAppliedEvent = _auraAppliedEventFactory.Create(Timestamp, WrathOfTheBerserker.BERSERKING_DURATION, Aura.Berserking);
        state.Events.Add(BerserkingAuraAppliedEvent);

        WrathOfTheBerserkerCooldownAuraAppliedEvent = _auraAppliedEventFactory.Create(Timestamp, WrathOfTheBerserker.COOLDOWN, Aura.WrathOfTheBerserkerCooldown);
        state.Events.Add(WrathOfTheBerserkerCooldownAuraAppliedEvent);
    }
}
