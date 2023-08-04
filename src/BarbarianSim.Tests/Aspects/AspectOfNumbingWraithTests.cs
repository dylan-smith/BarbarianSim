using BarbarianSim.Aspects;
using BarbarianSim.Config;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.Aspects;

public sealed class AspectOfNumbingWraithTests
{
    private readonly Mock<MaxLifeCalculator> _mockMaxLifeCalculator = TestHelpers.CreateMock<MaxLifeCalculator>();
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly AspectOfNumbingWraith _aspect;

    public AspectOfNumbingWraithTests()
    {
        _mockMaxLifeCalculator.Setup(m => m.Calculate(It.IsAny<SimulationState>())).Returns(1200);
        _state.Player.Fortify = 0;
        _aspect = new AspectOfNumbingWraith(_mockMaxLifeCalculator.Object) { Fortify = 54 };
        _state.Config.Gear.Helm.Aspect = _aspect;
    }

    [Fact]
    public void Min_Fortify_Gain_After_1_Extra_FuryGenerated()
    {
        var furyGeneratedEvent = new FuryGeneratedEvent(123, null, 20) { OverflowFury = 1 };

        _aspect.ProcessEvent(furyGeneratedEvent, _state);
        _state.Player.Fortify.Should().Be(54);
    }

    [Fact]
    public void Fortify_Matches_MaxLife_After_Lots_Of_FuryGenerated()
    {
        _state.Player.Fortify = 500;
        var furyGeneratedEvent = new FuryGeneratedEvent(123, null, 20) { OverflowFury = 20 };
        _aspect.ProcessEvent(furyGeneratedEvent, _state);

        _state.Player.Fortify.Should().Be(1200);
    }

    [Fact]
    public void Does_Nothing_When_Not_Equipped()
    {
        _state.Config.Gear.Helm.Aspect = null;
        var furyGeneratedEvent = new FuryGeneratedEvent(123, null, 20) { OverflowFury = 10 };

        _aspect.ProcessEvent(furyGeneratedEvent, _state);
        _state.Player.Fortify.Should().Be(0);
    }
}
