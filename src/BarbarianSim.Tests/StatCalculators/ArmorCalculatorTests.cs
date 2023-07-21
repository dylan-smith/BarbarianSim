using BarbarianSim.Aspects;
using BarbarianSim.Config;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class ArmorCalculatorTests
{
    private readonly Mock<StrengthCalculator> _mockStrengthCalculator = TestHelpers.CreateMock<StrengthCalculator>();
    private readonly Mock<AspectOfDisobedience> _mockAspectOfDisobedience = TestHelpers.CreateMock<AspectOfDisobedience>();
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly ArmorCalculator _calculator;

    public ArmorCalculatorTests()
    {
        _mockStrengthCalculator.Setup(x => x.Calculate(It.IsAny<SimulationState>())).Returns(0.0);
        _mockAspectOfDisobedience.Setup(m => m.GetArmorBonus(It.IsAny<SimulationState>())).Returns(1.0);
        _calculator = new ArmorCalculator(_mockStrengthCalculator.Object, _mockAspectOfDisobedience.Object);
    }

    [Fact]
    public void Starts_At_Zero()
    {
        var result = _calculator.Calculate(_state);

        result.Should().Be(0.0);
    }

    [Fact]
    public void Includes_Gear_Bonus()
    {
        _state.Config.Gear.Helm.Armor = 42;

        var result = _calculator.Calculate(_state);

        result.Should().Be(42);
    }

    [Fact]
    public void Includes_Strength_Bonus()
    {
        _mockStrengthCalculator.Setup(x => x.Calculate(It.IsAny<SimulationState>())).Returns(42.0);

        var result = _calculator.Calculate(_state);

        result.Should().Be(42);
    }

    [Fact]
    public void Includes_AspectOfDisobedience_Bonus()
    {
        _state.Config.Gear.Helm.Armor = 800;
        _mockAspectOfDisobedience.Setup(m => m.GetArmorBonus(_state)).Returns(1.4);

        _calculator.Calculate(_state).Should().Be(1120);
    }
}
