using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.Skills;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Skills;

public class BattleLungingStrikeTests
{
    [Fact]
    public void BattleLungingStrike_Applies_Bleed()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.BattleLungingStrike, 1);
        var lungingStrikeEvent = new LungingStrikeEvent(123, state.Enemies.First())
        {
            DamageEvent = new DamageEvent(123, 1200, DamageType.Direct, DamageSource.LungingStrike, SkillType.Basic, state.Enemies.First())
        };

        BattleLungingStrike.ProcessEvent(lungingStrikeEvent, state);

        state.Events.Should().ContainSingle(e => e is BleedAppliedEvent);
        state.Events.OfType<BleedAppliedEvent>().Single().Timestamp.Should().Be(123);
        state.Events.OfType<BleedAppliedEvent>().Single().Duration.Should().Be(5);
        state.Events.OfType<BleedAppliedEvent>().Single().Damage.Should().Be(240);
    }

    [Fact]
    public void BattleLungingStrike_Does_Nothing_If_Not_Skilled()
    {
        var state = new SimulationState(new SimulationConfig());
        var lungingStrikeEvent = new LungingStrikeEvent(123, state.Enemies.First())
        {
            DamageEvent = new DamageEvent(123, 1200, DamageType.Direct, DamageSource.LungingStrike, SkillType.Basic, state.Enemies.First())
        };

        BattleLungingStrike.ProcessEvent(lungingStrikeEvent, state);

        state.Events.Should().BeEmpty();
    }
}
