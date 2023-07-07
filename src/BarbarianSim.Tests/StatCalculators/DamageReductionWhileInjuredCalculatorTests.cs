using BarbarianSim.Config;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public sealed class DamageReductionWhileInjuredCalculatorTests : IDisposable
{
    public void Dispose() => BaseStatCalculator.ClearMocks();

    public DamageReductionWhileInjuredCalculatorTests()
    {
        BaseStatCalculator.InjectMock(typeof(MaxLifeCalculator), new FakeStatCalculator(1000.0));
    }

    [Fact]
    public void Returns_1_When_Not_Injured()
    {
        var config = new SimulationConfig();
        var state = new SimulationState(config);
        state.Player.Life = 800;

        var result = DamageReductionWhileInjuredCalculator.Calculate(state);

        result.Should().Be(1);
    }

    [Fact]
    public void Multiplies_Stats_From_Gear()
    {
        var config = new SimulationConfig();
        config.Gear.Helm.DamageReductionWhileInjured = 12.0;
        config.Gear.Chest.DamageReductionWhileInjured = 12.0;
        var state = new SimulationState(config);
        state.Player.Life = 300;

        var result = DamageReductionWhileInjuredCalculator.Calculate(state);

        result.Should().Be(0.7744);
    }
}
