using BarbarianSim.Config;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Events;

public sealed class HealingEventTests : IDisposable
{
    public void Dispose() => BaseStatCalculator.ClearMocks();

    public HealingEventTests() => BaseStatCalculator.InjectMock(typeof(HealingReceivedCalculator), new FakeStatCalculator(1.0));

    [Fact]
    public void Increases_Player_Life()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Life = 500.0;
        state.Player.MaxLife = 1000.0;
        var e = new HealingEvent(123.0, 270.0);

        e.ProcessEvent(state);

        state.Player.Life.Should().Be(770.0);
        e.BaseAmountHealed.Should().Be(270.0);
        e.AmountHealed.Should().Be(270.0);
        e.OverHeal.Should().Be(0.0);
    }

    [Fact]
    public void Applies_HealingReceived_Multiplier()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Life = 500.0;
        state.Player.MaxLife = 1000.0;
        BaseStatCalculator.InjectMock(typeof(HealingReceivedCalculator), new FakeStatCalculator(1.5));
        var e = new HealingEvent(123.0, 270.0);

        e.ProcessEvent(state);

        state.Player.Life.Should().Be(905.0);
    }

    [Fact]
    public void Calculates_Overheal()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Life = 500.0;
        state.Player.MaxLife = 1000.0;
        var e = new HealingEvent(123.0, 810.0);

        e.ProcessEvent(state);

        state.Player.Life.Should().Be(1000.0);
        e.BaseAmountHealed.Should().Be(810.0);
        e.AmountHealed.Should().Be(500.0);
        e.OverHeal.Should().Be(310.0);
    }
}
