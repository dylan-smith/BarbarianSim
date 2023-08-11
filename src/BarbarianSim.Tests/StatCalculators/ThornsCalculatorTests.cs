using BarbarianSim.Config;
using BarbarianSim.Skills;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class ThornsCalculatorTests
{
    private readonly Mock<StrategicChallengingShout> _mockStrategicChallengingShout = TestHelpers.CreateMock<StrategicChallengingShout>();
    private readonly Mock<SimLogger> _mockSimLogger = TestHelpers.CreateMock<SimLogger>();
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly ThornsCalculator _calculator;

    public ThornsCalculatorTests()
    {
        _mockStrategicChallengingShout.Setup(m => m.GetThorns(It.IsAny<SimulationState>())).Returns(0);
        _calculator = new ThornsCalculator(_mockStrategicChallengingShout.Object, _mockSimLogger.Object);
    }

    [Fact]
    public void Includes_Stats_From_Gear()
    {
        _state.Config.Gear.Helm.Thorns = 100;
        _state.Config.Gear.Chest.Thorns = 100;

        var result = _calculator.Calculate(_state);

        result.Should().Be(200);
    }

    [Fact]
    public void Includes_Stats_From_Paragon()
    {
        _state.Config.Paragon.Thorns = 100;
        _state.Config.Gear.Chest.Thorns = 100;

        var result = _calculator.Calculate(_state);

        result.Should().Be(200);
    }

    [Fact]
    public void Includes_Bonus_From_StrategicChallengingShout()
    {
        _mockStrategicChallengingShout.Setup(m => m.GetThorns(It.IsAny<SimulationState>())).Returns(320);

        var result = _calculator.Calculate(_state);

        result.Should().Be(320);
    }

    [Fact]
    public void Returns_0_When_No_Bonuses()
    {
        var result = _calculator.Calculate(_state);

        result.Should().Be(0);
    }
}
