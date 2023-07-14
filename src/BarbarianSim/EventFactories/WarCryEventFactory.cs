using BarbarianSim.Events;
using BarbarianSim.Skills;

namespace BarbarianSim.EventFactories;

public class WarCryEventFactory
{
    public WarCryEventFactory(BoomingVoice boomingVoice, AuraAppliedEventFactory auraAppliedEventFactory, FortifyGeneratedEventFactory fortifyGeneratedEventFactory, RaidLeaderProcEventFactory raidLeaderProcEventFactory)
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

    public WarCryEvent Create(double timestamp) =>
        new(_boomingVoice, _auraAppliedEventFactory, _fortifyGeneratedEventFactory, _raidLeaderProcEventFactory, timestamp);
}
