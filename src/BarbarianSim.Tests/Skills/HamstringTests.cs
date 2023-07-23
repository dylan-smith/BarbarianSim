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
    public void Creates_AuraAppliedEvent()
    {
        _state.Config.Skills.Add(Skill.Hamstring, 1);
        var bleedEvent = new BleedAppliedEvent(123, 99, 12, _state.Enemies.First());

        _skill.ProcessEvent(bleedEvent, _state);

        _state.Events.Should().ContainSingle(e => e is AuraAppliedEvent);
        _state.Events.OfType<AuraAppliedEvent>().First().Aura.Should().Be(Aura.Slow);
        _state.Events.OfType<AuraAppliedEvent>().First().Timestamp.Should().Be(123);
        _state.Events.OfType<AuraAppliedEvent>().First().Duration.Should().Be(12);
    }

    [Fact]
    public void Applies_It_To_The_Right_Enemy()
    {
        var config = new SimulationConfig();
        config.EnemySettings.NumberOfEnemies = 3;
        var state = new SimulationState(config);
        state.Config.Skills.Add(Skill.Hamstring, 1);
        var bleedEvent = new BleedAppliedEvent(123, 99, 12, state.Enemies.Last());

        _skill.ProcessEvent(bleedEvent, state);

        state.Events.Should().ContainSingle(e => e is AuraAppliedEvent);
        state.Events.OfType<AuraAppliedEvent>().First().Aura.Should().Be(Aura.Slow);
        state.Events.OfType<AuraAppliedEvent>().First().Timestamp.Should().Be(123);
        state.Events.OfType<AuraAppliedEvent>().First().Duration.Should().Be(12);
        state.Events.OfType<AuraAppliedEvent>().First().Target.Should().Be(state.Enemies.Last());
    }
}
