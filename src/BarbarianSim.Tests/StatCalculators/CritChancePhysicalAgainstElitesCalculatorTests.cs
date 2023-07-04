using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public sealed class CritChancePhysicalAgainstElitesCalculatorTests : IDisposable
{
    public void Dispose() => BaseStatCalculator.ClearMocks();

    [Fact]
    public void Includes_Stats_From_Gear()
    {
        var config = new SimulationConfig();
        config.Gear.Helm.CritChancePhysicalAgainstElites = 12.0;
        config.EnemySettings.IsElite = true;
        var state = new SimulationState(config);

        var result = CritChancePhysicalAgainstElitesCalculator.Calculate(state, DamageType.Physical);

        result.Should().Be(0.12);
    }

    [Fact]
    public void Returns_0_For_Non_Elites()
    {
        var config = new SimulationConfig();
        config.Gear.Helm.CritChancePhysicalAgainstElites = 12.0;
        config.EnemySettings.IsElite = false;
        var state = new SimulationState(config);

        var result = CritChancePhysicalAgainstElitesCalculator.Calculate(state, DamageType.Physical);

        result.Should().Be(0.0);
    }

    [Fact]
    public void Returns_0_For_Non_Physical_Damage()
    {
        var config = new SimulationConfig();
        config.Gear.Helm.CritChancePhysicalAgainstElites = 12.0;
        config.EnemySettings.IsElite = false;
        var state = new SimulationState(config);

        var result = CritChancePhysicalAgainstElitesCalculator.Calculate(state, DamageType.Direct);

        result.Should().Be(0.0);
    }
}
