using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.EventFactories;
using BarbarianSim.Skills;

namespace BarbarianSim.Events;

public class ChallengingShoutEvent : EventInfo
{
    public ChallengingShoutEvent(AuraAppliedEventFactory auraAppliedEventFactory,
                                 RaidLeaderProcEventFactory raidLeaderProcEventFactory,
                                 BoomingVoice boomingVoice,
                                 double timestamp) : base(timestamp)
    {
        _auraAppliedEventFactory = auraAppliedEventFactory;
        _raidLeaderProcEventFactory = raidLeaderProcEventFactory;
        _boomingVoice = boomingVoice;
    }

    private readonly AuraAppliedEventFactory _auraAppliedEventFactory;
    private readonly RaidLeaderProcEventFactory _raidLeaderProcEventFactory;
    private readonly BoomingVoice _boomingVoice;

    public AuraAppliedEvent ChallengingShoutCooldownAuraAppliedEvent { get; set; }
    public RaidLeaderProcEvent RaidLeaderProcEvent { get; set; }
    public double Duration { get; set; }
    public AuraAppliedEvent ChallengingShoutAuraAppliedEvent { get; set; }
    public IList<AuraAppliedEvent> TauntAuraAppliedEvent { get; init; } = new List<AuraAppliedEvent>();

    public override void ProcessEvent(SimulationState state)
    {
        Duration = ChallengingShout.DURATION * _boomingVoice.GetDurationIncrease(state);

        ChallengingShoutAuraAppliedEvent = _auraAppliedEventFactory.Create(Timestamp, Duration, Aura.ChallengingShout);
        state.Events.Add(ChallengingShoutAuraAppliedEvent);

        ChallengingShoutCooldownAuraAppliedEvent = _auraAppliedEventFactory.Create(Timestamp, ChallengingShout.COOLDOWN, Aura.ChallengingShoutCooldown);
        state.Events.Add(ChallengingShoutCooldownAuraAppliedEvent);

        foreach (var enemy in state.Enemies)
        {
            var tauntAppliedEvent = _auraAppliedEventFactory.Create(Timestamp, Duration, Aura.Taunt, enemy);
            TauntAuraAppliedEvent.Add(tauntAppliedEvent);
            state.Events.Add(tauntAppliedEvent);
        }

        if (state.Config.Skills.ContainsKey(Skill.RaidLeader))
        {
            RaidLeaderProcEvent = _raidLeaderProcEventFactory.Create(Timestamp, Duration);
            state.Events.Add(RaidLeaderProcEvent);
        }
    }
}
