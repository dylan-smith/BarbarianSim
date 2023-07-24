using BarbarianSim.Aspects;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Aspects;

public sealed class AspectOfTheIronWarriorTests
{
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly AspectOfTheIronWarrior _aspect = new();

    public AspectOfTheIronWarriorTests()
    {
        _aspect.DamageReduction = 28;
        _state.Config.Gear.Helm.Aspect = _aspect;
    }

    [Fact]
    public void Creates_AuraAppliedEvent()
    {
        var ironSkinEvent = new IronSkinEvent(123);

        _aspect.ProcessEvent(ironSkinEvent, _state);

        _state.Events.Should().ContainSingle(e => e is AuraAppliedEvent);
        _state.Events.OfType<AuraAppliedEvent>().First().Timestamp.Should().Be(123.0);
        _state.Events.OfType<AuraAppliedEvent>().First().Duration.Should().Be(5);
        _state.Events.OfType<AuraAppliedEvent>().First().Aura.Should().Be(Aura.Unstoppable);
    }

    [Fact]
    public void Does_Nothing_When_Not_Equipped()
    {
        _state.Config.Gear.Helm.Aspect = null;
        var ironSkinEvent = new IronSkinEvent(123);

        _aspect.ProcessEvent(ironSkinEvent, _state);

        _state.Events.Should().NotContain(e => e is AuraAppliedEvent);
    }

    [Fact]
    public void GetDamageReductionBonus_When_Active()
    {
        _state.Player.Auras.Add(Aura.IronSkin);

        _aspect.GetDamageReductionBonus(_state).Should().Be(28);
    }

    [Fact]
    public void GetDamageReductionBonus_Returns_0_When_Not_Equipped()
    {
        _state.Config.Gear.Helm.Aspect = null;
        _state.Player.Auras.Add(Aura.IronSkin);

        _aspect.GetDamageReductionBonus(_state).Should().Be(0);
    }

    [Fact]
    public void GetDamageReductionBonus_Returns_0_When_No_IronSkin_Aura()
    {
        _aspect.GetDamageReductionBonus(_state).Should().Be(0);
    }
}
