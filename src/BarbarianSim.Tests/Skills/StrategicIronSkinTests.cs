using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.Skills;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.Skills;

public class StrategicIronSkinTests
{
    private readonly Mock<MaxLifeCalculator> _maxLifeCalculator = TestHelpers.CreateMock<MaxLifeCalculator>();
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly StrategicIronSkin _skill;

    public StrategicIronSkinTests()
    {
        _maxLifeCalculator.Setup(m => m.Calculate(It.IsAny<SimulationState>())).Returns(1200);
        _skill = new(_maxLifeCalculator.Object);
    }

    [Fact]
    public void StrategicIronSkin_Creates_FortifyGeneratedEvent()
    {
        _state.Config.Skills.Add(Skill.StrategicIronSkin, 1);
        _state.Player.Life = 800;
        _state.Player.BaseLife = 700;
        var ironSkinEvent = new IronSkinEvent(123);

        _skill.ProcessEvent(ironSkinEvent, _state);

        _state.Events.Should().ContainSingle(e => e is FortifyGeneratedEvent);
        _state.Events.OfType<FortifyGeneratedEvent>().Single().Timestamp.Should().Be(123);
        _state.Events.OfType<FortifyGeneratedEvent>().Single().Amount.Should().Be(105);
    }

    [Fact]
    public void StrategicIronSkin_Fortify_Amount_Doubles_When_Below_Half_Life()
    {
        _state.Config.Skills.Add(Skill.StrategicIronSkin, 1);
        _state.Player.Life = 400;
        _state.Player.BaseLife = 700;
        var ironSkinEvent = new IronSkinEvent(123);

        _skill.ProcessEvent(ironSkinEvent, _state);

        _state.Events.Should().ContainSingle(e => e is FortifyGeneratedEvent);
        _state.Events.OfType<FortifyGeneratedEvent>().Single().Timestamp.Should().Be(123);
        _state.Events.OfType<FortifyGeneratedEvent>().Single().Amount.Should().Be(210);
    }
}
