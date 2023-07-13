using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.EventFactories;
using BarbarianSim.Skills;

namespace BarbarianSim.Events;

public class WarCryEvent : EventInfo
{
    public WarCryEvent(BoomingVoice boomingVoice, 
                       AuraAppliedEventFactory auraAppliedEventFactory, 
                       FortifyGeneratedEventFactory fortifyGeneratedEventFactory, 
                       RaidLeaderProcEventFactory raidLeaderProcEventFactory, 
                       double timestamp) : base(timestamp)
    {
        _boomingVoice = boomingVoice;
        _auraAppliedEventFactory = auraAppliedEventFactory;
        _fortifyGeneratedEventFactory = fortifyGeneratedEventFactory;
        _raidLeaderProcEventFactory = raidLeaderProcEventFactory;
    }

    private readonly BoomingVoice _boomingVoice;
    private readonly AuraAppliedEventFactory _auraAppliedEventFactory;
    private readonly FortifyGeneratedEventFactory _fortifyGeneratedEventFactory;
    private readonly RaidLeaderProcEventFactory _raidLeaderProcEventFactory;

    public AuraAppliedEvent WarCryAuraAppliedEvent { get; set; }
    public AuraAppliedEvent WarCryCooldownAuraAppliedEvent { get; set; }
    public AuraAppliedEvent BerserkingAuraAppliedEvent { get; set; }
    public FortifyGeneratedEvent FortifyGeneratedEvent { get; set; }
    public RaidLeaderProcEvent RaidLeaderProcEvent { get; set; }
    public double Duration { get; set; }

    public override void ProcessEvent(SimulationState state)
    {
        Duration = WarCry.DURATION * _boomingVoice.GetDurationIncrease(state);

        WarCryAuraAppliedEvent = _auraAppliedEventFactory.Create(Timestamp, Duration, Aura.WarCry);
        state.Events.Add(WarCryAuraAppliedEvent);

        WarCryCooldownAuraAppliedEvent = _auraAppliedEventFactory.Create(Timestamp, WarCry.COOLDOWN, Aura.WarCryCooldown);
        state.Events.Add(WarCryCooldownAuraAppliedEvent);

        if (state.Config.Skills.ContainsKey(Skill.EnhancedWarCry))
        {
            BerserkingAuraAppliedEvent = _auraAppliedEventFactory.Create(Timestamp, WarCry.BERSERKING_DURATION_FROM_ENHANCED, Aura.Berserking);
            state.Events.Add(BerserkingAuraAppliedEvent);
        }

        if (state.Config.Skills.ContainsKey(Skill.MightyWarCry))
        {
            FortifyGeneratedEvent = _fortifyGeneratedEventFactory.Create(Timestamp, WarCry.FORTIFY_FROM_MIGHTY * state.Player.BaseLife);
            state.Events.Add(FortifyGeneratedEvent);
        }

        if (state.Config.Skills.ContainsKey(Skill.RaidLeader))
        {
            RaidLeaderProcEvent = _raidLeaderProcEventFactory.Create(Timestamp, Duration);
            state.Events.Add(RaidLeaderProcEvent);
        }
    }
}
