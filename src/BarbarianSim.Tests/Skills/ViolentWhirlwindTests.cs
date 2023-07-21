using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.Skills;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Skills;

public class ViolentWhirlwindTests
{
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly ViolentWhirlwind _skill = new();

    [Fact]
    public void Creates_ViolentWhirlwindAppliedEvent()
    {
        _state.Config.Skills.Add(Skill.ViolentWhirlwind, 1);
        var whirlwindStartedEvent = new WhirlwindSpinEvent(123.0);

        _skill.ProcessEvent(whirlwindStartedEvent, _state);

        _state.Events.Should().ContainSingle(e => e is AuraAppliedEvent && ((AuraAppliedEvent)e).Aura == Aura.ViolentWhirlwind);
        _state.Events.OfType<AuraAppliedEvent>().Single().Timestamp.Should().Be(125);
        _state.Events.OfType<AuraAppliedEvent>().Single().Duration.Should().Be(0);
        _state.Events.OfType<AuraAppliedEvent>().Single().Aura.Should().Be(Aura.ViolentWhirlwind);
    }

    [Fact]
    public void Does_Nothing_If_Not_Skilled()
    {
        var whirlwindStartedEvent = new WhirlwindSpinEvent(123.0);

        _skill.ProcessEvent(whirlwindStartedEvent, _state);

        _state.Events.Should().BeEmpty();
    }

    [Fact]
    public void GetDamageBonus_When_Active()
    {
        _state.Player.Auras.Add(Aura.ViolentWhirlwind);
        _skill.GetDamageBonus(_state, DamageSource.Whirlwind).Should().Be(1.3);
    }

    [Fact]
    public void GetDamageBonus_Return_1_When_No_Aura()
    {
        _skill.GetDamageBonus(_state, DamageSource.Whirlwind).Should().Be(1.0);
    }

    [Fact]
    public void GetDamageBonus_Return_1_When_Other_DamageSource()
    {
        _state.Player.Auras.Add(Aura.ViolentWhirlwind);
        _skill.GetDamageBonus(_state, DamageSource.LungingStrike).Should().Be(1.0);
    }
}
