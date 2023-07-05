using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Events;

public sealed class GenerateFuryEventTests : IDisposable
{
    public void Dispose() => BaseStatCalculator.ClearMocks();

    [Fact]
    public void Adds_Fury_To_Player()
    {
        var state = new SimulationState(new SimulationConfig());
        BaseStatCalculator.InjectMock(typeof(ResourceGenerationCalculator), new FakeStatCalculator(1.0));
        var e = new GenerateFuryEvent(123.0, 12.0);

        e.ProcessEvent(state);

        state.Player.Fury.Should().Be(12.0);
    }

    [Fact]
    public void Applies_Resource_Generation_Multiplier()
    {
        var state = new SimulationState(new SimulationConfig());
        BaseStatCalculator.InjectMock(typeof(ResourceGenerationCalculator), new FakeStatCalculator(1.4));

        var e = new GenerateFuryEvent(123.0, 12.0);

        e.ProcessEvent(state);

        state.Player.Fury.Should().BeApproximately(16.8, 0.00001);
    }

    [Fact]
    public void Caps_Fury_At_Max()
    {
        var state = new SimulationState(new SimulationConfig());
        BaseStatCalculator.InjectMock(typeof(ResourceGenerationCalculator), new FakeStatCalculator(1.0));
        state.Player.Fury = 95;
        var e = new GenerateFuryEvent(123.0, 12.0);

        e.ProcessEvent(state);

        state.Player.Fury.Should().Be(100.0);
    }
}
