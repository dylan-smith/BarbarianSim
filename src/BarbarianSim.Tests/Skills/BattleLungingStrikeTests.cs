using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.Skills;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Skills;

public class BattleLungingStrikeTests
{
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly BattleLungingStrike _skill = new();

    [Fact]
    public void BattleLungingStrike_Applies_Bleed()
    {
        _state.Config.Skills.Add(Skill.BattleLungingStrike, 1);
        var lungingStrikeEvent = new LungingStrikeEvent(123, _state.Enemies.First())
        {
            BaseDamage = 1200,
        };

        _skill.ProcessEvent(lungingStrikeEvent, _state);

        _state.Events.Should().ContainSingle(e => e is BleedAppliedEvent);
        _state.Events.OfType<BleedAppliedEvent>().Single().Timestamp.Should().Be(123);
        _state.Events.OfType<BleedAppliedEvent>().Single().Duration.Should().Be(5);
        _state.Events.OfType<BleedAppliedEvent>().Single().Damage.Should().Be(240);
    }

    [Fact]
    public void BattleLungingStrike_Does_Nothing_If_Not_Skilled()
    {
        var lungingStrikeEvent = new LungingStrikeEvent(123, _state.Enemies.First())
        {
            BaseDamage = 1200,
        };

        _skill.ProcessEvent(lungingStrikeEvent, _state);

        _state.Events.Should().BeEmpty();
    }
}
