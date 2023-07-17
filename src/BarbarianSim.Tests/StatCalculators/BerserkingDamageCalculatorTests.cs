using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class BerserkingDamageCalculatorTests
{
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly BerserkingDamageCalculator _calculator = new();

    [Fact]
    public void When_Berserking_Returns_25()
    {
        _state.Player.Auras.Add(Aura.Berserking);

        var result = _calculator.Calculate(_state);

        result.Should().Be(25.0);
    }

    [Fact]
    public void When_Not_Berserking_Returns_0()
    {
        var result = _calculator.Calculate(_state);

        result.Should().Be(0.0);
    }
}
