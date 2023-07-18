using BarbarianSim.Abilities;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.Skills;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Skills;

public class TacticalRallyingCryTests
{
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly TacticalRallyingCry _skill = new();

    [Fact]
    public void Creates_FuryGeneratedEvent()
    {
        _state.Config.Skills.Add(Skill.TacticalRallyingCry, 1);
        var rallyingCryEvent = new RallyingCryEvent(123);

        _skill.ProcessEvent(rallyingCryEvent, _state);

        _state.Events.Should().ContainSingle(e => e is FuryGeneratedEvent);
        _state.Events.OfType<FuryGeneratedEvent>().Single().Timestamp.Should().Be(123);
        _state.Events.OfType<FuryGeneratedEvent>().Single().BaseFury.Should().Be(RallyingCry.FURY_FROM_TACTICAL_RALLYING_CRY);
    }

    [Fact]
    public void Does_Nothing_If_Not_Skilled()
    {
        _state.Config.Skills.Add(Skill.TacticalRallyingCry, 0);
        var rallyingCryEvent = new RallyingCryEvent(123);

        _skill.ProcessEvent(rallyingCryEvent, _state);

        _state.Events.Should().NotContain(e => e is FuryGeneratedEvent);
    }
}
