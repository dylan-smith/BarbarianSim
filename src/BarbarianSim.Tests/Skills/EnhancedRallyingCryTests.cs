using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.Skills;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Skills;

public class EnhancedRallyingCryTests
{
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly EnhancedRallyingCry _skill = new();

    [Fact]
    public void Creates_UnstoppableAuraAppliedEvent()
    {
        _state.Config.Skills.Add(Skill.EnhancedRallyingCry, 1);
        var rallyingCryEvent = new RallyingCryEvent(123)
        {
            Duration = 6.0
        };

        _skill.ProcessEvent(rallyingCryEvent, _state);

        _state.Events.Should().ContainSingle(e => e is AuraAppliedEvent && ((AuraAppliedEvent)e).Aura == Aura.Unstoppable);
        _state.Events.OfType<AuraAppliedEvent>().Single().Timestamp.Should().Be(123);
        _state.Events.OfType<AuraAppliedEvent>().Single().Aura.Should().Be(Aura.Unstoppable);
        _state.Events.OfType<AuraAppliedEvent>().Single().Duration.Should().Be(6);
    }

    [Fact]
    public void Does_Nothing_If_Not_Skilled()
    {
        _state.Config.Skills.Add(Skill.EnhancedRallyingCry, 0);
        var rallyingCryEvent = new RallyingCryEvent(123)
        {
            Duration = 6.0
        };

        _skill.ProcessEvent(rallyingCryEvent, _state);

        _state.Events.Should().NotContain(e => e is AuraAppliedEvent);
    }
}
