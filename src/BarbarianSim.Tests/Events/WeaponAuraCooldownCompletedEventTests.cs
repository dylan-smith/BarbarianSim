﻿using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Events;

public class WeaponAuraCooldownCompletedEventTests
{
    [Fact]
    public void Removes_Aura()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Auras.Add(Aura.WeaponCooldown);
        var e = new WeaponAuraCooldownCompletedEvent(0.0);

        e.ProcessEvent(state);

        state.Player.Auras.Should().BeEmpty();
    }

    [Fact]
    public void Throws_If_Aura_Is_Missing()
    {
        var state = new SimulationState(new SimulationConfig());
        var e = new WeaponAuraCooldownCompletedEvent(0.0);

        var act = () => e.ProcessEvent(state);

        act.Should().Throw<Exception>();
    }
}