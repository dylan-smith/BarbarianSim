using BarbarianSim.Aspects;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.Aspects;

public sealed class RamaladnisMagnumOpusTests
{
    private readonly Mock<SimLogger> _mockSimLogger = TestHelpers.CreateMock<SimLogger>();
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly RamaladnisMagnumOpus _aspect;

    public RamaladnisMagnumOpusTests()
    {
        _aspect = new(_mockSimLogger.Object) { DamagePerFury = 0.3 };
        _state.Config.Gear.OneHandLeft.Aspect = _aspect;
    }

    [Fact]
    public void Creates_RamaladnisMagnumOpusEvent()
    {
        _aspect.ProcessEvent(new SimulationStartedEvent(0), _state);

        _state.Events.Should().ContainSingle(e => e is RamaladnisMagnumOpusEvent);
        _state.Events.OfType<RamaladnisMagnumOpusEvent>().First().Timestamp.Should().Be(1.0);
    }

    [Fact]
    public void Does_Nothing_If_Not_Equipped()
    {
        _state.Config.Gear.OneHandLeft.Aspect = null;
        _aspect.ProcessEvent(new SimulationStartedEvent(0), _state);

        _state.Events.Should().NotContain(e => e is RamaladnisMagnumOpusEvent);
    }

    [Fact]
    public void GetDamageBonus_When_Active()
    {
        var weapon = new GearItem
        {
            Expertise = Expertise.OneHandedSword,
            Aspect = _aspect
        };
        _state.Player.Fury = 80;
        _aspect.GetDamageBonus(_state, weapon).Should().Be(1.24);
    }

    [Fact]
    public void GetDamageBonus_Returns_1_When_Different_Weapon_Used()
    {
        var weapon = new GearItem
        {
            Expertise = Expertise.OneHandedSword,
        };
        _aspect.GetDamageBonus(_state, weapon).Should().Be(1.0);
    }
}
