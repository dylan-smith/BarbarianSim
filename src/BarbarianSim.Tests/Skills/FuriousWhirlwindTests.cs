using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.Skills;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Skills;

public class FuriousWhirlwindTests
{
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly FuriousWhirlwind _skill = new();

    [Fact]
    public void Creates_BleedAppliedEvent()
    {
        var whirlwindSpinEvent = new WhirlwindSpinEvent(123)
        {
            BaseDamage = 3500,
        };
        _state.Config.PlayerSettings.SkillWeapons.Add(Skill.Whirlwind, _state.Config.Gear.TwoHandSlashing);
        _state.Config.Skills.Add(Skill.FuriousWhirlwind, 1);

        _skill.ProcessEvent(whirlwindSpinEvent, _state);

        _state.Events.Should().ContainSingle(e => e is BleedAppliedEvent);
        _state.Events.OfType<BleedAppliedEvent>().Single().Timestamp.Should().Be(123);
        _state.Events.OfType<BleedAppliedEvent>().Single().Damage.Should().Be(3500 * 0.4);
        _state.Events.OfType<BleedAppliedEvent>().Single().Duration.Should().Be(5);
        _state.Events.OfType<BleedAppliedEvent>().Single().Target.Should().Be(_state.Enemies.First());
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
        state.Config.PlayerSettings.SkillWeapons.Add(Skill.Whirlwind, state.Config.Gear.TwoHandSlashing);
        state.Config.Skills.Add(Skill.FuriousWhirlwind, 1);

        _skill.ProcessEvent(whirlwindSpinEvent, state);

        state.Events.OfType<BleedAppliedEvent>().Should().HaveCount(3);
        state.Events.OfType<BleedAppliedEvent>().First().Target.Should().Be(state.Enemies.First());
        state.Events.OfType<BleedAppliedEvent>().Last().Target.Should().Be(state.Enemies.Last());
    }

    [Fact]
    public void Only_Applies_For_Slashing_Weapon()
    {
        var whirlwindSpinEvent = new WhirlwindSpinEvent(123)
        {
            BaseDamage = 3500,
        };
        _state.Config.PlayerSettings.SkillWeapons.Add(Skill.Whirlwind, _state.Config.Gear.TwoHandBludgeoning);
        _state.Config.Skills.Add(Skill.FuriousWhirlwind, 1);

        _skill.ProcessEvent(whirlwindSpinEvent, _state);

        _state.Events.Should().BeEmpty();
    }

    [Fact]
    public void Only_Applies_If_Skilled()
    {
        var whirlwindSpinEvent = new WhirlwindSpinEvent(123)
        {
            BaseDamage = 3500,
        };
        _state.Config.PlayerSettings.SkillWeapons.Add(Skill.Whirlwind, _state.Config.Gear.TwoHandSlashing);

        _skill.ProcessEvent(whirlwindSpinEvent, _state);

        _state.Events.Should().BeEmpty();
    }
}
