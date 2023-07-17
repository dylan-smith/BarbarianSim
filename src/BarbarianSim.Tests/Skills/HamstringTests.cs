using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.Skills;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Skills;

public class HamstringTests
{
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly Hamstring _skill = new();

    [Fact]
    public void Applies_Slow_Aura()
    {
        _state.Config.Skills.Add(Skill.Hamstring, 1);
        var bleedEvent = new BleedAppliedEvent(123, 99, 12, _state.Enemies.First())
        {
            BleedCompletedEvent = new BleedCompletedEvent(135, 99, _state.Enemies.First())
        };

        _skill.ProcessEvent(bleedEvent, _state);

        _state.Enemies.First().Auras.Should().Contain(Aura.Slow);
    }

    [Fact]
    public void Creates_AuraExpiredEvent()
    {
        _state.Config.Skills.Add(Skill.Hamstring, 1);
        var bleedEvent = new BleedAppliedEvent(123, 99, 12, _state.Enemies.First())
        {
            BleedCompletedEvent = new BleedCompletedEvent(135, 99, _state.Enemies.First())
        };

        _skill.ProcessEvent(bleedEvent, _state);

        _state.Events.Should().ContainSingle(e => e is AuraExpiredEvent);
        _state.Events.OfType<AuraExpiredEvent>().First().Aura.Should().Be(Aura.Slow);
        _state.Events.OfType<AuraExpiredEvent>().First().Timestamp.Should().Be(135);
    }
}
