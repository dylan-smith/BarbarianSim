using BarbarianSim.Aspects;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.Rotations;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Rotations;

public class SpinToWinTests
{
    [Fact]
    public void Uses_LungingStrike_When_Not_Enough_Fury_For_Whirlwind()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Fury = 0;
        var rotation = new SpinToWin();

        rotation.Execute(state);

        state.Events.Any(e => e is LungingStrikeEvent).Should().BeTrue();
    }

    [Fact]
    public void Uses_Whirlwind_When_Enough_Fury()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Fury = 25;
        var rotation = new SpinToWin();

        rotation.Execute(state);

        state.Events.Any(e => e is WhirlwindStartedEvent).Should().BeTrue();
    }

    [Fact]
    public void Stops_Whirlwind_When_Gohrs_Reaches_Max_HitCount()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Auras.Add(Aura.Whirlwinding);
        var aspect = new GohrsDevastatingGrips(20.0)
        {
            HitCount = GohrsDevastatingGrips.MAX_HIT_COUNT
        };
        state.Config.Gear.Helm.Aspect = aspect;
        var rotation = new SpinToWin();

        rotation.Execute(state);

        state.Player.Auras.Should().NotContain(Aura.Whirlwinding);
    }

    [Fact]
    public void Uses_All_Shouts_When_Not_On_Cooldown()
    {
        var state = new SimulationState(new SimulationConfig());
        var rotation = new SpinToWin();

        rotation.Execute(state);

        state.Events.Any(e => e is RallyingCryEvent).Should().BeTrue();
        state.Events.Any(e => e is ChallengingShoutEvent).Should().BeTrue();
        state.Events.Any(e => e is WarCryEvent).Should().BeTrue();
    }

    [Fact]
    public void Uses_WrathOfTheBerserker_When_Not_On_Cooldown()
    {
        var state = new SimulationState(new SimulationConfig());
        var rotation = new SpinToWin();

        rotation.Execute(state);

        state.Events.Any(e => e is WrathOfTheBerserkerEvent).Should().BeTrue();
    }

    [Fact]
    public void Does_Nothing_When_Shouts_On_Cooldown_Wrath_On_Cooldown_And_No_Fury()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Fury = 0;
        state.Player.Auras.Add(Aura.WeaponCooldown);
        state.Player.Auras.Add(Aura.RallyingCryCooldown);
        state.Player.Auras.Add(Aura.ChallengingShoutCooldown);
        state.Player.Auras.Add(Aura.WarCryCooldown);
        state.Player.Auras.Add(Aura.WrathOfTheBerserkerCooldown);
        var rotation = new SpinToWin();

        rotation.Execute(state);

        state.Events.Should().BeEmpty();
    }
}
