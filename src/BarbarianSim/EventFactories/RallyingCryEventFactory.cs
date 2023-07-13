using BarbarianSim.Events;
using BarbarianSim.Skills;

namespace BarbarianSim.EventFactories;

public class RallyingCryEventFactory
{
    public RallyingCryEventFactory(BoomingVoice boomingVoice, AuraAppliedEventFactory auraAppliedEventFactory, FuryGeneratedEventFactory furyGeneratedEventFactory, FortifyGeneratedEventFactory fortifyGeneratedEventFactory, RaidLeaderProcEventFactory raidLeaderProcEventFactory)
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

    public RallyingCryEvent Create(double timestamp) => 
        new(_boomingVoice, _auraAppliedEventFactory, _furyGeneratedEventFactory, _fortifyGeneratedEventFactory, _raidLeaderProcEventFactory, timestamp);
}
