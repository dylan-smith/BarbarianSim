using BarbarianSim.Abilities;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Skills;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class ResourceGenerationCalculatorTests
{
    private readonly Mock<WillpowerCalculator> _mockWillpowerCalculator = TestHelpers.CreateMock<WillpowerCalculator>();
    private readonly Mock<RallyingCry> _mockRallyingCry = TestHelpers.CreateMock<RallyingCry>();
    private readonly Mock<ProlificFury> _mockProlificFury = TestHelpers.CreateMock<ProlificFury>();
    private readonly Mock<TacticalRallyingCry> _mockTacticalRallyingCry = TestHelpers.CreateMock<TacticalRallyingCry>();
    private readonly Mock<PrimeWrathOfTheBerserker> _mockPrimeWrathOfTheBerserker = TestHelpers.CreateMock<PrimeWrathOfTheBerserker>();
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly ResourceGenerationCalculator _calculator;

    public ResourceGenerationCalculatorTests()
    {
        _mockWillpowerCalculator.Setup(m => m.Calculate(It.IsAny<SimulationState>())).Returns(0.0);
        _mockRallyingCry.Setup(m => m.GetResourceGeneration(It.IsAny<SimulationState>())).Returns(1.0);
        _mockProlificFury.Setup(m => m.GetFuryGeneration(It.IsAny<SimulationState>())).Returns(1.0);
        _mockTacticalRallyingCry.Setup(m => m.GetResourceGeneration(It.IsAny<SimulationState>())).Returns(1.0);
        _mockPrimeWrathOfTheBerserker.Setup(m => m.GetResourceGeneration(It.IsAny<SimulationState>())).Returns(1.0);

        _calculator = new ResourceGenerationCalculator(_mockWillpowerCalculator.Object, _mockRallyingCry.Object, _mockProlificFury.Object, _mockTacticalRallyingCry.Object, _mockPrimeWrathOfTheBerserker.Object);
    }

    [Fact]
    public void Returns_1_By_Default()
    {
        var result = _calculator.Calculate(_state);

        result.Should().Be(1.0);
    }

    [Fact]
    public void Includes_ResourceGeneration_Gear_Bonus()
    {
        _state.Config.Gear.Helm.ResourceGeneration = 42;

        var result = _calculator.Calculate(_state);

        result.Should().Be(1.42);
    }

    [Fact]
    public void Includes_Willpower_Bonus()
    {
        _mockWillpowerCalculator.Setup(m => m.Calculate(_state)).Returns(400.0);

        var result = _calculator.Calculate(_state);

        result.Should().Be(1.12);
    }

    [Fact]
    public void Includes_RallyingCry_Bonus()
    {
        _state.Player.Auras.Add(Aura.RallyingCry);
        _mockRallyingCry.Setup(m => m.GetResourceGeneration(_state)).Returns(1.56);

        var result = _calculator.Calculate(_state);

        result.Should().Be(1.56);
    }

    [Fact]
    public void RallyingCry_Bonus_And_Gear_Multiply_Together()
    {
        _mockRallyingCry.Setup(m => m.GetResourceGeneration(_state)).Returns(1.56);
        _state.Config.Gear.Helm.ResourceGeneration = 30;

        var result = _calculator.Calculate(_state);

        result.Should().Be(1.3 * 1.56);
    }

    [Fact]
    public void TacticalRallyingCry_Bonus_Multiplied_In()
    {
        _mockTacticalRallyingCry.Setup(m => m.GetResourceGeneration(_state)).Returns(1.2);
        _mockRallyingCry.Setup(m => m.GetResourceGeneration(_state)).Returns(1.56);
        _state.Config.Gear.Helm.ResourceGeneration = 30;

        var result = _calculator.Calculate(_state);

        result.Should().Be(1.3 * 1.56 * 1.2);
    }

    [Fact]
    public void ProlificFury_Bonus_Multiplied_In()
    {
        _mockProlificFury.Setup(m => m.GetFuryGeneration(_state)).Returns(1.18);
        _state.Config.Gear.Helm.ResourceGeneration = 30;

        var result = _calculator.Calculate(_state);

        result.Should().Be(1.3 * 1.18);
    }

    [Fact]
    public void PrimeWrathOfTheBerserker_Bonus_Multiplied_In()
    {
        _mockPrimeWrathOfTheBerserker.Setup(m => m.GetResourceGeneration(It.IsAny<SimulationState>())).Returns(1.3);
        _state.Config.Gear.Helm.ResourceGeneration = 30;

        var result = _calculator.Calculate(_state);

        result.Should().Be(1.3 * 1.3);
    }
}
