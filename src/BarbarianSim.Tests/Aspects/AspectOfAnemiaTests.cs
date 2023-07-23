using BarbarianSim.Aspects;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.Aspects;

public sealed class AspectOfAnemiaTests
{
    private readonly Mock<RandomGenerator> _mockRandomGenerator = TestHelpers.CreateMock<RandomGenerator>();
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly AspectOfAnemia _aspect;

    public AspectOfAnemiaTests()
    {
        _mockRandomGenerator.Setup(m => m.Roll(RollType.AspectOfAnemia)).Returns(1.0);
        _aspect = new AspectOfAnemia(_mockRandomGenerator.Object) { StunChance = 30 };
        _state.Config.Gear.Helm.Aspect = _aspect;
    }

    [Fact]
    public void Creates_AuraAppliedEvent()
    {
        _state.Enemies.First().Auras.Add(Aura.Bleeding);
        _mockRandomGenerator.Setup(m => m.Roll(RollType.AspectOfAnemia)).Returns(0.29);
        var luckyHitEvent = new LuckyHitEvent(123, SkillType.Basic, _state.Enemies.First());

        _aspect.ProcessEvent(luckyHitEvent, _state);

        _state.Events.Should().ContainSingle(e => e is AuraAppliedEvent);
        _state.Events.OfType<AuraAppliedEvent>().First().Timestamp.Should().Be(123.0);
        _state.Events.OfType<AuraAppliedEvent>().First().Duration.Should().Be(2);
        _state.Events.OfType<AuraAppliedEvent>().First().Aura.Should().Be(Aura.Stun);
        _state.Events.OfType<AuraAppliedEvent>().First().Target.Should().Be(_state.Enemies.First());
    }

    [Fact]
    public void Does_Nothing_When_Not_Equipped()
    {
        _state.Config.Gear.Helm.Aspect = null;
        _state.Enemies.First().Auras.Add(Aura.Bleeding);
        _mockRandomGenerator.Setup(m => m.Roll(RollType.AspectOfAnemia)).Returns(0.29);
        var luckyHitEvent = new LuckyHitEvent(123, SkillType.Basic, _state.Enemies.First());

        _aspect.ProcessEvent(luckyHitEvent, _state);

        _state.Events.Should().NotContain(e => e is AuraAppliedEvent);
    }

    [Fact]
    public void Does_Nothing_When_Enemy_Not_Bleeding()
    {
        var config = new SimulationConfig();
        config.EnemySettings.NumberOfEnemies = 3;
        var state = new SimulationState(config);
        state.Config.Gear.Helm.Aspect = _aspect;
        state.Enemies.First().Auras.Add(Aura.Bleeding);
        _mockRandomGenerator.Setup(m => m.Roll(RollType.AspectOfAnemia)).Returns(0.29);
        var luckyHitEvent = new LuckyHitEvent(123, SkillType.Basic, state.Enemies.Last());

        _aspect.ProcessEvent(luckyHitEvent, state);

        state.Events.Should().NotContain(e => e is AuraAppliedEvent);
    }

    [Fact]
    public void Does_Nothing_When_Roll_Too_High()
    {
        _state.Enemies.First().Auras.Add(Aura.Bleeding);
        _mockRandomGenerator.Setup(m => m.Roll(RollType.AspectOfAnemia)).Returns(0.31);
        var luckyHitEvent = new LuckyHitEvent(123, SkillType.Basic, _state.Enemies.First());

        _aspect.ProcessEvent(luckyHitEvent, _state);

        _state.Events.Should().NotContain(e => e is AuraAppliedEvent);
    }
}
