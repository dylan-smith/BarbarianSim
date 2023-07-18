using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.Skills;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Skills;

public class EnhancedWarCryTests
{
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly EnhancedWarCry _skill = new();

    [Fact]
    public void Creates_BerserkingAuraAppliedEvent()
    {
        _state.Config.Skills.Add(Skill.EnhancedWarCry, 1);
        var warCryEvent = new WarCryEvent(123);

        _skill.ProcessEvent(warCryEvent, _state);

        _state.Events.Should().ContainSingle(e => e is AuraAppliedEvent && ((AuraAppliedEvent)e).Aura == Aura.Berserking);
        _state.Events.OfType<AuraAppliedEvent>().Single().Timestamp.Should().Be(123);
        _state.Events.OfType<AuraAppliedEvent>().Single().Duration.Should().Be(4.0);
        _state.Events.OfType<AuraAppliedEvent>().Single().Aura.Should().Be(Aura.Berserking);
    }

    [Fact]
    public void Does_Nothing_If_Not_Skilled()
    {
        _state.Config.Skills.Add(Skill.EnhancedWarCry, 0);
        var warCryEvent = new WarCryEvent(123);

        _skill.ProcessEvent(warCryEvent, _state);

        _state.Events.Should().NotContain(e => e is AuraAppliedEvent);
    }
}
