using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Skills;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class CritDamageCalculatorTests
{
    private readonly Mock<HeavyHanded> _mockHeavyHanded = TestHelpers.CreateMock<HeavyHanded>();
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly CritDamageCalculator _calculator;

    public CritDamageCalculatorTests()
    {
        _mockHeavyHanded.Setup(m => m.GetCriticalStrikeDamage(It.IsAny<SimulationState>(), It.IsAny<Expertise>())).Returns(0.0);
        _calculator = new CritDamageCalculator(_mockHeavyHanded.Object);
    }

    [Fact]
    public void Includes_Stats_From_Gear()
    {
        _state.Config.Gear.Helm.CritDamage = 12.0;

        var result = _calculator.Calculate(_state, Expertise.NA);

        // 1.5 base + 0.12 from helm == 1.62
        result.Should().Be(1.62);
    }

    [Fact]
    public void Includes_Stats_From_Paragon()
    {
        _state.Config.Paragon.CritDamage = 12.0;

        var result = _calculator.Calculate(_state, Expertise.NA);

        // 1.5 base + 0.12 from helm == 1.62
        result.Should().Be(1.62);
    }

    [Fact]
    public void Base_Crit_Is_50()
    {
        var result = _calculator.Calculate(_state, Expertise.NA);

        result.Should().Be(1.50);
    }

    [Fact]
    public void Includes_HeavyHanded_Bonus()
    {
        _mockHeavyHanded.Setup(m => m.GetCriticalStrikeDamage(_state, Expertise.TwoHandedSword)).Returns(10);

        var result = _calculator.Calculate(_state, Expertise.TwoHandedSword);

        result.Should().Be(1.60);
    }
}
