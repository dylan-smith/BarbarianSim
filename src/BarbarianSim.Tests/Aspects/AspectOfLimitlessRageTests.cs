using BarbarianSim.Aspects;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Aspects;

public sealed class AspectOfLimitlessRageTests
{
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly AspectOfLimitlessRage _aspect = new();

    [Fact]
    public void Returns_Min_DamageBonus_After_1_ExtraFury()
    {
        _state.Config.Gear.Helm.Aspect = _aspect;
        _aspect.Damage = 2;
        _aspect.MaxDamage = 30;

        var furyGeneratedEvent = new FuryGeneratedEvent(123, 20)
        {
            OverflowFury = 1
        };

        _aspect.ProcessEvent(furyGeneratedEvent, _state);
        _aspect.GetDamageBonus(_state, SkillType.Core).Should().Be(1.02);
    }

    [Fact]
    public void Returns_MaxDamage_After_Lots_Of_Overfury()
    {
        _state.Config.Gear.Helm.Aspect = _aspect;
        _aspect.Damage = 2;
        _aspect.MaxDamage = 30;

        var furyGeneratedEvent = new FuryGeneratedEvent(123, 20) { OverflowFury = 70 };

        _aspect.ProcessEvent(furyGeneratedEvent, _state);
        _aspect.GetDamageBonus(_state, SkillType.Core).Should().Be(1.3);
    }

    [Fact]
    public void Accumulates_Damage_Bonus_Over_Multiple_FuryGeneratedEvents()
    {
        _state.Config.Gear.Helm.Aspect = _aspect;
        _aspect.Damage = 2;
        _aspect.MaxDamage = 30;

        var furyGeneratedEvent1 = new FuryGeneratedEvent(123, 20) { OverflowFury = 2 };
        var furyGeneratedEvent2 = new FuryGeneratedEvent(123, 20) { OverflowFury = 3 };
        var furyGeneratedEvent3 = new FuryGeneratedEvent(123, 20) { OverflowFury = 4 };

        _aspect.ProcessEvent(furyGeneratedEvent1, _state);
        _aspect.ProcessEvent(furyGeneratedEvent2, _state);
        _aspect.ProcessEvent(furyGeneratedEvent3, _state);
        _aspect.GetDamageBonus(_state, SkillType.Core).Should().Be(1.18);
    }

    [Fact]
    public void DamageBonus_Goes_Away_After_Attack()
    {
        _state.Config.Gear.Helm.Aspect = _aspect;
        _aspect.Damage = 2;
        _aspect.MaxDamage = 30;

        var furyGeneratedEvent = new FuryGeneratedEvent(123, 20) { OverflowFury = 70 };

        _aspect.ProcessEvent(furyGeneratedEvent, _state);
        _aspect.GetDamageBonus(_state, SkillType.Core).Should().Be(1.3);
        _aspect.GetDamageBonus(_state, SkillType.Core).Should().Be(1.0);
    }

    [Fact]
    public void GetDamageBonus_Returns_1_When_Not_Equipped()
    {
        _aspect.Damage = 2;
        _aspect.MaxDamage = 30;

        var furyGeneratedEvent = new FuryGeneratedEvent(123, 20) { OverflowFury = 70 };

        _aspect.ProcessEvent(furyGeneratedEvent, _state);
        _aspect.GetDamageBonus(_state, SkillType.Core).Should().Be(1.0);
    }

    [Fact]
    public void GetDamageBonus_Returns_1_When_SkillType_Not_Core()
    {
        _state.Config.Gear.Helm.Aspect = _aspect;
        _aspect.Damage = 2;
        _aspect.MaxDamage = 30;

        var furyGeneratedEvent = new FuryGeneratedEvent(123, 20) { OverflowFury = 70 };

        _aspect.ProcessEvent(furyGeneratedEvent, _state);
        _aspect.GetDamageBonus(_state, SkillType.Basic).Should().Be(1.0);
    }
}
