using BarbarianSim.Config;
using BarbarianSim.Skills;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class MaxLifeCalculatorTests
{
    private readonly Mock<EnhancedChallengingShout> _mockEnhancedChallengingShout = TestHelpers.CreateMock<EnhancedChallengingShout>();
    private readonly Mock<SimLogger> _mockSimLogger = TestHelpers.CreateMock<SimLogger>();
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly MaxLifeCalculator _calculator;

    public MaxLifeCalculatorTests()
    {
        _mockEnhancedChallengingShout.Setup(m => m.GetMaxLifeMultiplier(It.IsAny<SimulationState>()))
                                      .Returns(1.0);

        _calculator = new MaxLifeCalculator(_mockEnhancedChallengingShout.Object, _mockSimLogger.Object);
    }

    [Fact]
    public void Includes_Base_Life()
    {
        _state.Player.BaseLife = 1000;

        var result = _calculator.Calculate(_state);

        result.Should().Be(1000);
    }

    [Fact]
    public void Includes_Stats_From_Gear()
    {
        _state.Config.Gear.Helm.MaxLife = 100;
        _state.Config.Gear.Chest.MaxLife = 100;
        _state.Player.BaseLife = 1000;

        var result = _calculator.Calculate(_state);

        result.Should().Be(1200);
    }

    [Fact]
    public void Includes_Stats_From_Paragon()
    {
        _state.Config.Paragon.MaxLife = 100;
        _state.Config.Gear.Chest.MaxLife = 100;
        _state.Player.BaseLife = 1000;

        var result = _calculator.Calculate(_state);

        result.Should().Be(1200);
    }

    [Fact]
    public void Includes_Bonus_From_EnhancedChallengingShout()
    {
        _state.Config.Gear.Helm.MaxLife = 100;
        _state.Config.Gear.Chest.MaxLife = 100;
        _state.Player.BaseLife = 1000;
        _mockEnhancedChallengingShout.Setup(m => m.GetMaxLifeMultiplier(It.IsAny<SimulationState>()))
                                      .Returns(1.2);

        var result = _calculator.Calculate(_state);

        result.Should().Be(1440);
    }

    [Fact]
    public void Includes_MaxLifePercent_From_Gear()
    {
        _state.Config.Gear.Helm.MaxLifePercent = 20;
        _state.Config.Gear.Chest.MaxLifePercent = 10;
        _state.Player.BaseLife = 1000;

        var result = _calculator.Calculate(_state);

        result.Should().Be(1300);
    }
}
