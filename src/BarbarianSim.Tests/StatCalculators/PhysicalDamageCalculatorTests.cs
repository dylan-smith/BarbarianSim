using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class PhysicalDamageCalculatorTests
{
    [Fact]
    public void Includes_Stats_From_Gear()
    {
        var config = new SimulationConfig();
        config.Gear.Helm.PhysicalDamage = 12.0;
        var state = new SimulationState(config);

        var result = PhysicalDamageCalculator.Calculate(state, DamageType.Physical);

        result.Should().Be(12.0);
    }

    [Fact]
    public void Returns_0_For_Non_Physical_Damage_Type()
    {
        var config = new SimulationConfig();
        config.Gear.Helm.PhysicalDamage = 12.0;
        var state = new SimulationState(config);

        var result = PhysicalDamageCalculator.Calculate(state, DamageType.Direct);

        result.Should().Be(0.0);
    }
}
