using BarbarianSim.Aspects;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class CritChanceCalculatorTests
{
    private readonly Mock<CritChancePhysicalAgainstElitesCalculator> _mockcritChancePhysicalAgainstElitesCalculator = TestHelpers.CreateMock<CritChancePhysicalAgainstElitesCalculator>();
    private readonly Mock<DexterityCalculator> _mockDexterityCalculator = TestHelpers.CreateMock<DexterityCalculator>();
    private readonly Mock<AspectOfTheDireWhirlwind> _mockAspectOfTheDireWhirlwind = TestHelpers.CreateMock<AspectOfTheDireWhirlwind>();
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly CritChanceCalculator _calculator;

    public CritChanceCalculatorTests()
    {
        _mockcritChancePhysicalAgainstElitesCalculator.Setup(x => x.Calculate(It.IsAny<SimulationState>(), It.IsAny<DamageType>())).Returns(0.0);
        _mockDexterityCalculator.Setup(x => x.Calculate(It.IsAny<SimulationState>())).Returns(0.0);
        _mockAspectOfTheDireWhirlwind.Setup(m => m.GetCritChanceBonus(It.IsAny<SimulationState>())).Returns(0);

        _calculator = new CritChanceCalculator(_mockcritChancePhysicalAgainstElitesCalculator.Object, _mockDexterityCalculator.Object, _mockAspectOfTheDireWhirlwind.Object);
    }

    [Fact]
    public void Includes_Base_5_Percent_Chance()
    {
        var result = _calculator.Calculate(_state, DamageType.Physical);

        result.Should().Be(0.05);
    }

    [Fact]
    public void Includes_Stats_From_Gear()
    {
        _state.Config.Gear.Helm.CritChance = 12.0;

        var result = _calculator.Calculate(_state, DamageType.Physical);

        result.Should().Be(0.17);
    }

    [Fact]
    public void Includes_Crit_Chance_Physical_Against_Elites()
    {
        _mockcritChancePhysicalAgainstElitesCalculator.Setup(x => x.Calculate(It.IsAny<SimulationState>(), It.IsAny<DamageType>())).Returns(12.0);

        var result = _calculator.Calculate(_state, DamageType.Physical);

        result.Should().Be(0.17);
    }

    [Fact]
    public void Includes_Dexterity_Bonus()
    {
        _mockDexterityCalculator.Setup(x => x.Calculate(_state)).Returns(400.0);

        var result = _calculator.Calculate(_state, DamageType.Physical);

        result.Should().Be(0.13);
    }

    [Fact]
    public void Includes_AspectOfTheDireWhirlwind_Bonus()
    {
        _mockAspectOfTheDireWhirlwind.Setup(m => m.GetCritChanceBonus(_state)).Returns(20);

        var result = _calculator.Calculate(_state, DamageType.Physical);

        result.Should().Be(0.25);
    }
}
