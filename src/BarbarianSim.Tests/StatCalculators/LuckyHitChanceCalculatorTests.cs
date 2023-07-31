using BarbarianSim.Config;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public sealed class LuckyHitChanceCalculatorTests
{
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly LuckyHitChanceCalculator _calculator = new();

    [Fact]
    public void Includes_Stats_From_Gear()
    {
        _state.Config.Gear.Helm.LuckyHitChance = 12.0;

        var result = _calculator.Calculate(_state);

        result.Should().Be(0.12);
    }

    [Fact]
    public void Includes_Stats_From_Paragon()
    {
        _state.Config.Paragon.LuckyHitChance = 12.0;

        var result = _calculator.Calculate(_state);

        result.Should().Be(0.12);
    }
}
