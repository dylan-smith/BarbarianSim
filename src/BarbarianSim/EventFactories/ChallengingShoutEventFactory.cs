using BarbarianSim.Events;
using BarbarianSim.Skills;

namespace BarbarianSim.EventFactories;

public class ChallengingShoutEventFactory
{
    public ChallengingShoutEventFactory(AuraAppliedEventFactory auraAppliedEventFactory, RaidLeaderProcEventFactory raidLeaderProcEventFactory, BoomingVoice boomingVoice)
    {
        _auraAppliedEventFactory = auraAppliedEventFactory;
        _raidLeaderProcEventFactory = raidLeaderProcEventFactory;
        _boomingVoice = boomingVoice;
    }

    private readonly AuraAppliedEventFactory _auraAppliedEventFactory;
    private readonly RaidLeaderProcEventFactory _raidLeaderProcEventFactory;
    private readonly BoomingVoice _boomingVoice;

    public ChallengingShoutEvent Create(double timestamp) => new(_auraAppliedEventFactory, _raidLeaderProcEventFactory, _boomingVoice, timestamp);
}
