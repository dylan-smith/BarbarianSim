using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.Skills;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Skills;

public class HamstringTests
{
    [Fact]
    public void Applies_Slow_Aura()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.Hamstring, 1);
        var bleedEvent = new BleedAppliedEvent(123, 99, 12, state.Enemies.First())
        {
            BleedCompletedEvent = new BleedCompletedEvent(135, 99, state.Enemies.First())
        };

        Hamstring.ProcessEvent(bleedEvent, state);

        state.Enemies.First().Auras.Should().Contain(Aura.Slow);
    }

    [Fact]
    public void Creates_AuraExpiredEvent()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.Hamstring, 1);
        var bleedEvent = new BleedAppliedEvent(123, 99, 12, state.Enemies.First())
        {
            BleedCompletedEvent = new BleedCompletedEvent(135, 99, state.Enemies.First())
        };

        Hamstring.ProcessEvent(bleedEvent, state);

        state.Events.Should().ContainSingle(e => e is AuraExpiredEvent);
        state.Events.OfType<AuraExpiredEvent>().First().Aura.Should().Be(Aura.Slow);
        state.Events.OfType<AuraExpiredEvent>().First().Timestamp.Should().Be(135);
    }
}
