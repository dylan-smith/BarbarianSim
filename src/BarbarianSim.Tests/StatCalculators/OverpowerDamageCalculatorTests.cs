﻿using BarbarianSim.Config;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public sealed class OverpowerDamageCalculatorTests : IDisposable
{
    public void Dispose() => BaseStatCalculator.ClearMocks();

    [Fact]
    public void Returns_1_By_Default()
    {
        var state = new SimulationState(new SimulationConfig());
        BaseStatCalculator.InjectMock(typeof(WillpowerCalculator), new FakeStatCalculator(0.0));

        var result = OverpowerDamageCalculator.Calculate(state);

        result.Should().Be(1.0);
    }

    [Fact]
    public void Includes_OverpowerDamage_Gear_Bonus()
    {
        var config = new SimulationConfig();
        config.Gear.Helm.OverpowerDamage = 42;
        var state = new SimulationState(config);
        BaseStatCalculator.InjectMock(typeof(WillpowerCalculator), new FakeStatCalculator(0.0));

        var result = OverpowerDamageCalculator.Calculate(state);

        result.Should().Be(1.42);
    }

    [Fact]
    public void Includes_Willpower_Bonus()
    {
        var state = new SimulationState(new SimulationConfig());
        BaseStatCalculator.InjectMock(typeof(WillpowerCalculator), new FakeStatCalculator(400.0));

        var result = OverpowerDamageCalculator.Calculate(state);

        result.Should().Be(2.0);
    }
}
