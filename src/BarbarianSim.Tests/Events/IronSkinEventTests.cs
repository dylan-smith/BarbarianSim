using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Events;

public sealed class IronSkinEventTests : IDisposable
{
    public void Dispose() => BaseStatCalculator.ClearMocks();

    [Fact]
    public void Creates_CooldownCompletedEvent()
    {
        var state = new SimulationState(new SimulationConfig());
        var ironSkinEvent = new IronSkinEvent(123);

        ironSkinEvent.ProcessEvent(state);

        ironSkinEvent.IronSkinCooldownAuraAppliedEvent.Should().NotBeNull();
        state.Events.Should().Contain(ironSkinEvent.IronSkinCooldownAuraAppliedEvent);
        state.Events.Should().ContainSingle(e => e is AuraAppliedEvent && ((AuraAppliedEvent)e).Aura == Aura.IronSkinCooldown);
        ironSkinEvent.IronSkinCooldownAuraAppliedEvent.Timestamp.Should().Be(123);
        ironSkinEvent.IronSkinCooldownAuraAppliedEvent.Duration.Should().Be(14);
        ironSkinEvent.IronSkinCooldownAuraAppliedEvent.Aura.Should().Be(Aura.IronSkinCooldown);
    }

    [Fact]
    public void Creates_AuraAppliedEvent()
    {
        var state = new SimulationState(new SimulationConfig());
        var ironSkinEvent = new IronSkinEvent(123);

        ironSkinEvent.ProcessEvent(state);

        ironSkinEvent.IronSkinAuraAppliedEvent.Should().NotBeNull();
        state.Events.Should().Contain(ironSkinEvent.IronSkinAuraAppliedEvent);
        state.Events.Should().ContainSingle(e => e is AuraAppliedEvent && ((AuraAppliedEvent)e).Aura == Aura.IronSkin);
        ironSkinEvent.IronSkinAuraAppliedEvent.Timestamp.Should().Be(123);
        ironSkinEvent.IronSkinAuraAppliedEvent.Duration.Should().Be(5);
        ironSkinEvent.IronSkinAuraAppliedEvent.Aura.Should().Be(Aura.IronSkin);
    }

    [Fact]
    public void Creates_BarrierAppliedEvent()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.IronSkin, 5);
        state.Player.Life = 800;
        BaseStatCalculator.InjectMock(typeof(MaxLifeCalculator), new FakeStatCalculator(1000));
        var ironSkinEvent = new IronSkinEvent(123);

        ironSkinEvent.ProcessEvent(state);

        ironSkinEvent.BarrierAppliedEvent.Should().NotBeNull();
        state.Events.Should().Contain(ironSkinEvent.BarrierAppliedEvent);
        state.Events.Should().ContainSingle(e => e is BarrierAppliedEvent);
        ironSkinEvent.BarrierAppliedEvent.Timestamp.Should().Be(123);
        ironSkinEvent.BarrierAppliedEvent.BarrierAmount.Should().Be(140);
        ironSkinEvent.BarrierAppliedEvent.Duration.Should().Be(5);
    }

    [Fact]
    public void EnhancedIronSkin_Increases_Barrier_By_20_Percent_Max_Life()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.IronSkin, 5);
        state.Config.Skills.Add(Skill.EnhancedIronSkin, 1);
        state.Player.Life = 800;
        BaseStatCalculator.InjectMock(typeof(MaxLifeCalculator), new FakeStatCalculator(1000));
        var ironSkinEvent = new IronSkinEvent(123);

        ironSkinEvent.ProcessEvent(state);

        ironSkinEvent.BarrierAppliedEvent.Should().NotBeNull();
        state.Events.Should().Contain(ironSkinEvent.BarrierAppliedEvent);
        state.Events.Should().ContainSingle(e => e is BarrierAppliedEvent);
        ironSkinEvent.BarrierAppliedEvent.Timestamp.Should().Be(123);
        ironSkinEvent.BarrierAppliedEvent.BarrierAmount.Should().Be(340);
        ironSkinEvent.BarrierAppliedEvent.Duration.Should().Be(5);
    }

    [Fact]
    public void TacticalIronSkin_Creates_5_HealingEvents()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.IronSkin, 5);
        state.Config.Skills.Add(Skill.EnhancedIronSkin, 1);
        state.Config.Skills.Add(Skill.TacticalIronSkin, 1);
        state.Player.Life = 800;
        BaseStatCalculator.InjectMock(typeof(MaxLifeCalculator), new FakeStatCalculator(1000));
        var ironSkinEvent = new IronSkinEvent(123);

        ironSkinEvent.ProcessEvent(state);

        ironSkinEvent.HealingEvents.Should().HaveCount(5);
        state.Events.Should().Contain(ironSkinEvent.HealingEvents);
        ironSkinEvent.HealingEvents[0].Timestamp.Should().Be(124);
        ironSkinEvent.HealingEvents[1].Timestamp.Should().Be(125);
        ironSkinEvent.HealingEvents[2].Timestamp.Should().Be(126);
        ironSkinEvent.HealingEvents[3].Timestamp.Should().Be(127);
        ironSkinEvent.HealingEvents[4].Timestamp.Should().Be(128);
        ironSkinEvent.HealingEvents[0].BaseAmountHealed.Should().Be(34);
        ironSkinEvent.HealingEvents[1].BaseAmountHealed.Should().Be(34);
        ironSkinEvent.HealingEvents[2].BaseAmountHealed.Should().Be(34);
        ironSkinEvent.HealingEvents[3].BaseAmountHealed.Should().Be(34);
        ironSkinEvent.HealingEvents[4].BaseAmountHealed.Should().Be(34);
    }

    [Fact]
    public void StrategicIronSkin_Creates_FortifyGeneratedEvent()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.StrategicIronSkin, 1);
        state.Player.Life = 800;
        state.Player.BaseLife = 700;
        BaseStatCalculator.InjectMock(typeof(MaxLifeCalculator), new FakeStatCalculator(1000));
        var ironSkinEvent = new IronSkinEvent(123);

        ironSkinEvent.ProcessEvent(state);

        ironSkinEvent.FortifyGeneratedEvent.Should().NotBeNull();
        state.Events.Should().Contain(ironSkinEvent.FortifyGeneratedEvent);
        state.Events.Should().ContainSingle(e => e is FortifyGeneratedEvent);
        ironSkinEvent.FortifyGeneratedEvent.Timestamp.Should().Be(123);
        ironSkinEvent.FortifyGeneratedEvent.Amount.Should().Be(105);
    }

    [Fact]
    public void StrategicIronSkin_Fortify_Amount_Doubles_When_Below_Half_Life()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.StrategicIronSkin, 1);
        state.Player.Life = 400;
        state.Player.BaseLife = 700;
        BaseStatCalculator.InjectMock(typeof(MaxLifeCalculator), new FakeStatCalculator(1000));
        var ironSkinEvent = new IronSkinEvent(123);

        ironSkinEvent.ProcessEvent(state);

        ironSkinEvent.FortifyGeneratedEvent.Should().NotBeNull();
        state.Events.Should().Contain(ironSkinEvent.FortifyGeneratedEvent);
        state.Events.Should().ContainSingle(e => e is FortifyGeneratedEvent);
        ironSkinEvent.FortifyGeneratedEvent.Timestamp.Should().Be(123);
        ironSkinEvent.FortifyGeneratedEvent.Amount.Should().Be(210);
    }
}
