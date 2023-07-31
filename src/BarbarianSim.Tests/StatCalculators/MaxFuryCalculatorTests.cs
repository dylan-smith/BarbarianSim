using BarbarianSim.Config;
using BarbarianSim.Skills;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class MaxFuryCalculatorTests
{
    private readonly Mock<TemperedFury> _mockTemperedFury = TestHelpers.CreateMock<TemperedFury>();
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly MaxFuryCalculator _calculator;

    public MaxFuryCalculatorTests()
    {
        _mockTemperedFury.Setup(m => m.GetMaximumFury(It.IsAny<SimulationState>())).Returns(0);
        _calculator = new MaxFuryCalculator(_mockTemperedFury.Object);
    }

    [Fact]
    public void Includes_Base_Fury()
    {
        var result = _calculator.Calculate(_state);

        result.Should().Be(100);
    }

    [Fact]
    public void Includes_Stats_From_Gear()
    {
        _state.Config.Gear.Helm.MaxFury = 8;
        _state.Config.Gear.Chest.MaxFury = 12;

        var result = _calculator.Calculate(_state);

        result.Should().Be(120);
    }

    [Fact]
    public void Includes_Stats_From_Paragon()
    {
        _state.Config.Paragon.MaxFury = 8;
        _state.Config.Gear.Chest.MaxFury = 12;

        var result = _calculator.Calculate(_state);

        result.Should().Be(120);
    }

    [Fact]
    public void Includes_Bonus_From_TemperedFury()
    {
        _mockTemperedFury.Setup(m => m.GetMaximumFury(It.IsAny<SimulationState>())).Returns(6);
        var result = _calculator.Calculate(_state);

        result.Should().Be(106);
    }
}
