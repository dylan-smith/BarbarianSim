using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.Skills;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Skills;

public class ViolentWhirlwindTests
{
    [Fact]
    public void Creates_ViolentWhirlwindAppliedEvent()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.ViolentWhirlwind, 1);
        var whirlwindStartedEvent = new WhirlwindSpinEvent(123.0);

        ViolentWhirlwind.ProcessEvent(whirlwindStartedEvent, state);

        state.Events.Should().ContainSingle(e => e is AuraAppliedEvent && ((AuraAppliedEvent)e).Aura == Aura.ViolentWhirlwind);
        state.Events.OfType<AuraAppliedEvent>().Single().Timestamp.Should().Be(125);
        state.Events.OfType<AuraAppliedEvent>().Single().Duration.Should().Be(0);
        state.Events.OfType<AuraAppliedEvent>().Single().Aura.Should().Be(Aura.ViolentWhirlwind);
    }

    [Fact]
    public void Does_Nothing_If_Not_Skilled()
    {
        var state = new SimulationState(new SimulationConfig());
        var whirlwindStartedEvent = new WhirlwindSpinEvent(123.0);

        ViolentWhirlwind.ProcessEvent(whirlwindStartedEvent, state);

        state.Events.Should().BeEmpty();
    }
}
