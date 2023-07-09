using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public sealed class TotalDamageMultiplierCalculatorTests : IDisposable
{
    public void Dispose() => BaseStatCalculator.ClearMocks();

    public TotalDamageMultiplierCalculatorTests()
    {
        BaseStatCalculator.InjectMock(typeof(AdditiveDamageBonusCalculator), new FakeStatCalculator(1.0));
        BaseStatCalculator.InjectMock(typeof(VulnerableDamageBonusCalculator), new FakeStatCalculator(1.0));
        BaseStatCalculator.InjectMock(typeof(StrengthCalculator), new FakeStatCalculator(0.0));
    }

    [Fact]
    public void Returns_1_When_No_Extra_Bonuses()
    {
        var state = new SimulationState(new SimulationConfig());

        var result = TotalDamageMultiplierCalculator.Calculate(state, DamageType.Physical, state.Enemies.First(), SkillType.Basic);

        result.Should().Be(1.0);
    }

    [Fact]
    public void Includes_Additive_Damage()
    {
        var state = new SimulationState(new SimulationConfig());

        BaseStatCalculator.InjectMock(typeof(AdditiveDamageBonusCalculator), new FakeStatCalculator(1.12));

        var result = TotalDamageMultiplierCalculator.Calculate(state, DamageType.Physical, state.Enemies.First(), SkillType.Basic);

        result.Should().Be(1.12);
    }

    [Fact]
    public void Includes_Vulnerable_Damage()
    {
        var state = new SimulationState(new SimulationConfig());

        BaseStatCalculator.InjectMock(typeof(VulnerableDamageBonusCalculator), new FakeStatCalculator(1.12));

        var result = TotalDamageMultiplierCalculator.Calculate(state, DamageType.Physical, state.Enemies.First(), SkillType.Basic);

        result.Should().Be(1.12);
    }

    [Fact]
    public void Includes_Strength_Bonus()
    {
        var state = new SimulationState(new SimulationConfig());

        BaseStatCalculator.InjectMock(typeof(StrengthCalculator), new FakeStatCalculator(42.0));

        var result = TotalDamageMultiplierCalculator.Calculate(state, DamageType.Physical, state.Enemies.First(), SkillType.Basic);

        result.Should().Be(1.042);
    }

    [Fact]
    public void Includes_WarCry_Bonus()
    {
        var state = new SimulationState(new SimulationConfig());

        state.Player.Auras.Add(Aura.WarCry);
        state.Config.Skills.Add(Skill.WarCry, 5);

        var result = TotalDamageMultiplierCalculator.Calculate(state, DamageType.Physical, state.Enemies.First(), SkillType.Basic);

        result.Should().Be(1.21);
    }

    [Fact]
    public void Includes_UnbridledRage_Bonus()
    {
        var state = new SimulationState(new SimulationConfig());

        state.Config.Skills.Add(Skill.UnbridledRage, 1);

        var result = TotalDamageMultiplierCalculator.Calculate(state, DamageType.Physical, state.Enemies.First(), SkillType.Core);

        result.Should().Be(2);
    }

    [Fact]
    public void UnbridledRage_Only_Applies_To_Core_Skills()
    {
        var state = new SimulationState(new SimulationConfig());

        state.Config.Skills.Add(Skill.UnbridledRage, 1);

        var result = TotalDamageMultiplierCalculator.Calculate(state, DamageType.Physical, state.Enemies.First(), SkillType.Basic);

        result.Should().Be(1);
    }

    [Fact]
    public void Includes_PitFighter_Bonus()
    {
        var state = new SimulationState(new SimulationConfig());

        state.Config.Skills.Add(Skill.PitFighter, 1);

        var result = TotalDamageMultiplierCalculator.Calculate(state, DamageType.Physical, state.Enemies.First(), SkillType.Core);

        result.Should().Be(1.03);
    }

    [Fact]
    public void Multiplies_All_Bonuses_Together()
    {
        var state = new SimulationState(new SimulationConfig());

        state.Player.Auras.Add(Aura.WarCry);
        state.Config.Skills.Add(Skill.WarCry, 5);
        state.Config.Skills.Add(Skill.UnbridledRage, 1);
        state.Config.Skills.Add(Skill.PitFighter, 3);

        BaseStatCalculator.InjectMock(typeof(AdditiveDamageBonusCalculator), new FakeStatCalculator(1.2));
        BaseStatCalculator.InjectMock(typeof(VulnerableDamageBonusCalculator), new FakeStatCalculator(1.2));
        BaseStatCalculator.InjectMock(typeof(StrengthCalculator), new FakeStatCalculator(50.0));

        var result = TotalDamageMultiplierCalculator.Calculate(state, DamageType.Physical, state.Enemies.First(), SkillType.Core);

        result.Should().BeApproximately(3.9883536, 0.0000001); // 1.2 * 1.2 * 1.05 * 1.09 * 1.21 * 2.0
    }
}
