using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.EventHandlers;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.EventHandlers;

public class BleedCompletedEventHandlerTests
{
    private readonly Mock<TotalDamageMultiplierCalculator> _mockTotalDamageMultiplierCalculator = TestHelpers.CreateMock<TotalDamageMultiplierCalculator>();
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly BleedCompletedEventHandler _handler;

    public BleedCompletedEventHandlerTests()
    {
        _mockTotalDamageMultiplierCalculator.Setup(m => m.Calculate(It.IsAny<SimulationState>(), It.IsAny<DamageType>(), It.IsAny<EnemyState>(), It.IsAny<SkillType>(), It.IsAny<DamageSource>(), It.IsAny<GearItem>()))
                                            .Returns(1.0);

        _handler = new BleedCompletedEventHandler(_mockTotalDamageMultiplierCalculator.Object);
    }

    [Fact]
    public void Removes_Bleeding_Aura()
    {
        _state.Enemies.First().Auras.Add(Aura.Bleeding);
        var bleedCompletedEvent = new BleedCompletedEvent(123.0, 500.0, _state.Enemies.First());

        _handler.ProcessEvent(bleedCompletedEvent, _state);

        _state.Enemies.First().Auras.Should().NotContain(Aura.Bleeding);
    }

    [Fact]
    public void Leaves_Bleeding_Aura_If_Other_BleedCompletedEvents_Exist()
    {
        _state.Enemies.First().Auras.Add(Aura.Bleeding);
        _state.Events.Add(new BleedCompletedEvent(126.0, 300.0, _state.Enemies.First()));
        var bleedCompletedEvent = new BleedCompletedEvent(123.0, 500.0, _state.Enemies.First());

        _handler.ProcessEvent(bleedCompletedEvent, _state);

        _state.Enemies.First().Auras.Should().Contain(Aura.Bleeding);
    }

    [Fact]
    public void Creates_DamageEvent()
    {
        var bleedCompletedEvent = new BleedCompletedEvent(123.0, 500.0, _state.Enemies.First());

        _handler.ProcessEvent(bleedCompletedEvent, _state);

        _state.Events.Should().Contain(bleedCompletedEvent.DamageEvent);
        _state.Events.Should().ContainSingle(e => e is DamageEvent);
        bleedCompletedEvent.DamageEvent.Timestamp.Should().Be(123.0);
        bleedCompletedEvent.DamageEvent.Damage.Should().Be(500.0);
        bleedCompletedEvent.DamageEvent.DamageType.Should().Be(DamageType.DamageOverTime);
        bleedCompletedEvent.DamageEvent.DamageSource.Should().Be(DamageSource.Bleeding);
        bleedCompletedEvent.DamageEvent.Target.Should().Be(_state.Enemies.First());
        bleedCompletedEvent.DamageEvent.SkillType.Should().Be(SkillType.None);
    }

    [Fact]
    public void Applied_Damage_Multipliers()
    {
        _mockTotalDamageMultiplierCalculator.Setup(m => m.Calculate(_state, DamageType.Physical | DamageType.DamageOverTime, _state.Enemies.First(), SkillType.None, DamageSource.Bleeding, null)).Returns(2.5);
        var bleedCompletedEvent = new BleedCompletedEvent(123.0, 500.0, _state.Enemies.First());

        _handler.ProcessEvent(bleedCompletedEvent, _state);

        _state.Events.Should().ContainSingle(e => e is DamageEvent);
        bleedCompletedEvent.DamageEvent.Damage.Should().Be(1250.0);
    }
}
