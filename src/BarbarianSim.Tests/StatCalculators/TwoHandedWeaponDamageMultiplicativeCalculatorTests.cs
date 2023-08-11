using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class TwoHandedWeaponDamageMultiplicativeCalculatorTests
{
    private readonly Mock<SimLogger> _mockSimLogger = TestHelpers.CreateMock<SimLogger>();
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly TwoHandedWeaponDamageMultiplicativeCalculator _calculator;

    public TwoHandedWeaponDamageMultiplicativeCalculatorTests() => _calculator = new TwoHandedWeaponDamageMultiplicativeCalculator(_mockSimLogger.Object);

    [Fact]
    public void Includes_Stats_From_Gear_TwoHandBludgeoning()
    {
        _state.Config.Gear.TwoHandBludgeoning.Expertise = Expertise.TwoHandedMace;
        _state.Config.Gear.Helm.TwoHandWeaponDamageMultiplicative = 3;
        _state.Config.Gear.Chest.TwoHandWeaponDamageMultiplicative = 5;

        var result = _calculator.Calculate(_state, _state.Config.Gear.TwoHandBludgeoning);

        result.Should().Be(1.08);
    }

    [Fact]
    public void Includes_Stats_From_Gear_TwoHandSword()
    {
        _state.Config.Gear.TwoHandSlashing.Expertise = Expertise.TwoHandedSword;
        _state.Config.Gear.Helm.TwoHandWeaponDamageMultiplicative = 3;
        _state.Config.Gear.Chest.TwoHandWeaponDamageMultiplicative = 5;

        var result = _calculator.Calculate(_state, _state.Config.Gear.TwoHandSlashing);

        result.Should().Be(1.08);
    }

    [Fact]
    public void Includes_Stats_From_Gear_TwoHandAxe()
    {
        _state.Config.Gear.TwoHandSlashing.Expertise = Expertise.TwoHandedAxe;
        _state.Config.Gear.Helm.TwoHandWeaponDamageMultiplicative = 3;
        _state.Config.Gear.Chest.TwoHandWeaponDamageMultiplicative = 5;

        var result = _calculator.Calculate(_state, _state.Config.Gear.TwoHandSlashing);

        result.Should().Be(1.08);
    }

    [Fact]
    public void Includes_Stats_From_Paragon()
    {
        _state.Config.Gear.TwoHandBludgeoning.Expertise = Expertise.TwoHandedMace;
        _state.Config.Paragon.TwoHandWeaponDamageMultiplicative = 3;
        _state.Config.Gear.Chest.TwoHandWeaponDamageMultiplicative = 5;

        var result = _calculator.Calculate(_state, _state.Config.Gear.TwoHandBludgeoning);

        result.Should().Be(1.08);
    }

    [Fact]
    public void Returns_1_If_Not_TwoHandedWeapon()
    {
        _state.Config.Gear.OneHandLeft.Expertise = Expertise.OneHandedSword;
        _state.Config.Gear.Helm.TwoHandWeaponDamageMultiplicative = 3;
        _state.Config.Gear.Chest.TwoHandWeaponDamageMultiplicative = 5;

        var result = _calculator.Calculate(_state, _state.Config.Gear.OneHandLeft);

        result.Should().Be(1.0);
    }

    [Fact]
    public void Returns_1_When_No_Bonuses()
    {
        var result = _calculator.Calculate(_state, _state.Config.Gear.TwoHandSlashing);

        result.Should().Be(1.0);
    }
}
