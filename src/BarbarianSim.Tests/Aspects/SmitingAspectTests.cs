using BarbarianSim.Aspects;
using BarbarianSim.Config;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.Aspects;

public sealed class SmitingAspectTests
{
    private readonly Mock<MaxLifeCalculator> _mockMaxLifeCalculator = TestHelpers.CreateMock<MaxLifeCalculator>();
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly SmitingAspect _aspect;

    public SmitingAspectTests()
    {
        _mockMaxLifeCalculator.Setup(m => m.Calculate(It.IsAny<SimulationState>())).Returns(1200.0);
        _aspect = new SmitingAspect(_mockMaxLifeCalculator.Object) { CritChance = 20, CrowdControlDuration = 40 };
        _state.Config.Gear.Helm.Aspect = _aspect;
    }

    [Fact]
    public void GetCriticalStrikeChanceBonus_When_Active()
    {
        _state.Enemies.First().MaxLife = 1000;
        _state.Enemies.First().Life = 100;

        _aspect.GetCriticalStrikeChanceBonus(_state, _state.Enemies.First()).Should().Be(1.2);
    }

    [Fact]
    public void GetCriticalStrikeChanceBonus_Returns_1_When_Not_Equipped()
    {
        _state.Enemies.First().MaxLife = 1000;
        _state.Enemies.First().Life = 100;
        _state.Config.Gear.Helm.Aspect = null;

        _aspect.GetCriticalStrikeChanceBonus(_state, _state.Enemies.First()).Should().Be(1.0);
    }

    [Fact]
    public void GetCriticalStrikeChanceBonus_Returns_1_When_Enemy_Not_Injured()
    {
        _state.Enemies.First().MaxLife = 1000;
        _state.Enemies.First().Life = 400;

        _aspect.GetCriticalStrikeChanceBonus(_state, _state.Enemies.First()).Should().Be(1.0);
    }

    [Fact]
    public void GetCrowdControlDurationBonus_When_Active()
    {
        _state.Player.Life = 1200;

        _aspect.GetCrowdControlDurationBonus(_state).Should().Be(1.4);
    }

    [Fact]
    public void GetCrowdControlDurationBonus_Returns_1_When_Not_Equipped()
    {
        _state.Player.Life = 1200;
        _state.Config.Gear.Helm.Aspect = null;

        _aspect.GetCrowdControlDurationBonus(_state).Should().Be(1.0);
    }

    [Fact]
    public void GetCrowdControlDurationBonus_Returns_1_When_Not_Healthy()
    {
        _state.Player.Life = 500;

        _aspect.GetCrowdControlDurationBonus(_state).Should().Be(1.0);
    }
}
