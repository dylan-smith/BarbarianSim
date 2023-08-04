using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.Skills;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Skills;

public class TacticalIronSkinTests
{
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly TacticalIronSkin _skill = new();

    [Fact]
    public void TacticalIronSkin_Creates_5_HealingEvents()
    {
        _state.Config.Skills.Add(Skill.TacticalIronSkin, 1);
        var ironSkinEvent = new IronSkinEvent(123)
        {
            BarrierAppliedEvent = new BarrierAppliedEvent(123, null, 1200, 5.0)
        };

        _skill.ProcessEvent(ironSkinEvent, _state);

        _state.Events.Count(x => x is HealingEvent).Should().Be(5);
        _state.Events.OfType<HealingEvent>().ToList()[0].Timestamp.Should().Be(124);
        _state.Events.OfType<HealingEvent>().ToList()[1].Timestamp.Should().Be(125);
        _state.Events.OfType<HealingEvent>().ToList()[2].Timestamp.Should().Be(126);
        _state.Events.OfType<HealingEvent>().ToList()[3].Timestamp.Should().Be(127);
        _state.Events.OfType<HealingEvent>().ToList()[4].Timestamp.Should().Be(128);
        _state.Events.OfType<HealingEvent>().ToList()[0].BaseAmountHealed.Should().Be(120);
        _state.Events.OfType<HealingEvent>().ToList()[1].BaseAmountHealed.Should().Be(120);
        _state.Events.OfType<HealingEvent>().ToList()[2].BaseAmountHealed.Should().Be(120);
        _state.Events.OfType<HealingEvent>().ToList()[3].BaseAmountHealed.Should().Be(120);
        _state.Events.OfType<HealingEvent>().ToList()[4].BaseAmountHealed.Should().Be(120);
    }
}
