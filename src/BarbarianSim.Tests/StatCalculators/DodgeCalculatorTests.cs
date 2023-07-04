using BarbarianSim.Config;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public sealed class DodgeCalculatorTests : IDisposable
{
    public void Dispose() => BaseStatCalculator.ClearMocks();

    [Fact]
    public void Returns_0_By_Default()
    {
        var state = new SimulationState(new SimulationConfig());
        BaseStatCalculator.InjectMock(typeof(DexterityCalculator), new FakeStatCalculator(0.0));

        var result = DodgeCalculator.Calculate(state);

        result.Should().Be(0.0);
    }

    [Fact]
    public void Includes_Dodge_Gear_Bonus()
    {
        var config = new SimulationConfig();
        config.Gear.Helm.Dodge = 42;
        var state = new SimulationState(config);
        BaseStatCalculator.InjectMock(typeof(DexterityCalculator), new FakeStatCalculator(0.0));

        var result = DodgeCalculator.Calculate(state);

        result.Should().Be(0.42);
    }

    [Fact]
    public void Includes_Dexterity_Bonus()
    {
        var state = new SimulationState(new SimulationConfig());
        BaseStatCalculator.InjectMock(typeof(DexterityCalculator), new FakeStatCalculator(400.0));

        var result = DodgeCalculator.Calculate(state);

        result.Should().Be(0.04);
    }
}
