using BarbarianSim.Arsenal;
using BarbarianSim.Config;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public sealed class LuckyHitChanceCalculatorTests
{
    private readonly Mock<PolearmExpertise> _mockPolearmExpertise = TestHelpers.CreateMock<PolearmExpertise>();
    private readonly Mock<SimLogger> _mockSimLogger = TestHelpers.CreateMock<SimLogger>();
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly LuckyHitChanceCalculator _calculator;

    public LuckyHitChanceCalculatorTests()
    {
        _mockPolearmExpertise.Setup(m => m.GetLuckyHitChanceMultiplier(It.IsAny<SimulationState>(), It.IsAny<GearItem>())).Returns(1.0);
        _calculator = new(_mockPolearmExpertise.Object, _mockSimLogger.Object);
    }

    [Fact]
    public void Includes_Stats_From_Gear()
    {
        _state.Config.Gear.Helm.LuckyHitChance = 12.0;

        var result = _calculator.Calculate(_state, null);

        result.Should().Be(0.12);
    }

    [Fact]
    public void Includes_Stats_From_Paragon()
    {
        _state.Config.Paragon.LuckyHitChance = 12.0;

        var result = _calculator.Calculate(_state, null);

        result.Should().Be(0.12);
    }

    [Fact]
    public void Includes_PolearmExpertise_Bonus()
    {
        _mockPolearmExpertise.Setup(m => m.GetLuckyHitChanceMultiplier(It.IsAny<SimulationState>(), It.IsAny<GearItem>())).Returns(1.1);
        _state.Config.Gear.Helm.LuckyHitChance = 12.0;

        var result = _calculator.Calculate(_state, null);

        result.Should().Be(0.12 * 1.1);
    }
}
