using BarbarianSim.Arsenal;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Arsenal;

public class PolearmExpertiseTests
{
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly PolearmExpertise _expertise = new();

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

        var multiplier = _expertise.GetHealthyDamageMultiplier(_state.Config.Gear.TwoHandSlashing);

        multiplier.Should().Be(1.1);
    }

    [Fact]
    public void GetHealthyDamageMultiplier_When_Technique_Is_Polearm()
    {
        _state.Config.Gear.TwoHandSlashing.Expertise = Expertise.NA;
        _state.Config.PlayerSettings.ExpertiseTechnique = Expertise.Polearm;

        var multiplier = _expertise.GetHealthyDamageMultiplier(_state.Config.Gear.TwoHandSlashing);

        multiplier.Should().Be(1.0);
    }

    [Fact]
    public void GetHealthyDamageMultiplier_Returns_1_When_Not_Active()
    {
        _state.Config.Gear.TwoHandSlashing.Expertise = Expertise.NA;
        _state.Config.PlayerSettings.ExpertiseTechnique = Expertise.NA;

        var multiplier = _expertise.GetHealthyDamageMultiplier(_state.Config.Gear.TwoHandSlashing);

        multiplier.Should().Be(1.0);
    }
}
