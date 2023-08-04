using BarbarianSim.Aspects;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Aspects;

public class BoldChieftainsAspectTests
{
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly BoldChieftainsAspect _aspect = new();

    public BoldChieftainsAspectTests()
    {
        _state.Config.Gear.Helm.Aspect = _aspect;
        _aspect.CooldownReduction = 1.9;
    }

    [Fact]
    public void ChallengingShout_Reduces_Cooldown()
    {
        var auraAppliedEvent = new AuraAppliedEvent(123, null, 20, Aura.ChallengingShoutCooldown);
        _state.Events.Add(auraAppliedEvent);
        var challengingShoutEvent = new ChallengingShoutEvent(123);

        _aspect.ProcessEvent(challengingShoutEvent, _state);

        auraAppliedEvent.Duration.Should().Be(20 - 1.9);
    }

    [Fact]
    public void ChallengingShout_Cooldown_Reduction_Increases_With_Nearby_Enemies()
    {
        var config = new SimulationConfig();
        config.EnemySettings.NumberOfEnemies = 3;
        config.Gear.Helm.Aspect = _aspect;
        var state = new SimulationState(config);

        var auraAppliedEvent = new AuraAppliedEvent(123, null, 20, Aura.ChallengingShoutCooldown);
        state.Events.Add(auraAppliedEvent);
        var challengingShoutEvent = new ChallengingShoutEvent(123);

        _aspect.ProcessEvent(challengingShoutEvent, state);

        auraAppliedEvent.Duration.Should().Be(14.3);
    }

    [Fact]
    public void ChallengingShout_Does_Nothing_When_Not_Equipped()
    {
        var config = new SimulationConfig();
        config.EnemySettings.NumberOfEnemies = 3;
        var state = new SimulationState(config);

        var auraAppliedEvent = new AuraAppliedEvent(123, null, 20, Aura.ChallengingShoutCooldown);
        state.Events.Add(auraAppliedEvent);
        var challengingShoutEvent = new ChallengingShoutEvent(123);

        _aspect.ProcessEvent(challengingShoutEvent, state);

        auraAppliedEvent.Duration.Should().Be(20);
    }

    [Fact]
    public void ChallengingShout_Cooldown_Reduction_Cannot_Exceed_Max()
    {
        var config = new SimulationConfig();
        config.EnemySettings.NumberOfEnemies = 17;
        config.Gear.Helm.Aspect = _aspect;
        var state = new SimulationState(config);

        var auraAppliedEvent = new AuraAppliedEvent(123, null, 20, Aura.ChallengingShoutCooldown);
        state.Events.Add(auraAppliedEvent);
        var challengingShoutEvent = new ChallengingShoutEvent(123);

        _aspect.ProcessEvent(challengingShoutEvent, state);

        auraAppliedEvent.Duration.Should().Be(14);
    }

    [Fact]
    public void RallyingCry_Reduces_Cooldown()
    {
        var auraAppliedEvent = new AuraAppliedEvent(123, null, 20, Aura.RallyingCryCooldown);
        _state.Events.Add(auraAppliedEvent);
        var rallyingCryEvent = new RallyingCryEvent(123);

        _aspect.ProcessEvent(rallyingCryEvent, _state);

        auraAppliedEvent.Duration.Should().Be(20 - 1.9);
    }

    [Fact]
    public void RallyingCry_Cooldown_Reduction_Increases_With_Nearby_Enemies()
    {
        var config = new SimulationConfig();
        config.EnemySettings.NumberOfEnemies = 3;
        config.Gear.Helm.Aspect = _aspect;
        var state = new SimulationState(config);

        var auraAppliedEvent = new AuraAppliedEvent(123, null, 20, Aura.RallyingCryCooldown);
        state.Events.Add(auraAppliedEvent);
        var rallyingCryEvent = new RallyingCryEvent(123);

        _aspect.ProcessEvent(rallyingCryEvent, state);

        auraAppliedEvent.Duration.Should().Be(14.3);
    }

    [Fact]
    public void RallyingCry_Does_Nothing_When_Not_Equipped()
    {
        var config = new SimulationConfig();
        config.EnemySettings.NumberOfEnemies = 3;
        var state = new SimulationState(config);

        var auraAppliedEvent = new AuraAppliedEvent(123, null, 20, Aura.RallyingCryCooldown);
        state.Events.Add(auraAppliedEvent);
        var rallyingCryEvent = new RallyingCryEvent(123);

        _aspect.ProcessEvent(rallyingCryEvent, state);

        auraAppliedEvent.Duration.Should().Be(20);
    }

    [Fact]
    public void RallyingCry_Cooldown_Reduction_Cannot_Exceed_Max()
    {
        var config = new SimulationConfig();
        config.EnemySettings.NumberOfEnemies = 17;
        config.Gear.Helm.Aspect = _aspect;
        var state = new SimulationState(config);

        var auraAppliedEvent = new AuraAppliedEvent(123, null, 20, Aura.RallyingCryCooldown);
        state.Events.Add(auraAppliedEvent);
        var rallyingCryEvent = new RallyingCryEvent(123);

        _aspect.ProcessEvent(rallyingCryEvent, state);

        auraAppliedEvent.Duration.Should().Be(14);
    }

    [Fact]
    public void WarCry_Reduces_Cooldown()
    {
        var auraAppliedEvent = new AuraAppliedEvent(123, null, 20, Aura.WarCryCooldown);
        _state.Events.Add(auraAppliedEvent);
        var warCryEvent = new WarCryEvent(123);

        _aspect.ProcessEvent(warCryEvent, _state);

        auraAppliedEvent.Duration.Should().Be(20 - 1.9);
    }

    [Fact]
    public void WarCry_Cooldown_Reduction_Increases_With_Nearby_Enemies()
    {
        var config = new SimulationConfig();
        config.EnemySettings.NumberOfEnemies = 3;
        config.Gear.Helm.Aspect = _aspect;
        var state = new SimulationState(config);

        var auraAppliedEvent = new AuraAppliedEvent(123, null, 20, Aura.WarCryCooldown);
        state.Events.Add(auraAppliedEvent);
        var warCryEvent = new WarCryEvent(123);

        _aspect.ProcessEvent(warCryEvent, state);

        auraAppliedEvent.Duration.Should().Be(14.3);
    }

    [Fact]
    public void WarCry_Does_Nothing_When_Not_Equipped()
    {
        var config = new SimulationConfig();
        config.EnemySettings.NumberOfEnemies = 3;
        var state = new SimulationState(config);

        var auraAppliedEvent = new AuraAppliedEvent(123, null, 20, Aura.WarCryCooldown);
        state.Events.Add(auraAppliedEvent);
        var warCryEvent = new WarCryEvent(123);

        _aspect.ProcessEvent(warCryEvent, state);

        auraAppliedEvent.Duration.Should().Be(20);
    }

    [Fact]
    public void WarCry_Cooldown_Reduction_Cannot_Exceed_Max()
    {
        var config = new SimulationConfig();
        config.EnemySettings.NumberOfEnemies = 17;
        config.Gear.Helm.Aspect = _aspect;
        var state = new SimulationState(config);

        var auraAppliedEvent = new AuraAppliedEvent(123, null, 20, Aura.WarCryCooldown);
        state.Events.Add(auraAppliedEvent);
        var warCryEvent = new WarCryEvent(123);

        _aspect.ProcessEvent(warCryEvent, state);

        auraAppliedEvent.Duration.Should().Be(14);
    }
}
