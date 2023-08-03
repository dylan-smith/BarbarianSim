using BarbarianSim.Arsenal;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Arsenal;

public class TwoHandedAxeExpertiseTests
{
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly TwoHandedAxeExpertise _expertise = new();

    [Fact]
    public void GetVulnerableDamageMultiplier_Returns_1_When_Not_Active()
    {
        _state.Config.Gear.TwoHandSlashing.Expertise = Expertise.TwoHandedSword;
        _state.Config.PlayerSettings.ExpertiseTechnique = Expertise.TwoHandedMace;

        var multiplier = _expertise.GetVulnerableDamageMultiplier(_state, _state.Config.Gear.TwoHandSlashing);

        multiplier.Should().Be(1.0);
    }

    [Fact]
    public void GetVulnerableDamageMultiplier_Returns_15_Percent_When_Using_Axe()
    {
        _state.Config.Gear.TwoHandSlashing.Expertise = Expertise.TwoHandedAxe;
        _state.Config.PlayerSettings.ExpertiseTechnique = Expertise.TwoHandedMace;

        var multiplier = _expertise.GetVulnerableDamageMultiplier(_state, _state.Config.Gear.TwoHandSlashing);

        multiplier.Should().Be(1.15);
    }

    [Fact]
    public void GetVulnerableDamageMultiplier_Returns_15_Percent_When_Technique_Set_To_Axe()
    {
        _state.Config.Gear.TwoHandSlashing.Expertise = Expertise.TwoHandedSword;
        _state.Config.PlayerSettings.ExpertiseTechnique = Expertise.TwoHandedAxe;

        var multiplier = _expertise.GetVulnerableDamageMultiplier(_state, _state.Config.Gear.TwoHandSlashing);

        multiplier.Should().Be(1.15);
    }

    [Fact]
    public void GetVulnerableDamageMultiplier_Returns_15_Percent_When_Using_Axe_And_Technique_Set_To_Axe()
    {
        _state.Config.Gear.TwoHandSlashing.Expertise = Expertise.TwoHandedAxe;
        _state.Config.PlayerSettings.ExpertiseTechnique = Expertise.TwoHandedAxe;

        var multiplier = _expertise.GetVulnerableDamageMultiplier(_state, _state.Config.Gear.TwoHandSlashing);

        multiplier.Should().Be(1.15);
    }

    [Fact]
    public void GetVulnerableDamageMultiplier_Returns_15_Percent_When_Technique_Set_To_Axe_And_Weapon_Is_Null()
    {
        _state.Config.PlayerSettings.ExpertiseTechnique = Expertise.TwoHandedAxe;

        var multiplier = _expertise.GetVulnerableDamageMultiplier(_state, null);

        multiplier.Should().Be(1.15);
    }

    [Fact]
    public void GetCritChanceVulnerable_Returns_0_When_Not_Active()
    {
        _state.Config.Gear.TwoHandSlashing.Expertise = Expertise.TwoHandedSword;
        _state.Config.PlayerSettings.ExpertiseTechnique = Expertise.TwoHandedMace;

        var critChance = _expertise.GetCritChanceVulnerable(_state, _state.Config.Gear.TwoHandSlashing);

        critChance.Should().Be(0);
    }

    [Fact]
    public void GetCritChanceVulnerable_Returns_10_Percent_When_Using_Axe()
    {
        _state.Config.Gear.TwoHandSlashing.Expertise = Expertise.TwoHandedAxe;
        _state.Config.PlayerSettings.ExpertiseTechnique = Expertise.TwoHandedMace;

        var critChance = _expertise.GetCritChanceVulnerable(_state, _state.Config.Gear.TwoHandSlashing);

        critChance.Should().Be(10);
    }

    [Fact]
    public void GetCritChanceVulnerable_Returns_0_When_Technique_Set_To_Axe_But_Weapon_Is_Sword()
    {
        _state.Config.Gear.TwoHandSlashing.Expertise = Expertise.TwoHandedSword;
        _state.Config.PlayerSettings.ExpertiseTechnique = Expertise.TwoHandedAxe;

        var critChance = _expertise.GetCritChanceVulnerable(_state, _state.Config.Gear.TwoHandSlashing);

        critChance.Should().Be(0);
    }

    [Fact]
    public void GetCritChanceVulnerable_Returns_0_Weapon_Is_Null()
    {
        _state.Config.PlayerSettings.ExpertiseTechnique = Expertise.TwoHandedAxe;

        var critChance = _expertise.GetCritChanceVulnerable(_state, null);

        critChance.Should().Be(0);
    }
}
