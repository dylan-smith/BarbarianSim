using BarbarianSim.Aspects;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Aspects;

public sealed class AspectOfTheDireWhirlwindTests
{
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly AspectOfTheDireWhirlwind _aspect = new();

    public AspectOfTheDireWhirlwindTests()
    {
        _state.Config.Gear.Helm.Aspect = _aspect;
        _aspect.CritChance = 8;
        _aspect.MaxCritChance = 24;
    }

    [Fact]
    public void Returns_Max_Bonus_When_Spinning_For_Long_Time()
    {
        _state.Player.Auras.Add(Aura.Whirlwinding);
        var auraAppliedEvent = new AuraAppliedEvent(0, 0, Aura.Whirlwinding);
        _state.ProcessedEvents.Add(auraAppliedEvent);
        _state.CurrentTime = 123;

        _aspect.GetCritChanceBonus(_state).Should().Be(24);
    }

    [Fact]
    public void Returns_0_When_Not_Equipped()
    {
        _state.Config.Gear.Helm.Aspect = null;
        _state.Player.Auras.Add(Aura.Whirlwinding);
        var auraAppliedEvent = new AuraAppliedEvent(0, 0, Aura.Whirlwinding);
        _state.ProcessedEvents.Add(auraAppliedEvent);
        _state.CurrentTime = 123;

        _aspect.GetCritChanceBonus(_state).Should().Be(0);
    }

    [Fact]
    public void Returns_0_When_No_Aura()
    {
        var auraAppliedEvent = new AuraAppliedEvent(0, 0, Aura.Whirlwinding);
        _state.ProcessedEvents.Add(auraAppliedEvent);
        _state.CurrentTime = 123;

        _aspect.GetCritChanceBonus(_state).Should().Be(0);
    }

    [Fact]
    public void Bonus_Is_Based_On_Time_Spinning()
    {
        _state.Player.Auras.Add(Aura.Whirlwinding);
        var auraAppliedEvent = new AuraAppliedEvent(0, 0, Aura.Whirlwinding);
        _state.ProcessedEvents.Add(auraAppliedEvent);
        _state.CurrentTime = 2;

        _aspect.GetCritChanceBonus(_state).Should().Be(16);
    }

    [Fact]
    public void Starts_Looking_From_Last_AuraExpiredEvent()
    {
        _state.Player.Auras.Add(Aura.Whirlwinding);
        _state.ProcessedEvents.Add(new AuraAppliedEvent(0, 0, Aura.Whirlwinding));
        _state.ProcessedEvents.Add(new AuraExpiredEvent(5.0, Aura.Whirlwinding));
        _state.ProcessedEvents.Add(new AuraAppliedEvent(6.1, 0, Aura.Whirlwinding));
        _state.CurrentTime = 7.2;

        _aspect.GetCritChanceBonus(_state).Should().Be(8);
    }

    [Fact]
    public void Ignores_Redundant_AuraAppliedEvents()
    {
        _state.Player.Auras.Add(Aura.Whirlwinding);
        _state.ProcessedEvents.Add(new AuraAppliedEvent(0, 0, Aura.Whirlwinding));
        _state.ProcessedEvents.Add(new AuraExpiredEvent(5.0, Aura.Whirlwinding));
        _state.ProcessedEvents.Add(new AuraAppliedEvent(6.1, 0, Aura.Whirlwinding));
        _state.ProcessedEvents.Add(new AuraAppliedEvent(6.5, 0, Aura.Whirlwinding));
        _state.CurrentTime = 7.2;

        _aspect.GetCritChanceBonus(_state).Should().Be(8);
    }
}
