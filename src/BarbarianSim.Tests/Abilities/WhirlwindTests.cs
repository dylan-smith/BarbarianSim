using BarbarianSim.Abilities;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.Abilities;

public class WhirlwindTests
{
    private readonly Mock<FuryCostReductionCalculator> _mockFuryCostReductionCalculator = TestHelpers.CreateMock<FuryCostReductionCalculator>();

    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly Whirlwind _whirlwind;

    public WhirlwindTests()
    {
        _mockFuryCostReductionCalculator.Setup(m => m.Calculate(It.IsAny<SimulationState>(), SkillType.Core))
                                        .Returns(1.0);

        _whirlwind = new(_mockFuryCostReductionCalculator.Object);
    }

    [Fact]
    public void CanUse_Returns_True_When_Enough_Fury()
    {
        _state.Player.Fury = 25;

        _whirlwind.CanUse(_state).Should().BeTrue();
    }

    [Fact]
    public void CanUse_When_Weapon_On_Cooldown_Returns_False()
    {
        _state.Player.Fury = 25;
        _state.Player.Auras.Add(Aura.WeaponCooldown);

        _whirlwind.CanUse(_state).Should().BeFalse();
    }

    [Fact]
    public void CanUse_When_Whirlwinding_Aura_Returns_False()
    {
        _state.Player.Fury = 25;
        _state.Player.Auras.Add(Aura.Whirlwinding);

        _whirlwind.CanUse(_state).Should().BeFalse();
    }

    [Fact]
    public void CanUse_When_Not_Enough_Fury_Returns_False()
    {
        _state.Player.Fury = 24;

        _whirlwind.CanUse(_state).Should().BeFalse();
    }

    [Fact]
    public void CanUse_Considers_FuryCostReduction()
    {
        _state.Player.Fury = 20;
        _mockFuryCostReductionCalculator.Setup(m => m.Calculate(It.IsAny<SimulationState>(), SkillType.Core))
                                        .Returns(0.8);

        _whirlwind.CanUse(_state).Should().BeTrue();
    }

    [Fact]
    public void CanRefresh_When_Enough_Fury_And_Whirlwinding_Aura_Applied_Should_Return_True()
    {
        _state.Player.Fury = 25;
        _state.Player.Auras.Add(Aura.Whirlwinding);

        _whirlwind.CanRefresh(_state).Should().BeTrue();
    }

    [Fact]
    public void CanRefresh_When_Not_Enough_Fury_Returns_False()
    {
        _state.Player.Fury = 24;
        _state.Player.Auras.Add(Aura.Whirlwinding);

        _whirlwind.CanRefresh(_state).Should().BeFalse();
    }

    [Fact]
    public void CanRefresh_When_No_Whirlwinding_Aura_Returns_False()
    {
        _state.Player.Fury = 100;

        _whirlwind.CanRefresh(_state).Should().BeFalse();
    }

    [Fact]
    public void CanRefresh_Considers_FuryCostReduction()
    {
        _state.Player.Fury = 20;
        _state.Player.Auras.Add(Aura.Whirlwinding);
        _mockFuryCostReductionCalculator.Setup(m => m.Calculate(It.IsAny<SimulationState>(), SkillType.Core))
                                        .Returns(0.8);


        _whirlwind.CanRefresh(_state).Should().BeTrue();
    }

    [Fact]
    public void Use_Creates_WhirlwindSpinEvent()
    {
        _state.CurrentTime = 123;

        _whirlwind.Use(_state);

        _state.Events.Count.Should().Be(1);
        _state.Events.First().Should().BeOfType<WhirlwindSpinEvent>();
        _state.Events.First().Timestamp.Should().Be(123);
    }

    [Fact]
    public void StopSpinning_Creates_AuraExpiredEvent()
    {
        _state.CurrentTime = 123;

        _whirlwind.StopSpinning(_state);

        _state.Events.Should().ContainSingle(e => e is AuraExpiredEvent);
        _state.Events.OfType<AuraExpiredEvent>().First().Timestamp.Should().Be(123);
        _state.Events.OfType<AuraExpiredEvent>().First().Aura.Should().Be(Aura.Whirlwinding);
    }

    [Theory]
    [InlineData(0, 0.0)]
    [InlineData(1, 0.17)]
    [InlineData(2, 0.19)]
    [InlineData(3, 0.21)]
    [InlineData(4, 0.23)]
    [InlineData(5, 0.24)]
    [InlineData(6, 0.24)]
    public void GetSkillMultiplier_Converts_Skill_Points_To_Correct_Multiplier(int skillPoints, double expectedMultiplier)
    {
        _state.Config.Skills.Add(Skill.Whirlwind, skillPoints);

        var result = _whirlwind.GetSkillMultiplier(_state);

        result.Should().Be(expectedMultiplier);
    }

    [Fact]
    public void GetSkillMultiplier_Includes_Skill_Points_And_Gear_Bonuses()
    {
        _state.Config.Skills.Add(Skill.Whirlwind, 1);

        _state.Config.Gear.Helm.Whirlwind = 2;

        var result = _whirlwind.GetSkillMultiplier(_state);

        result.Should().Be(0.21);
    }
}
