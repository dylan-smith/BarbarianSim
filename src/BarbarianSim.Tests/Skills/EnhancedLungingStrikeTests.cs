using BarbarianSim.Abilities;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.Skills;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Skills;

public sealed class EnhancedLungingStrikeTests : IDisposable
{
    public void Dispose() => BaseStatCalculator.ClearMocks();

    [Fact]
    public void Creates_HealingEvent_When_Enemy_Healthy()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Enemies.First().MaxLife = 1000;
        state.Enemies.First().Life = 1000;
        state.Config.Skills.Add(Skill.EnhancedLungingStrike, 1);
        BaseStatCalculator.InjectMock(typeof(MaxLifeCalculator), new FakeStatCalculator(1000));
        var damageEvent = new DamageEvent(123, 1200, DamageType.Direct, DamageSource.LungingStrike, SkillType.Basic, state.Enemies.First());

        EnhancedLungingStrike.ProcessEvent(damageEvent, state);

        state.Events.Should().ContainSingle(e => e is HealingEvent);
        state.Events.OfType<HealingEvent>().First().BaseAmountHealed.Should().Be(20);
        state.Events.OfType<HealingEvent>().First().Timestamp.Should().Be(123);
    }

    [Fact]
    public void EnhancedLungingStrike_Does_Not_Create_HealingEvent_When_Enemy_Not_Healthy()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Enemies.First().MaxLife = 1000;
        state.Enemies.First().Life = 600;
        state.Config.Skills.Add(Skill.EnhancedLungingStrike, 1);
        BaseStatCalculator.InjectMock(typeof(MaxLifeCalculator), new FakeStatCalculator(1000));
        var damageEvent = new DamageEvent(123, 1200, DamageType.Direct, DamageSource.LungingStrike, SkillType.Basic, state.Enemies.First());

        EnhancedLungingStrike.ProcessEvent(damageEvent, state);

        state.Events.Should().NotContain(e => e is HealingEvent);
    }
}
