using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Events;

public sealed class FurySpentEventTests : IDisposable
{
    public void Dispose() => BaseStatCalculator.ClearMocks();

    [Fact]
    public void Removes_Fury_From_Player()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Fury = 29.0;
        BaseStatCalculator.InjectMock(typeof(FuryCostReductionCalculator), new FakeStatCalculator(1.0, SkillType.Basic));
        var e = new FurySpentEvent(123.0, 12.0, SkillType.Basic);

        e.ProcessEvent(state);

        state.Player.Fury.Should().Be(17.0);
    }

    [Fact]
    public void Applies_FuryCostReduction_Multiplier()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Fury = 29.0;
        BaseStatCalculator.InjectMock(typeof(FuryCostReductionCalculator), new FakeStatCalculator(0.8, SkillType.Core));

        var e = new FurySpentEvent(123.0, 12.0, SkillType.Core);

        e.ProcessEvent(state);

        state.Player.Fury.Should().Be(19.4);
    }

    [Fact]
    public void Throws_Exception_If_Spending_More_Fury_Than_Player_Has()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Fury = 10.0;
        BaseStatCalculator.InjectMock(typeof(FuryCostReductionCalculator), new FakeStatCalculator(1.0, SkillType.Basic));
        var e = new FurySpentEvent(123.0, 12.0, SkillType.Basic);

        var act = () => e.ProcessEvent(state);

        act.Should().Throw<Exception>();
    }
}
