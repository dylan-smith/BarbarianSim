using BarbarianSim.Aspects;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Aspects;

public sealed class EdgemastersAspectTests : IDisposable
{
    public void Dispose() => BaseStatCalculator.ClearMocks();

    [Fact]
    public void GetDamageBonus_At_Max_Fury()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Fury = 100;
        BaseStatCalculator.InjectMock(typeof(MaxFuryCalculator), new FakeStatCalculator(100));

        var aspect = new EdgemastersAspect(15);

        var result = aspect.GetDamageBonus(state, SkillType.Basic);

        result.Should().Be(1.15);
    }

    [Fact]
    public void GetDamageBonus_At_Zero_Fury()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Fury = 0;
        BaseStatCalculator.InjectMock(typeof(MaxFuryCalculator), new FakeStatCalculator(100));

        var aspect = new EdgemastersAspect(15);

        var result = aspect.GetDamageBonus(state, SkillType.Core);

        result.Should().Be(1.0);
    }

    [Fact]
    public void GetDamageBonus_At_Partial_Fury()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Fury = 30;
        BaseStatCalculator.InjectMock(typeof(MaxFuryCalculator), new FakeStatCalculator(50));

        var aspect = new EdgemastersAspect(10);

        var result = aspect.GetDamageBonus(state, SkillType.Basic);

        result.Should().Be(1.06);
    }

    [Fact]
    public void GetDamageBonus_Returns_1_For_SkillType_None()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Fury = 30;
        BaseStatCalculator.InjectMock(typeof(MaxFuryCalculator), new FakeStatCalculator(50));

        var aspect = new EdgemastersAspect(10);

        var result = aspect.GetDamageBonus(state, SkillType.None);

        result.Should().Be(1.0);
    }
}
