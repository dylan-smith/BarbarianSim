using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.EventFactories;
using BarbarianSim.Skills;

namespace BarbarianSim.Events;

public class RallyingCryEvent : EventInfo
{
    public RallyingCryEvent(BoomingVoice boomingVoice,
                            AuraAppliedEventFactory auraAppliedEventFactory,
                            FuryGeneratedEventFactory furyGeneratedEventFactory,
                            FortifyGeneratedEventFactory fortifyGeneratedEventFactory,
                            RaidLeaderProcEventFactory raidLeaderProcEventFactory,
                            double timestamp) : base(timestamp)
    {
        _boomingVoice = boomingVoice;
        _auraAppliedEventFactory = auraAppliedEventFactory;
        _furyGeneratedEventFactory = furyGeneratedEventFactory;
        _fortifyGeneratedEventFactory = fortifyGeneratedEventFactory;
        _raidLeaderProcEventFactory = raidLeaderProcEventFactory;
    }

    private readonly BoomingVoice _boomingVoice;
    private readonly AuraAppliedEventFactory _auraAppliedEventFactory;
    private readonly FuryGeneratedEventFactory _furyGeneratedEventFactory;
    private readonly FortifyGeneratedEventFactory _fortifyGeneratedEventFactory;
    private readonly RaidLeaderProcEventFactory _raidLeaderProcEventFactory;

    public AuraAppliedEvent RallyingCryAuraAppliedEvent { get; set; }
    public AuraAppliedEvent RallyingCryCooldownAuraAppliedEvent { get; set; }
    public AuraAppliedEvent UnstoppableAuraAppliedEvent { get; set; }
    public FuryGeneratedEvent FuryGeneratedEvent { get; set; }
    public FortifyGeneratedEvent FortifyGeneratedEvent { get; set; }
    public RaidLeaderProcEvent RaidLeaderProcEvent { get; set; }
    public double Duration { get; set; }

    public override void ProcessEvent(SimulationState state)
    {
        Duration = RallyingCry.DURATION * _boomingVoice.GetDurationIncrease(state);

        RallyingCryAuraAppliedEvent = _auraAppliedEventFactory.Create(Timestamp, Duration, Aura.RallyingCry);
        state.Events.Add(RallyingCryAuraAppliedEvent);

        RallyingCryCooldownAuraAppliedEvent = _auraAppliedEventFactory.Create(Timestamp, RallyingCry.COOLDOWN, Aura.RallyingCryCooldown);
        state.Events.Add(RallyingCryCooldownAuraAppliedEvent);

        if (state.Config.Skills.ContainsKey(Skill.EnhancedRallyingCry))
        {
            UnstoppableAuraAppliedEvent = _auraAppliedEventFactory.Create(Timestamp, Duration, Aura.Unstoppable);
            state.Events.Add(UnstoppableAuraAppliedEvent);
        }

        if (state.Config.Skills.ContainsKey(Skill.TacticalRallyingCry))
        {
            FuryGeneratedEvent = _furyGeneratedEventFactory.Create(Timestamp, RallyingCry.FURY_FROM_TACTICAL_RALLYING_CRY);
            state.Events.Add(FuryGeneratedEvent);
        }

        if (state.Config.Skills.ContainsKey(Skill.StrategicRallyingCry))
        {
            FortifyGeneratedEvent = _fortifyGeneratedEventFactory.Create(Timestamp, StrategicRallyingCry.FORTIFY * state.Player.BaseLife);
            state.Events.Add(FortifyGeneratedEvent);
        }

        if (state.Config.Skills.ContainsKey(Skill.RaidLeader))
        {
            RaidLeaderProcEvent = _raidLeaderProcEventFactory.Create(Timestamp, Duration);
            state.Events.Add(RaidLeaderProcEvent);
        }
    }
}
