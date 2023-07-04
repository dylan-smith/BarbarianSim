using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public sealed class CritChanceCalculatorTests : IDisposable
{
    public void Dispose() => BaseStatCalculator.ClearMocks();

    [Fact]
    public void Includes_Base_5_Percent_Chance()
    {
        var state = new SimulationState(new SimulationConfig());
        BaseStatCalculator.InjectMock(typeof(DexterityCalculator), new FakeStatCalculator(0.0));

        var result = CritChanceCalculator.Calculate(state, DamageType.Physical);

        result.Should().Be(0.05);
    }

    [Fact]
    public void Includes_Stats_From_Gear()
    {
        var config = new SimulationConfig();
        config.Gear.Helm.CritChance = 12.0;
        var state = new SimulationState(config);
        BaseStatCalculator.InjectMock(typeof(DexterityCalculator), new FakeStatCalculator(0.0));

        var result = CritChanceCalculator.Calculate(state, DamageType.Physical);

        result.Should().Be(0.17);
    }

    [Fact]
    public void Includes_Crit_Chance_Physical_Against_Elites()
    {
        var state = new SimulationState(new SimulationConfig());

        BaseStatCalculator.InjectMock(typeof(DexterityCalculator), new FakeStatCalculator(0.0));
        BaseStatCalculator.InjectMock(typeof(CritChancePhysicalAgainstElitesCalculator), new FakeStatCalculator(12.0));

        var result = CritChanceCalculator.Calculate(state, DamageType.Physical);

        result.Should().Be(0.17);
    }

    [Fact]
    public void Includes_Dexterity_Bonus()
    {
        var state = new SimulationState(new SimulationConfig());
        BaseStatCalculator.InjectMock(typeof(DexterityCalculator), new FakeStatCalculator(400.0));

        var result = CritChanceCalculator.Calculate(state, DamageType.Physical);

        result.Should().Be(0.13);
    }
}
