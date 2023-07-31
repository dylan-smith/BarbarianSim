using BarbarianSim.Config;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class DodgeCalculatorTests
{
    private readonly Mock<DexterityCalculator> _mockDexterityCalculator = TestHelpers.CreateMock<DexterityCalculator>();
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly DodgeCalculator _calculator;

    public DodgeCalculatorTests()
    {
        _mockDexterityCalculator.Setup(x => x.Calculate(It.IsAny<SimulationState>())).Returns(0.0);
        _calculator = new DodgeCalculator(_mockDexterityCalculator.Object);
    }

    [Fact]
    public void Returns_0_By_Default()
    {

        var result = _calculator.Calculate(_state);

        result.Should().Be(0.0);
    }

    [Fact]
    public void Includes_Dodge_Gear_Bonus()
    {
        _state.Config.Gear.Helm.Dodge = 42;

        var result = _calculator.Calculate(_state);

        result.Should().Be(0.42);
    }

    [Fact]
    public void Includes_Dodge_Paragon_Bonus()
    {
        _state.Config.Paragon.Dodge = 42;

        var result = _calculator.Calculate(_state);

        result.Should().Be(0.42);
    }

    [Fact]
    public void Includes_Dexterity_Bonus()
    {
        _mockDexterityCalculator.Setup(x => x.Calculate(_state)).Returns(400.0);

        var result = _calculator.Calculate(_state);

        result.Should().Be(0.04);
    }
}
