using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.Paragon;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Paragon;

public class MarshalTests
{
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly Marshal _paragon = new();

    public MarshalTests() => _state.Config.ParagonNodes.Add(ParagonNode.Marshal);

    [Fact]
    public void Reduces_Shout_Cooldown()
    {
        var warCryCooldownAuraExpiredEvent = new AuraExpiredEvent(123, null, Aura.WarCryCooldown);
        _state.Events.Add(warCryCooldownAuraExpiredEvent);

        var challengingShoutCooldownAuraAppliedEvent = new AuraAppliedEvent(112, null, 20, Aura.ChallengingShoutCooldown);

        _paragon.ProcessEvent(challengingShoutCooldownAuraAppliedEvent, _state);

        warCryCooldownAuraExpiredEvent.Timestamp.Should().Be(121.8);
    }

    [Fact]
    public void Does_Nothing_If_Paragon_Not_Active()
    {
        _state.Config.ParagonNodes.Remove(ParagonNode.Marshal);
        var warCryCooldownAuraExpiredEvent = new AuraExpiredEvent(123, null, Aura.WarCryCooldown);
        _state.Events.Add(warCryCooldownAuraExpiredEvent);

        var challengingShoutCooldownAuraAppliedEvent = new AuraAppliedEvent(112, null, 20, Aura.ChallengingShoutCooldown);

        _paragon.ProcessEvent(challengingShoutCooldownAuraAppliedEvent, _state);

        warCryCooldownAuraExpiredEvent.Timestamp.Should().Be(123);
    }

    [Fact]
    public void Reduces_Multiple_Shout_Cooldowns()
    {
        var warCryCooldownAuraExpiredEvent = new AuraExpiredEvent(123, null, Aura.WarCryCooldown);
        _state.Events.Add(warCryCooldownAuraExpiredEvent);

        var rallyingCryCooldownAuraExpiredEvent = new AuraExpiredEvent(125, null, Aura.RallyingCryCooldown);
        _state.Events.Add(rallyingCryCooldownAuraExpiredEvent);

        var challengingShoutCooldownAuraAppliedEvent = new AuraAppliedEvent(112, null, 20, Aura.ChallengingShoutCooldown);

        _paragon.ProcessEvent(challengingShoutCooldownAuraAppliedEvent, _state);

        warCryCooldownAuraExpiredEvent.Timestamp.Should().Be(121.8);
        rallyingCryCooldownAuraExpiredEvent.Timestamp.Should().Be(123.8);
    }

    [Fact]
    public void Does_Not_Reduce_Cooldown_Of_Triggering_Shout()
    {
        var warCryCooldownAuraExpiredEvent = new AuraExpiredEvent(123, null, Aura.WarCryCooldown);
        _state.Events.Add(warCryCooldownAuraExpiredEvent);

        var challengingShoutCooldownAuraExpiredEvent = new AuraExpiredEvent(132, null, Aura.ChallengingShoutCooldown);
        _state.Events.Add(challengingShoutCooldownAuraExpiredEvent);

        var challengingShoutCooldownAuraAppliedEvent = new AuraAppliedEvent(112, null, 20, Aura.ChallengingShoutCooldown);

        _paragon.ProcessEvent(challengingShoutCooldownAuraAppliedEvent, _state);

        warCryCooldownAuraExpiredEvent.Timestamp.Should().Be(121.8);
        challengingShoutCooldownAuraExpiredEvent.Timestamp.Should().Be(132);
    }

    [Fact]
    public void Cooldown_Cannot_Expire_In_The_Past()
    {
        var warCryCooldownAuraExpiredEvent = new AuraExpiredEvent(112.5, null, Aura.WarCryCooldown);
        _state.Events.Add(warCryCooldownAuraExpiredEvent);
        _state.CurrentTime = 112;

        var challengingShoutCooldownAuraAppliedEvent = new AuraAppliedEvent(112, null, 20, Aura.ChallengingShoutCooldown);

        _paragon.ProcessEvent(challengingShoutCooldownAuraAppliedEvent, _state);

        warCryCooldownAuraExpiredEvent.Timestamp.Should().Be(112);
    }
}
