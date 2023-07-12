using BarbarianSim.Aspects;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
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

        var result = TotalDamageMultiplierCalculator.Calculate(state, DamageType.Physical, state.Enemies.First(), SkillType.Basic, DamageSource.LungingStrike);

        result.Should().Be(1.0);
    }

    [Fact]
    public void Includes_Additive_Damage()
    {
        var state = new SimulationState(new SimulationConfig());

        BaseStatCalculator.InjectMock(typeof(AdditiveDamageBonusCalculator), new FakeStatCalculator(1.12));

        var result = TotalDamageMultiplierCalculator.Calculate(state, DamageType.Physical, state.Enemies.First(), SkillType.Basic, DamageSource.LungingStrike);

        result.Should().Be(1.12);
    }

    [Fact]
    public void Includes_Vulnerable_Damage()
    {
        var state = new SimulationState(new SimulationConfig());

        BaseStatCalculator.InjectMock(typeof(VulnerableDamageBonusCalculator), new FakeStatCalculator(1.12));

        var result = TotalDamageMultiplierCalculator.Calculate(state, DamageType.Physical, state.Enemies.First(), SkillType.Basic, DamageSource.LungingStrike);

        result.Should().Be(1.12);
    }

    [Fact]
    public void Includes_Strength_Bonus()
    {
        var state = new SimulationState(new SimulationConfig());

        BaseStatCalculator.InjectMock(typeof(StrengthCalculator), new FakeStatCalculator(42.0));

        var result = TotalDamageMultiplierCalculator.Calculate(state, DamageType.Physical, state.Enemies.First(), SkillType.Basic, DamageSource.LungingStrike);

        result.Should().Be(1.042);
    }

    [Fact]
    public void Includes_WarCry_Bonus()
    {
        var state = new SimulationState(new SimulationConfig());

        state.Player.Auras.Add(Aura.WarCry);
        state.Config.Skills.Add(Skill.WarCry, 5);

        var result = TotalDamageMultiplierCalculator.Calculate(state, DamageType.Physical, state.Enemies.First(), SkillType.Basic, DamageSource.LungingStrike);

        result.Should().Be(1.21);
    }

    [Fact]
    public void Includes_UnbridledRage_Bonus()
    {
        var state = new SimulationState(new SimulationConfig());

        state.Config.Skills.Add(Skill.UnbridledRage, 1);

        var result = TotalDamageMultiplierCalculator.Calculate(state, DamageType.Physical, state.Enemies.First(), SkillType.Core, DamageSource.Whirlwind);

        result.Should().Be(2);
    }

    [Fact]
    public void UnbridledRage_Only_Applies_To_Core_Skills()
    {
        var state = new SimulationState(new SimulationConfig());

        state.Config.Skills.Add(Skill.UnbridledRage, 1);

        var result = TotalDamageMultiplierCalculator.Calculate(state, DamageType.Physical, state.Enemies.First(), SkillType.Basic, DamageSource.LungingStrike);

        result.Should().Be(1);
    }

    [Fact]
    public void Includes_PitFighter_Bonus()
    {
        var state = new SimulationState(new SimulationConfig());

        state.Config.Skills.Add(Skill.PitFighter, 1);

        var result = TotalDamageMultiplierCalculator.Calculate(state, DamageType.Physical, state.Enemies.First(), SkillType.Core, DamageSource.Whirlwind);

        result.Should().Be(1.03);
    }

    [Fact]
    public void Includes_SupremeWrathOfTheBerserker_Bonus()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Auras.Add(Aura.Berserking);
        state.Player.Auras.Add(Aura.WrathOfTheBerserker);
        state.Config.Skills.Add(Skill.SupremeWrathOfTheBerserker, 1);
        state.ProcessedEvents.Add(new WrathOfTheBerserkerEvent(123));
        state.ProcessedEvents.Add(new FurySpentEvent(127, 157, SkillType.None) { FurySpent = 157 });

        var result = TotalDamageMultiplierCalculator.Calculate(state, DamageType.Physical, state.Enemies.First(), SkillType.Core, DamageSource.Whirlwind);

        result.Should().Be(1.25 * 1.25 * 1.25);
    }

    [Fact]
    public void Includes_EdgemastersAspect_Bonus()
    {
        var state = new SimulationState(new SimulationConfig());
        MaxFuryCalculator.InjectMock(typeof(MaxFuryCalculator), new FakeStatCalculator(100));
        state.Player.Fury = 80;
        state.Config.Gear.Chest.Aspect = new EdgemastersAspect(20);

        var result = TotalDamageMultiplierCalculator.Calculate(state, DamageType.Physical, state.Enemies.First(), SkillType.Core, DamageSource.Whirlwind);

        result.Should().BeApproximately(1.16, 0.0000001);
    }

    [Fact]
    public void Includes_ViolentWhirlwind_Bonus()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Auras.Add(Aura.ViolentWhirlwind);
        var result = TotalDamageMultiplierCalculator.Calculate(state, DamageType.Physical, state.Enemies.First(), SkillType.Core, DamageSource.Whirlwind);

        result.Should().Be(1.3);
    }

    [Fact]
    public void Includes_EnhancedLungingStrike_Bonus()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.EnhancedLungingStrike, 1);
        state.Enemies.First().MaxLife = 1000;
        state.Enemies.First().Life = 1000;
        var result = TotalDamageMultiplierCalculator.Calculate(state, DamageType.Physical, state.Enemies.First(), SkillType.Basic, DamageSource.LungingStrike);

        result.Should().Be(1.3);
    }

    [Fact]
    public void EnhancedLungingStrike_Bonus_Only_Applies_When_Enemy_Healthy()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.EnhancedLungingStrike, 1);
        state.Enemies.First().MaxLife = 1000;
        state.Enemies.First().Life = 500;
        var result = TotalDamageMultiplierCalculator.Calculate(state, DamageType.Physical, state.Enemies.First(), SkillType.Basic, DamageSource.LungingStrike);

        result.Should().Be(1.0);
    }

    [Fact]
    public void Multiplies_Bonuses_Together()
    {
        var state = new SimulationState(new SimulationConfig());

        state.Player.Auras.Add(Aura.WarCry);
        state.Config.Skills.Add(Skill.WarCry, 5);
        state.Config.Skills.Add(Skill.UnbridledRage, 1);
        state.Config.Skills.Add(Skill.PitFighter, 3);

        BaseStatCalculator.InjectMock(typeof(AdditiveDamageBonusCalculator), new FakeStatCalculator(1.2));
        BaseStatCalculator.InjectMock(typeof(VulnerableDamageBonusCalculator), new FakeStatCalculator(1.2));
        BaseStatCalculator.InjectMock(typeof(StrengthCalculator), new FakeStatCalculator(50.0));

        var result = TotalDamageMultiplierCalculator.Calculate(state, DamageType.Physical, state.Enemies.First(), SkillType.Core, DamageSource.Whirlwind);

        result.Should().BeApproximately(3.9883536, 0.0000001); // 1.2 * 1.2 * 1.05 * 1.09 * 1.21 * 2.0
    }
}
