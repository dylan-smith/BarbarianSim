using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.Paragon;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.Paragon;

public class WrathTests
{
    private readonly Mock<SimLogger> _mockSimLogger = TestHelpers.CreateMock<SimLogger>();
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly Wrath _paragon;

    public WrathTests()
    {
        _state.Config.ParagonNodes.Add(ParagonNode.Wrath);
        _paragon = new Wrath(_mockSimLogger.Object);
    }

    [Fact]
    public void Creates_FuryGeneratedEvent()
    {
        var damageEvent = new DamageEvent(123, null, 500, DamageType.Physical | DamageType.CriticalStrike, DamageSource.None, SkillType.Core, _state.Enemies.First());

        _paragon.ProcessEvent(damageEvent, _state);

        _state.Events.Should().ContainSingle(e => e is FuryGeneratedEvent);
        _state.Events.OfType<FuryGeneratedEvent>().Single().Timestamp.Should().Be(123);
        _state.Events.OfType<FuryGeneratedEvent>().Single().BaseFury.Should().Be(3);
    }

    [Fact]
    public void Does_Nothing_If_Paragon_Not_Active()
    {
        _state.Config.ParagonNodes.Remove(ParagonNode.Wrath);
        var damageEvent = new DamageEvent(123, null, 500, DamageType.CriticalStrike, DamageSource.None, SkillType.Core, _state.Enemies.First());

        _paragon.ProcessEvent(damageEvent, _state);

        _state.Events.Should().NotContain(e => e is FuryGeneratedEvent);
    }

    [Fact]
    public void Only_Triggers_On_Crits()
    {
        var damageEvent = new DamageEvent(123, null, 500, DamageType.Physical, DamageSource.None, SkillType.Core, _state.Enemies.First());

        _paragon.ProcessEvent(damageEvent, _state);

        _state.Events.Should().NotContain(e => e is FuryGeneratedEvent);
    }

    [Fact]
    public void Only_Triggers_On_Skills()
    {
        var damageEvent = new DamageEvent(123, null, 500, DamageType.Physical | DamageType.CriticalStrike, DamageSource.None, SkillType.None, _state.Enemies.First());

        _paragon.ProcessEvent(damageEvent, _state);

        _state.Events.Should().NotContain(e => e is FuryGeneratedEvent);
    }
}
