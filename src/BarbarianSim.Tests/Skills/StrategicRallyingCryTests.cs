using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.Skills;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Skills;

public class StrategicRallyingCryTests
{
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly StrategicRallyingCry _skill = new();

    [Fact]
    public void Creates_FortifyGeneratedEvent()
    {
        _state.Config.Skills.Add(Skill.StrategicRallyingCry, 1);
        _state.Player.BaseLife = 4000;
        var rallyingCryEvent = new RallyingCryEvent(123);

        _skill.ProcessEvent(rallyingCryEvent, _state);

        _state.Events.Should().ContainSingle(e => e is FortifyGeneratedEvent);
        _state.Events.OfType<FortifyGeneratedEvent>().Single().Timestamp.Should().Be(123);
        _state.Events.OfType<FortifyGeneratedEvent>().Single().Amount.Should().Be(400);
    }

    [Fact]
    public void Does_Nothing_If_Not_Skilled()
    {
        _state.Config.Skills.Add(Skill.StrategicRallyingCry, 0);
        _state.Player.BaseLife = 4000;
        var rallyingCryEvent = new RallyingCryEvent(123);

        _skill.ProcessEvent(rallyingCryEvent, _state);

        _state.Events.Should().NotContain(e => e is FortifyGeneratedEvent);
    }
}
