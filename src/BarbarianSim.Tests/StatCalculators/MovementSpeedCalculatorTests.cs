using BarbarianSim.Aspects;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Skills;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class MovementSpeedCalculatorTests
{
    private readonly Mock<PrimeWrathOfTheBerserker> _mockPrimeWrathOfTheBerserker = TestHelpers.CreateMock<PrimeWrathOfTheBerserker>();
    private readonly Mock<GhostwalkerAspect> _mockGhostwalkerAspect = TestHelpers.CreateMock<GhostwalkerAspect>();
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly MovementSpeedCalculator _calculator;

    public MovementSpeedCalculatorTests()
    {
        _mockPrimeWrathOfTheBerserker.Setup(m => m.GetMovementSpeedIncrease(It.IsAny<SimulationState>()))
                                     .Returns(0);
        _mockGhostwalkerAspect.Setup(m => m.GetMovementSpeedIncrease(It.IsAny<SimulationState>()))
                              .Returns(0);

        _calculator = new(_mockPrimeWrathOfTheBerserker.Object, _mockGhostwalkerAspect.Object);
    }

    [Fact]
    public void Includes_Bonus_From_Gear()
    {
        _state.Config.Gear.Helm.MovementSpeed = 12.0;

        var result = _calculator.Calculate(_state);

        result.Should().Be(1.12);
    }

    [Fact]
    public void Bonus_From_Berserking()
    {
        _state.Config.Gear.Helm.MovementSpeed = 12.0;
        _state.Player.Auras.Add(Aura.Berserking);

        var result = _calculator.Calculate(_state);

        result.Should().Be(1.27);
    }

    [Fact]
    public void Bonus_From_RallyingCry()
    {
        _state.Config.Gear.Helm.MovementSpeed = 12.0;
        _state.Player.Auras.Add(Aura.RallyingCry);

        var result = _calculator.Calculate(_state);

        result.Should().Be(1.42);
    }

    [Fact]
    public void Bonus_From_PrimeWrathOfTheBerserker()
    {
        _mockPrimeWrathOfTheBerserker.Setup(m => m.GetMovementSpeedIncrease(It.IsAny<SimulationState>()))
                                      .Returns(20);
        _state.Config.Gear.Helm.MovementSpeed = 12.0;

        var result = _calculator.Calculate(_state);

        result.Should().Be(1.32);
    }

    [Fact]
    public void Bonus_From_Ghostwalker()
    {
        _mockGhostwalkerAspect.Setup(m => m.GetMovementSpeedIncrease(It.IsAny<SimulationState>()))
                              .Returns(25);
        _state.Config.Gear.Helm.MovementSpeed = 12.0;

        var result = _calculator.Calculate(_state);

        result.Should().Be(1.37);
    }
}
