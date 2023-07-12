using BarbarianSim.Abilities;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.Skills;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Skills;

public class FuriousWhirlwindTests
{
    [Fact]
    public void Creates_BleedAppliedEvent()
    {
        var state = new SimulationState(new SimulationConfig());
        var whirlwindSpinEvent = new WhirlwindSpinEvent(123)
        {
            BaseDamage = 3500,
        };
        Whirlwind.Weapon = state.Config.Gear.TwoHandSlashing;
        state.Config.Skills.Add(Skill.FuriousWhirlwind, 1);

        FuriousWhirlwind.ProcessEvent(whirlwindSpinEvent, state);

        state.Events.Should().ContainSingle(e => e is BleedAppliedEvent);
        state.Events.OfType<BleedAppliedEvent>().Single().Timestamp.Should().Be(123);
        state.Events.OfType<BleedAppliedEvent>().Single().Damage.Should().Be(3500 * 0.4);
        state.Events.OfType<BleedAppliedEvent>().Single().Duration.Should().Be(5);
        state.Events.OfType<BleedAppliedEvent>().Single().Target.Should().Be(state.Enemies.First());
    }

    [Fact]
    public void Applies_To_All_Enemies()
    {
        var config = new SimulationConfig();
        config.EnemySettings.NumberOfEnemies = 3;
        var state = new SimulationState(config);
        var whirlwindSpinEvent = new WhirlwindSpinEvent(123)
        {
            BaseDamage = 3500,
        };
        Whirlwind.Weapon = state.Config.Gear.TwoHandSlashing;
        state.Config.Skills.Add(Skill.FuriousWhirlwind, 1);

        FuriousWhirlwind.ProcessEvent(whirlwindSpinEvent, state);

        state.Events.OfType<BleedAppliedEvent>().Should().HaveCount(3);
        state.Events.OfType<BleedAppliedEvent>().First().Target.Should().Be(state.Enemies.First());
        state.Events.OfType<BleedAppliedEvent>().Last().Target.Should().Be(state.Enemies.Last());
    }

    [Fact]
    public void Only_Applies_For_Slashing_Weapon()
    {
        var state = new SimulationState(new SimulationConfig());
        var whirlwindSpinEvent = new WhirlwindSpinEvent(123)
        {
            BaseDamage = 3500,
        };
        Whirlwind.Weapon = state.Config.Gear.TwoHandBludgeoning;
        state.Config.Skills.Add(Skill.FuriousWhirlwind, 1);

        FuriousWhirlwind.ProcessEvent(whirlwindSpinEvent, state);

        state.Events.Should().BeEmpty();
    }

    [Fact]
    public void Only_Applies_If_Skilled()
    {
        var state = new SimulationState(new SimulationConfig());
        var whirlwindSpinEvent = new WhirlwindSpinEvent(123)
        {
            BaseDamage = 3500,
        };
        Whirlwind.Weapon = state.Config.Gear.TwoHandSlashing;

        FuriousWhirlwind.ProcessEvent(whirlwindSpinEvent, state);

        state.Events.Should().BeEmpty();
    }

}
