using BarbarianSim.Aspects;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Aspects;

public class GhostwalkerAspectTests
{
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly GhostwalkerAspect _aspect = new();

    public GhostwalkerAspectTests()
    {
        _aspect.Speed = 25;
        _state.Config.Gear.Helm.Aspect = _aspect;
    }

    [Fact]
    public void Creates_AuraAppliedEvent()
    {
        var unstoppableAppliedEvent = new AuraAppliedEvent(123, 5, Aura.Unstoppable);

        _aspect.ProcessEvent(unstoppableAppliedEvent, _state);

        _state.Events.Should().ContainSingle(e => e is AuraAppliedEvent);
        _state.Events.OfType<AuraAppliedEvent>().First().Timestamp.Should().Be(123);
        _state.Events.OfType<AuraAppliedEvent>().First().Duration.Should().Be(9);
        _state.Events.OfType<AuraAppliedEvent>().First().Aura.Should().Be(Aura.Ghostwalker);
    }

    [Fact]
    public void Does_Nothing_When_Not_Equipped()
    {
        _state.Config.Gear.Helm.Aspect = null;
        var unstoppableAppliedEvent = new AuraAppliedEvent(123, 5, Aura.Unstoppable);

        _aspect.ProcessEvent(unstoppableAppliedEvent, _state);

        _state.Events.Should().NotContain(e => e is AuraAppliedEvent);
    }

    [Fact]
    public void GetMovementSpeedIncrease_Returns_Bonus_When_Active()
    {
        _state.Player.Auras.Add(Aura.Ghostwalker);
        _state.Config.Gear.Ring1.Aspect = _aspect;

        _aspect.GetMovementSpeedIncrease(_state).Should().Be(25);
    }

    [Fact]
    public void GetMovementSpeedIncrease_Returns_0_When_Not_Active()
    {
        _state.Config.Gear.Ring1.Aspect = _aspect;

        _aspect.GetMovementSpeedIncrease(_state).Should().Be(0);
    }

    [Fact]
    public void GetMovementSpeedIncrease_Returns_0_When_Not_Equipped()
    {
        _state.Config.Gear.Helm.Aspect = null;
        _state.Player.Auras.Add(Aura.Ghostwalker);

        _aspect.GetMovementSpeedIncrease(_state).Should().Be(0);
    }
}
