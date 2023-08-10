using BarbarianSim.Arsenal;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class CritChanceVulnerableCalculatorTests
{
    private readonly Mock<TwoHandedAxeExpertise> _mockTwoHandedAxeExpertise = TestHelpers.CreateMock<TwoHandedAxeExpertise>();
    private readonly Mock<SimLogger> _mockSimLogger = TestHelpers.CreateMock<SimLogger>();
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly CritChanceVulnerableCalculator _calculator;

    public CritChanceVulnerableCalculatorTests()
    {
        _state.Enemies.First().Auras.Add(Aura.Vulnerable);
        _mockTwoHandedAxeExpertise.Setup(m => m.GetCritChanceVulnerable(It.IsAny<SimulationState>(), It.IsAny<GearItem>())).Returns(1.0);
        _calculator = new(_mockTwoHandedAxeExpertise.Object, _mockSimLogger.Object);
    }

    [Fact]
    public void Includes_TwoHandedAxeExpertise_Bonus()
    {
        _mockTwoHandedAxeExpertise.Setup(m => m.GetCritChanceVulnerable(_state, _state.Config.Gear.TwoHandSlashing)).Returns(10);
        var result = _calculator.Calculate(_state, _state.Enemies.First(), _state.Config.Gear.TwoHandSlashing);

        result.Should().Be(10);
    }

    [Fact]
    public void Returns_0_When_Not_Vulnerable()
    {
        _state.Enemies.First().Auras.Clear();
        _mockTwoHandedAxeExpertise.Setup(m => m.GetCritChanceVulnerable(_state, _state.Config.Gear.TwoHandSlashing)).Returns(10);
        var result = _calculator.Calculate(_state, _state.Enemies.First(), _state.Config.Gear.TwoHandSlashing);

        result.Should().Be(0);
    }
}
