using BarbarianSim.Arsenal;
using BarbarianSim.Config;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class DamageWhileHealthyCalculatorTests
{
    private readonly Mock<MaxLifeCalculator> _mockMaxLifeCalculator = TestHelpers.CreateMock<MaxLifeCalculator>();
    private readonly Mock<PolearmExpertise> _mockPolearmExpertise = TestHelpers.CreateMock<PolearmExpertise>();
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly DamageWhileHealthyCalculator _calculator;

    public DamageWhileHealthyCalculatorTests()
    {
        _mockMaxLifeCalculator.Setup(m => m.Calculate(It.IsAny<SimulationState>())).Returns(1000.0);
        _mockPolearmExpertise.Setup(m => m.GetHealthyDamageMultiplier(It.IsAny<GearItem>())).Returns(1.0);
        _calculator = new DamageWhileHealthyCalculator(_mockMaxLifeCalculator.Object, _mockPolearmExpertise.Object);
    }

    [Fact]
    public void Returns_1_When_Not_Healthy()
    {
        _state.Player.Life = 600;

        var result = _calculator.Calculate(_state, _state.Config.Gear.TwoHandSlashing);

        result.Should().Be(1);
    }

    [Fact]
    public void Includes_PolearmExpertise_Bonus()
    {
        _mockPolearmExpertise.Setup(m => m.GetHealthyDamageMultiplier(_state.Config.Gear.TwoHandSlashing)).Returns(1.1);
        _state.Player.Life = 900;

        var result = _calculator.Calculate(_state, _state.Config.Gear.TwoHandSlashing);

        result.Should().Be(1.1);
    }
}
