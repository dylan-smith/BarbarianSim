using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class MovementSpeedCalculatorTests
{
    [Fact]
    public void Includes_Bonus_From_Gear()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Gear.Helm.MovementSpeed = 12.0;

        var result = MovementSpeedCalculator.Calculate(state);

        result.Should().Be(12.0);
    }

    [Fact]
    public void Bonus_From_Berserking()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Gear.Helm.MovementSpeed = 12.0;
        state.Player.Auras.Add(Aura.Berserking);

        var result = MovementSpeedCalculator.Calculate(state);

        result.Should().Be(27.0);
    }

    [Fact]
    public void Bonus_From_RallyingCry()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Gear.Helm.MovementSpeed = 12.0;
        state.Player.Auras.Add(Aura.RallyingCry);

        var result = MovementSpeedCalculator.Calculate(state);

        result.Should().Be(42);
    }
}
