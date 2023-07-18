using BarbarianSim.Abilities;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.EventHandlers;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.EventHandlers;

public class IronSkinEventHandlerTests
{
    private readonly Mock<MaxLifeCalculator> _mockMaxLifeCalculator = TestHelpers.CreateMock<MaxLifeCalculator>();
    private readonly Mock<IronSkin> _mockIronSkin = TestHelpers.CreateMock<IronSkin>();
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly IronSkinEventHandler _handler;

    public IronSkinEventHandlerTests()
    {
        _mockMaxLifeCalculator.Setup(m => m.Calculate(It.IsAny<SimulationState>()))
                              .Returns(1200);

        _mockIronSkin.Setup(m => m.GetBarrierPercentage(It.IsAny<SimulationState>()))
                     .Returns(0.6);

        _handler = new IronSkinEventHandler(_mockMaxLifeCalculator.Object, _mockIronSkin.Object);
    }

    [Fact]
    public void Creates_CooldownCompletedEvent()
    {
        var ironSkinEvent = new IronSkinEvent(123);

        _handler.ProcessEvent(ironSkinEvent, _state);

        ironSkinEvent.IronSkinCooldownAuraAppliedEvent.Should().NotBeNull();
        _state.Events.Should().Contain(ironSkinEvent.IronSkinCooldownAuraAppliedEvent);
        _state.Events.Should().ContainSingle(e => e is AuraAppliedEvent && ((AuraAppliedEvent)e).Aura == Aura.IronSkinCooldown);
        ironSkinEvent.IronSkinCooldownAuraAppliedEvent.Timestamp.Should().Be(123);
        ironSkinEvent.IronSkinCooldownAuraAppliedEvent.Duration.Should().Be(14);
        ironSkinEvent.IronSkinCooldownAuraAppliedEvent.Aura.Should().Be(Aura.IronSkinCooldown);
    }

    [Fact]
    public void Creates_AuraAppliedEvent()
    {
        var ironSkinEvent = new IronSkinEvent(123);

        _handler.ProcessEvent(ironSkinEvent, _state);

        ironSkinEvent.IronSkinAuraAppliedEvent.Should().NotBeNull();
        _state.Events.Should().Contain(ironSkinEvent.IronSkinAuraAppliedEvent);
        _state.Events.Should().ContainSingle(e => e is AuraAppliedEvent && ((AuraAppliedEvent)e).Aura == Aura.IronSkin);
        ironSkinEvent.IronSkinAuraAppliedEvent.Timestamp.Should().Be(123);
        ironSkinEvent.IronSkinAuraAppliedEvent.Duration.Should().Be(5);
        ironSkinEvent.IronSkinAuraAppliedEvent.Aura.Should().Be(Aura.IronSkin);
    }

    [Fact]
    public void Creates_BarrierAppliedEvent()
    {
        _state.Player.Life = 800;
        var ironSkinEvent = new IronSkinEvent(123);

        _handler.ProcessEvent(ironSkinEvent, _state);

        ironSkinEvent.BarrierAppliedEvent.Should().NotBeNull();
        _state.Events.Should().Contain(ironSkinEvent.BarrierAppliedEvent);
        _state.Events.Should().ContainSingle(e => e is BarrierAppliedEvent);
        ironSkinEvent.BarrierAppliedEvent.Timestamp.Should().Be(123);
        var missingLife = 1200 - 800;
        var expectedBarrierAmount = missingLife * 0.6;
        ironSkinEvent.BarrierAppliedEvent.BarrierAmount.Should().Be(expectedBarrierAmount);
        ironSkinEvent.BarrierAppliedEvent.Duration.Should().Be(5);
    }

    [Fact]
    public void EnhancedIronSkin_Increases_Barrier_By_20_Percent_Max_Life()
    {
        _state.Config.Skills.Add(Skill.EnhancedIronSkin, 1);
        _state.Player.Life = 800;
        var ironSkinEvent = new IronSkinEvent(123);

        _handler.ProcessEvent(ironSkinEvent, _state);

        ironSkinEvent.BarrierAppliedEvent.Should().NotBeNull();
        _state.Events.Should().Contain(ironSkinEvent.BarrierAppliedEvent);
        _state.Events.Should().ContainSingle(e => e is BarrierAppliedEvent);
        ironSkinEvent.BarrierAppliedEvent.Timestamp.Should().Be(123);
        var missingLife = 1200 - 800;
        var expectedBarrierAmount = missingLife * 0.6;
        expectedBarrierAmount += 1200 * 0.2;
        ironSkinEvent.BarrierAppliedEvent.BarrierAmount.Should().Be(expectedBarrierAmount);
        ironSkinEvent.BarrierAppliedEvent.Duration.Should().Be(5);
    }

    

    [Fact]
    public void StrategicIronSkin_Creates_FortifyGeneratedEvent()
    {
        _state.Config.Skills.Add(Skill.StrategicIronSkin, 1);
        _state.Player.Life = 800;
        _state.Player.BaseLife = 700;
        var ironSkinEvent = new IronSkinEvent(123);

        _handler.ProcessEvent(ironSkinEvent, _state);

        ironSkinEvent.FortifyGeneratedEvent.Should().NotBeNull();
        _state.Events.Should().Contain(ironSkinEvent.FortifyGeneratedEvent);
        _state.Events.Should().ContainSingle(e => e is FortifyGeneratedEvent);
        ironSkinEvent.FortifyGeneratedEvent.Timestamp.Should().Be(123);
        ironSkinEvent.FortifyGeneratedEvent.Amount.Should().Be(105);
    }

    [Fact]
    public void StrategicIronSkin_Fortify_Amount_Doubles_When_Below_Half_Life()
    {
        _state.Config.Skills.Add(Skill.StrategicIronSkin, 1);
        _state.Player.Life = 400;
        _state.Player.BaseLife = 700;
        var ironSkinEvent = new IronSkinEvent(123);

        _handler.ProcessEvent(ironSkinEvent, _state);

        ironSkinEvent.FortifyGeneratedEvent.Should().NotBeNull();
        _state.Events.Should().Contain(ironSkinEvent.FortifyGeneratedEvent);
        _state.Events.Should().ContainSingle(e => e is FortifyGeneratedEvent);
        ironSkinEvent.FortifyGeneratedEvent.Timestamp.Should().Be(123);
        ironSkinEvent.FortifyGeneratedEvent.Amount.Should().Be(210);
    }
}
