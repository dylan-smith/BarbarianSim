using BarbarianSim.Arsenal;
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
    private readonly Mock<TwoHandedMaceExpertise> _mockTwoHandedMaceExpertise = TestHelpers.CreateMock<TwoHandedMaceExpertise>();
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly CritDamageCalculator _calculator;

    public CritDamageCalculatorTests()
    {
        _mockHeavyHanded.Setup(m => m.GetCriticalStrikeDamage(It.IsAny<SimulationState>(), It.IsAny<Expertise>())).Returns(0.0);
        _mockTwoHandedMaceExpertise.Setup(m => m.GetCriticalStrikeDamageMultiplier(It.IsAny<SimulationState>(), It.IsAny<GearItem>(), It.IsAny<EnemyState>())).Returns(1.0);
        _calculator = new CritDamageCalculator(_mockHeavyHanded.Object, _mockTwoHandedMaceExpertise.Object);
    }

    [Fact]
    public void Includes_Stats_From_Gear()
    {
        _state.Config.Gear.Helm.CritDamage = 12.0;

        var result = _calculator.Calculate(_state, Expertise.NA, null, _state.Enemies.First());

        // 1.5 base + 0.12 from helm == 1.62
        result.Should().Be(1.62);
    }

    [Fact]
    public void Includes_Stats_From_Paragon()
    {
        _state.Config.Paragon.CritDamage = 12.0;

        var result = _calculator.Calculate(_state, Expertise.NA, null, _state.Enemies.First());

        // 1.5 base + 0.12 from helm == 1.62
        result.Should().Be(1.62);
    }

    [Fact]
    public void Base_Crit_Is_50()
    {
        var result = _calculator.Calculate(_state, Expertise.NA, null, _state.Enemies.First());

        result.Should().Be(1.50);
    }

    [Fact]
    public void Includes_HeavyHanded_Bonus()
    {
        _mockHeavyHanded.Setup(m => m.GetCriticalStrikeDamage(_state, Expertise.TwoHandedSword)).Returns(10);

        var result = _calculator.Calculate(_state, Expertise.TwoHandedSword, null, _state.Enemies.First());

        result.Should().Be(1.60);
    }

    [Fact]
    public void Includes_TwoHandedMaceExpertise_Bonus()
    {
        _mockTwoHandedMaceExpertise.Setup(m => m.GetCriticalStrikeDamageMultiplier(_state, _state.Config.Gear.TwoHandBludgeoning, _state.Enemies.First())).Returns(1.15);

        var result = _calculator.Calculate(_state, Expertise.TwoHandedSword, _state.Config.Gear.TwoHandBludgeoning, _state.Enemies.First());

        result.Should().Be(1.15 * 1.5);
    }
}
