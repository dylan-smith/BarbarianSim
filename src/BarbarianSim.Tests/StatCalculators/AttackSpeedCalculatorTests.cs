using BarbarianSim.Config;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class AttackSpeedCalculatorTests
{
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly AttackSpeedCalculator _calculator = new();

    [Fact]
    public void Includes_Stats_From_Gear()
    {
        _state.Config.Gear.Helm.AttackSpeed = 60.0;

        var result = _calculator.Calculate(_state);

        result.Should().Be(0.625); // 1 / 1.60 == 0.625
    }
}
