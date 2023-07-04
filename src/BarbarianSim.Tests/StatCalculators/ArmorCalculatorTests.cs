using BarbarianSim.Config;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public sealed class ArmorCalculatorTests : IDisposable
{
    public void Dispose() => BaseStatCalculator.ClearMocks();

    public ArmorCalculatorTests() => BaseStatCalculator.InjectMock(typeof(StrengthCalculator), new FakeStatCalculator(0.0));

    [Fact]
    public void Starts_At_Zero()
    {
        var config = new SimulationConfig();
        var state = new SimulationState(config);

        var result = ArmorCalculator.Calculate(state);

        result.Should().Be(0.0);
    }

    [Fact]
    public void Includes_Gear_Bonus()
    {
        var config = new SimulationConfig();
        config.Gear.Helm.Armor = 42;
        var state = new SimulationState(config);

        var result = ArmorCalculator.Calculate(state);

        result.Should().Be(42);
    }

    [Fact]
    public void Includes_Strength_Bonus()
    {
        var config = new SimulationConfig();
        var state = new SimulationState(config);

        BaseStatCalculator.InjectMock(typeof(StrengthCalculator), new FakeStatCalculator(42.0));

        var result = ArmorCalculator.Calculate(state);

        result.Should().Be(42);
    }
}
