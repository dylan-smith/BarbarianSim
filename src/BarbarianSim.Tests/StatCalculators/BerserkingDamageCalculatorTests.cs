using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class BerserkingDamageCalculatorTests
{
    [Fact]
    public void When_Berserking_Returns_25()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Auras.Add(Aura.Berserking);

        var result = BerserkingDamageCalculator.Calculate(state);

        result.Should().Be(25.0);
    }

    [Fact]
    public void When_Not_Berserking_Returns_0()
    {
        var state = new SimulationState(new SimulationConfig());

        var result = BerserkingDamageCalculator.Calculate(state);

        result.Should().Be(0.0);
    }
}
