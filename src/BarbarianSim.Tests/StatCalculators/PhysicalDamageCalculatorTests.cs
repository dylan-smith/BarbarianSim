using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class PhysicalDamageCalculatorTests
{
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly PhysicalDamageCalculator _calculator = new();

    [Fact]
    public void Includes_Stats_From_Gear()
    {
        _state.Config.Gear.Helm.PhysicalDamage = 12.0;

        var result = _calculator.Calculate(_state, DamageType.Physical);

        result.Should().Be(12.0);
    }

    [Fact]
    public void Includes_Stats_From_Paragon()
    {
        _state.Config.Paragon.PhysicalDamage = 12.0;

        var result = _calculator.Calculate(_state, DamageType.Physical);

        result.Should().Be(12.0);
    }

    [Fact]
    public void Returns_0_For_Non_Physical_Damage_Type()
    {
        _state.Config.Gear.Helm.PhysicalDamage = 12.0;

        var result = _calculator.Calculate(_state, DamageType.Direct);

        result.Should().Be(0.0);
    }
}
