using BarbarianSim.Arsenal;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.Arsenal;

public class PolearmExpertiseTests
{
    private readonly Mock<MaxLifeCalculator> _mockMaxLifeCalculator = TestHelpers.CreateMock<MaxLifeCalculator>();
    private readonly Mock<SimLogger> _mockSimLogger = TestHelpers.CreateMock<SimLogger>();
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly PolearmExpertise _expertise;

    public PolearmExpertiseTests()
    {
        _mockMaxLifeCalculator.Setup(m => m.Calculate(_state)).Returns(1200.0);
        _expertise = new(_mockMaxLifeCalculator.Object, _mockSimLogger.Object);
    }

    [Fact]
    public void GetLuckyHitChanceMultiplier_When_Weapon_Is_Polearm()
    {
        _state.Config.Gear.TwoHandSlashing.Expertise = Expertise.Polearm;
        _state.Config.PlayerSettings.ExpertiseTechnique = Expertise.NA;

        var multiplier = _expertise.GetLuckyHitChanceMultiplier(_state, _state.Config.Gear.TwoHandSlashing);

        multiplier.Should().Be(1.1);
    }

    [Fact]
    public void GetLuckyHitChanceMultiplier_When_Technique_Is_Polearm()
    {
        _state.Config.Gear.TwoHandSlashing.Expertise = Expertise.NA;
        _state.Config.PlayerSettings.ExpertiseTechnique = Expertise.Polearm;

        var multiplier = _expertise.GetLuckyHitChanceMultiplier(_state, _state.Config.Gear.TwoHandSlashing);

        multiplier.Should().Be(1.1);
    }

    [Fact]
    public void GetLuckyHitChanceMultiplier_Returns_1_When_Not_Active()
    {
        _state.Config.Gear.TwoHandSlashing.Expertise = Expertise.NA;
        _state.Config.PlayerSettings.ExpertiseTechnique = Expertise.NA;

        var multiplier = _expertise.GetLuckyHitChanceMultiplier(_state, _state.Config.Gear.TwoHandSlashing);

        multiplier.Should().Be(1.0);
    }

    [Fact]
    public void GetHealthyDamageMultiplier_When_Weapon_Is_Polearm()
    {
        _state.Config.Gear.TwoHandSlashing.Expertise = Expertise.Polearm;
        _state.Config.PlayerSettings.ExpertiseTechnique = Expertise.NA;
        _state.Player.Life = 1200;

        var multiplier = _expertise.GetHealthyDamageMultiplier(_state, _state.Config.Gear.TwoHandSlashing);

        multiplier.Should().Be(1.1);
    }

    [Fact]
    public void GetHealthyDamageMultiplier_When_Technique_Is_Polearm()
    {
        _state.Config.Gear.TwoHandSlashing.Expertise = Expertise.NA;
        _state.Config.PlayerSettings.ExpertiseTechnique = Expertise.Polearm;

        var multiplier = _expertise.GetHealthyDamageMultiplier(_state, _state.Config.Gear.TwoHandSlashing);

        multiplier.Should().Be(1.0);
    }

    [Fact]
    public void GetHealthyDamageMultiplier_Returns_1_When_Not_Active()
    {
        _state.Config.Gear.TwoHandSlashing.Expertise = Expertise.NA;
        _state.Config.PlayerSettings.ExpertiseTechnique = Expertise.NA;

        var multiplier = _expertise.GetHealthyDamageMultiplier(_state, _state.Config.Gear.TwoHandSlashing);

        multiplier.Should().Be(1.0);
    }
}
