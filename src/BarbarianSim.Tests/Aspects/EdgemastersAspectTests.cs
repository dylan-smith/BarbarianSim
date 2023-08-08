using BarbarianSim.Aspects;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.Aspects;

public class EdgemastersAspectTests
{
    private readonly Mock<MaxFuryCalculator> _mockMaxFuryCalculator = TestHelpers.CreateMock<MaxFuryCalculator>();
    private readonly Mock<SimLogger> _mockSimLogger = TestHelpers.CreateMock<SimLogger>();
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly EdgemastersAspect _aspect;

    public EdgemastersAspectTests()
    {
        _mockMaxFuryCalculator.Setup(m => m.Calculate(It.IsAny<SimulationState>()))
                              .Returns(100);

        _aspect = new(_mockMaxFuryCalculator.Object, _mockSimLogger.Object);
        _state.Config.Gear.Helm.Aspect = _aspect;
    }

    [Fact]
    public void GetDamageBonus_At_Max_Fury()
    {
        _state.Player.Fury = 100;

        _aspect.Damage = 15;

        var result = _aspect.GetDamageBonus(_state, SkillType.Basic);

        result.Should().Be(1.15);
    }

    [Fact]
    public void GetDamageBonus_At_Zero_Fury()
    {
        _state.Player.Fury = 0;

        _aspect.Damage = 15;

        var result = _aspect.GetDamageBonus(_state, SkillType.Core);

        result.Should().Be(1.0);
    }

    [Fact]
    public void GetDamageBonus_At_Partial_Fury()
    {
        _state.Player.Fury = 30;
        _mockMaxFuryCalculator.Setup(m => m.Calculate(_state)).Returns(50);

        _aspect.Damage = 10;

        var result = _aspect.GetDamageBonus(_state, SkillType.Basic);

        result.Should().Be(1.06);
    }

    [Fact]
    public void GetDamageBonus_Returns_1_For_SkillType_None()
    {
        _state.Player.Fury = 30;

        _aspect.Damage = 10;

        var result = _aspect.GetDamageBonus(_state, SkillType.None);

        result.Should().Be(1.0);
    }

    [Fact]
    public void GetDamageBonus_Returns_1_When_Not_Equipped()
    {
        _state.Player.Fury = 100;
        _state.Config.Gear.Helm.Aspect = null;
        _aspect.Damage = 15;

        var result = _aspect.GetDamageBonus(_state, SkillType.Basic);

        result.Should().Be(1.0);
    }
}
