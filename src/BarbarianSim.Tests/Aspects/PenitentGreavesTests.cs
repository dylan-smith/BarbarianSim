using BarbarianSim.Aspects;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.Aspects;

public sealed class PenitentGreavesTests
{
    private readonly Mock<SimLogger> _mockSimLogger = TestHelpers.CreateMock<SimLogger>();
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly PenitentGreaves _aspect;

    public PenitentGreavesTests()
    {
        _aspect = new(_mockSimLogger.Object);
        _aspect.Damage = 10;
        _state.Config.Gear.Helm.Aspect = _aspect;
    }

    [Fact]
    public void Creates_AuraAppliedEvent()
    {
        _aspect.ProcessEvent(new SimulationStartedEvent(0), _state);

        _state.Events.Should().ContainSingle(e => e is AuraAppliedEvent);
        _state.Events.OfType<AuraAppliedEvent>().First().Timestamp.Should().Be(0);
        _state.Events.OfType<AuraAppliedEvent>().First().Duration.Should().Be(0);
        _state.Events.OfType<AuraAppliedEvent>().First().Aura.Should().Be(Aura.Chill);
        _state.Events.OfType<AuraAppliedEvent>().First().Target.Should().Be(_state.Enemies.First());
    }

    [Fact]
    public void Creates_AuraAppliedEvent_For_Each_Enemy()
    {
        var config = new SimulationConfig();
        config.EnemySettings.NumberOfEnemies = 3;
        config.Gear.Helm.Aspect = _aspect;
        var state = new SimulationState(config);

        _aspect.ProcessEvent(new SimulationStartedEvent(0), state);

        state.Events.OfType<AuraAppliedEvent>().Should().HaveCount(3);
    }

    [Fact]
    public void Does_Nothing_When_Not_Equipped()
    {
        _state.Config.Gear.Helm.Aspect = null;
        _aspect.ProcessEvent(new SimulationStartedEvent(0), _state);

        _state.Events.Should().NotContain(e => e is AuraAppliedEvent);
    }

    [Fact]
    public void GetDamageBonus_When_Active()
    {
        _aspect.GetDamageBonus(_state).Should().Be(1.1);
    }

    [Fact]
    public void GetDamageBonus_Returns_1_When_Not_Equipped()
    {
        _state.Config.Gear.Helm.Aspect = null;
        _aspect.GetDamageBonus(_state).Should().Be(1.0);
    }
}
