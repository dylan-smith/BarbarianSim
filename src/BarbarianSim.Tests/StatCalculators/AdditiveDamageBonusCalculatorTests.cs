using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class AdditiveDamageBonusCalculatorTests
{
    private readonly Mock<PhysicalDamageCalculator> _mockPhysicalDamageCalculator = TestHelpers.CreateMock<PhysicalDamageCalculator>();
    private readonly Mock<DamageToCloseCalculator> _mockDamageToCloseCalculator = TestHelpers.CreateMock<DamageToCloseCalculator>();
    private readonly Mock<DamageToInjuredCalculator> _mockDamageToInjuredCalculator = TestHelpers.CreateMock<DamageToInjuredCalculator>();
    private readonly Mock<DamageToSlowedCalculator> _mockDamageToSlowedCalculator = TestHelpers.CreateMock<DamageToSlowedCalculator>();
    private readonly Mock<DamageToCrowdControlledCalculator> _mockDamageToCrowdControlledCalculator = TestHelpers.CreateMock<DamageToCrowdControlledCalculator>();
    private readonly Mock<SimLogger> _mockSimLogger = TestHelpers.CreateMock<SimLogger>();
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly AdditiveDamageBonusCalculator _calculator;

    public AdditiveDamageBonusCalculatorTests()
    {
        _mockPhysicalDamageCalculator.Setup(m => m.Calculate(It.IsAny<SimulationState>(), It.IsAny<DamageType>())).Returns(0.0);
        _mockDamageToCloseCalculator.Setup(m => m.Calculate(It.IsAny<SimulationState>())).Returns(0.0);
        _mockDamageToInjuredCalculator.Setup(m => m.Calculate(It.IsAny<SimulationState>(), It.IsAny<EnemyState>())).Returns(0.0);
        _mockDamageToSlowedCalculator.Setup(m => m.Calculate(It.IsAny<SimulationState>(), It.IsAny<EnemyState>())).Returns(0.0);
        _mockDamageToCrowdControlledCalculator.Setup(m => m.Calculate(It.IsAny<SimulationState>(), It.IsAny<EnemyState>())).Returns(0.0);

        _calculator = new(_mockPhysicalDamageCalculator.Object,
                          _mockDamageToCloseCalculator.Object,
                          _mockDamageToInjuredCalculator.Object,
                          _mockDamageToSlowedCalculator.Object,
                          _mockDamageToCrowdControlledCalculator.Object,
                          _mockSimLogger.Object);
    }

    [Fact]
    public void Returns_1_When_No_Additive_Damage_Bonuses()
    {
        var result = _calculator.Calculate(_state, DamageType.Physical, _state.Enemies.First());

        result.Should().Be(1.0);
    }

    [Fact]
    public void Includes_Physical_Damage()
    {
        _mockPhysicalDamageCalculator.Setup(m => m.Calculate(_state, DamageType.Physical)).Returns(12.0);

        var result = _calculator.Calculate(_state, DamageType.Physical, _state.Enemies.First());

        result.Should().Be(1.12);
    }

    [Fact]
    public void Includes_Damage_To_Close()
    {
        _mockDamageToCloseCalculator.Setup(m => m.Calculate(_state)).Returns(12.0);

        var result = _calculator.Calculate(_state, DamageType.Physical, _state.Enemies.First());

        result.Should().Be(1.12);
    }

    [Fact]
    public void Includes_Damage_To_Injured()
    {
        _mockDamageToInjuredCalculator.Setup(m => m.Calculate(_state, _state.Enemies.First())).Returns(12.0);

        var result = _calculator.Calculate(_state, DamageType.Physical, _state.Enemies.First());

        result.Should().Be(1.12);
    }

    [Fact]
    public void Includes_Damage_To_Slowed()
    {
        _mockDamageToSlowedCalculator.Setup(m => m.Calculate(_state, _state.Enemies.First())).Returns(12.0);

        var result = _calculator.Calculate(_state, DamageType.Physical, _state.Enemies.First());

        result.Should().Be(1.12);
    }

    [Fact]
    public void Includes_Damage_To_Crowd_Controlled()
    {
        _mockDamageToCrowdControlledCalculator.Setup(m => m.Calculate(_state, _state.Enemies.First())).Returns(12.0);

        var result = _calculator.Calculate(_state, DamageType.Physical, _state.Enemies.First());

        result.Should().Be(1.12);
    }

    [Fact]
    public void Adds_All_Additive_Damage_Bonuses()
    {
        _mockPhysicalDamageCalculator.Setup(m => m.Calculate(_state, DamageType.Physical)).Returns(12.0);
        _mockDamageToCloseCalculator.Setup(m => m.Calculate(_state)).Returns(12.0);
        _mockDamageToInjuredCalculator.Setup(m => m.Calculate(_state, _state.Enemies.First())).Returns(12.0);
        _mockDamageToSlowedCalculator.Setup(m => m.Calculate(_state, _state.Enemies.First())).Returns(12.0);
        _mockDamageToCrowdControlledCalculator.Setup(m => m.Calculate(_state, _state.Enemies.First())).Returns(12.0);

        var result = _calculator.Calculate(_state, DamageType.Physical, _state.Enemies.First());

        result.Should().Be(1.6);
    }
}
