using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class ThornsCalculatorTests
{
    private readonly Mock<MaxLifeCalculator> _mockMaxLifeCalculator = TestHelpers.CreateMock<MaxLifeCalculator>();
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly ThornsCalculator _calculator;

    public ThornsCalculatorTests()
    {
        _mockMaxLifeCalculator.Setup(m => m.Calculate(It.IsAny<SimulationState>())).Returns(1000);
        _calculator = new ThornsCalculator(_mockMaxLifeCalculator.Object);
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
    public void Includes_Bonus_From_StrategicChallengingShout()
    {
        _state.Config.Skills.Add(Skill.StrategicChallengingShout, 1);
        _state.Player.Auras.Add(Aura.ChallengingShout);

        var result = _calculator.Calculate(_state);

        result.Should().Be(300);
    }

    [Fact]
    public void Returns_0_When_No_Bonuses()
    {
        var result = _calculator.Calculate(_state);

        result.Should().Be(0);
    }
}
