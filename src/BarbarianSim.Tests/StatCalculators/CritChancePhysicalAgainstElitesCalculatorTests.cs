using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class CritChancePhysicalAgainstElitesCalculatorTests
{
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly CritChancePhysicalAgainstElitesCalculator _calculator = new();

    [Fact]
    public void Includes_Stats_From_Gear()
    {
        _state.Config.Gear.Helm.CritChancePhysicalAgainstElites = 12.0;
        _state.Config.EnemySettings.IsElite = true;

        var result = _calculator.Calculate(_state, DamageType.Physical);

        result.Should().Be(0.12);
    }

    [Fact]
    public void Includes_Stats_From_Paragon()
    {
        _state.Config.Paragon.CritChancePhysicalAgainstElites = 12.0;
        _state.Config.EnemySettings.IsElite = true;

        var result = _calculator.Calculate(_state, DamageType.Physical);

        result.Should().Be(0.12);
    }

    [Fact]
    public void Returns_0_For_Non_Elites()
    {
        _state.Config.Gear.Helm.CritChancePhysicalAgainstElites = 12.0;
        _state.Config.EnemySettings.IsElite = false;

        var result = _calculator.Calculate(_state, DamageType.Physical);

        result.Should().Be(0.0);
    }

    [Fact]
    public void Returns_0_For_Non_Physical_Damage()
    {
        _state.Config.Gear.Helm.CritChancePhysicalAgainstElites = 12.0;
        _state.Config.EnemySettings.IsElite = true;

        var result = _calculator.Calculate(_state, DamageType.Fire);

        result.Should().Be(0.0);
    }
}
