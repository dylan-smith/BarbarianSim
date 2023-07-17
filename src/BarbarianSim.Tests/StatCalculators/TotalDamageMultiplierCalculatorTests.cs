using BarbarianSim.Abilities;
using BarbarianSim.Aspects;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.Skills;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class TotalDamageMultiplierCalculatorTests
{
    private readonly Mock<AdditiveDamageBonusCalculator> _mockAdditiveDamageBonusCalculator = TestHelpers.CreateMock<AdditiveDamageBonusCalculator>();
    private readonly Mock<VulnerableDamageBonusCalculator> _mockVulnerableDamageBonusCalculator = TestHelpers.CreateMock<VulnerableDamageBonusCalculator>();
    private readonly Mock<StrengthCalculator> _mockStrengthCalculator = TestHelpers.CreateMock<StrengthCalculator>();
    private readonly Mock<PitFighter> _mockPitFighter = TestHelpers.CreateMock<PitFighter>();
    private readonly Mock<WarCry> _mockWarCry = TestHelpers.CreateMock<WarCry>();
    private readonly Mock<WrathOfTheBerserker> _mockWrathOfTheBerserker = TestHelpers.CreateMock<WrathOfTheBerserker>();
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly TotalDamageMultiplierCalculator _calculator;

    public TotalDamageMultiplierCalculatorTests()
    {
        _mockAdditiveDamageBonusCalculator.Setup(m => m.Calculate(It.IsAny<SimulationState>(), It.IsAny<DamageType>(), It.IsAny<EnemyState>())).Returns(1.0);
        _mockVulnerableDamageBonusCalculator.Setup(m => m.Calculate(It.IsAny<SimulationState>(), It.IsAny<EnemyState>())).Returns(1.0);
        _mockStrengthCalculator.Setup(m => m.Calculate(It.IsAny<SimulationState>())).Returns(0.0);
        _mockPitFighter.Setup(m => m.GetCloseDamageBonus(It.IsAny<SimulationState>())).Returns(1.0);
        _mockWarCry.Setup(m => m.GetDamageBonus(It.IsAny<SimulationState>())).Returns(1.0);
        _mockWrathOfTheBerserker.Setup(m => m.GetBerserkDamageBonus(It.IsAny<SimulationState>())).Returns(1.0);

        _calculator = new TotalDamageMultiplierCalculator(_mockAdditiveDamageBonusCalculator.Object,
                                                          _mockVulnerableDamageBonusCalculator.Object,
                                                          _mockStrengthCalculator.Object,
                                                          _mockPitFighter.Object,
                                                          _mockWarCry.Object,
                                                          _mockWrathOfTheBerserker.Object);
    }

    [Fact]
    public void Returns_1_When_No_Extra_Bonuses()
    {
        var result = _calculator.Calculate(_state, DamageType.Physical, _state.Enemies.First(), SkillType.Basic, DamageSource.LungingStrike);

        result.Should().Be(1.0);
    }

    [Fact]
    public void Includes_Additive_Damage()
    {
        _mockAdditiveDamageBonusCalculator.Setup(m => m.Calculate(_state, DamageType.Physical, _state.Enemies.First())).Returns(1.12);

        var result = _calculator.Calculate(_state, DamageType.Physical, _state.Enemies.First(), SkillType.Basic, DamageSource.LungingStrike);

        result.Should().Be(1.12);
    }

    [Fact]
    public void Includes_Vulnerable_Damage()
    {
        _mockVulnerableDamageBonusCalculator.Setup(m => m.Calculate(_state, _state.Enemies.First())).Returns(1.12);

        var result = _calculator.Calculate(_state, DamageType.Physical, _state.Enemies.First(), SkillType.Basic, DamageSource.LungingStrike);

        result.Should().Be(1.12);
    }

    [Fact]
    public void Includes_Strength_Bonus()
    {
        _mockStrengthCalculator.Setup(m => m.Calculate(_state)).Returns(42.0);

        var result = _calculator.Calculate(_state, DamageType.Physical, _state.Enemies.First(), SkillType.Basic, DamageSource.LungingStrike);

        result.Should().Be(1.042);
    }

    [Fact]
    public void Includes_WarCry_Bonus()
    {
        _state.Player.Auras.Add(Aura.WarCry);
        _mockWarCry.Setup(m => m.GetDamageBonus(_state)).Returns(1.21);

        var result = _calculator.Calculate(_state, DamageType.Physical, _state.Enemies.First(), SkillType.Basic, DamageSource.LungingStrike);

        result.Should().Be(1.21);
    }

    [Fact]
    public void Includes_UnbridledRage_Bonus()
    {
        _state.Config.Skills.Add(Skill.UnbridledRage, 1);

        var result = _calculator.Calculate(_state, DamageType.Physical, _state.Enemies.First(), SkillType.Core, DamageSource.Whirlwind);

        result.Should().Be(2);
    }

    [Fact]
    public void UnbridledRage_Only_Applies_To_Core_Skills()
    {
        _state.Config.Skills.Add(Skill.UnbridledRage, 1);

        var result = _calculator.Calculate(_state, DamageType.Physical, _state.Enemies.First(), SkillType.Basic, DamageSource.LungingStrike);

        result.Should().Be(1);
    }

    [Fact]
    public void Includes_PitFighter_Bonus()
    {
        _mockPitFighter.Setup(m => m.GetCloseDamageBonus(_state)).Returns(1.03);

        var result = _calculator.Calculate(_state, DamageType.Physical, _state.Enemies.First(), SkillType.Core, DamageSource.Whirlwind);

        result.Should().Be(1.03);
    }

    [Fact]
    public void Includes_WrathOfTheBerserker_Bonus()
    {
        _mockWrathOfTheBerserker.Setup(m => m.GetBerserkDamageBonus(_state)).Returns(1.25);

        var result = _calculator.Calculate(_state, DamageType.Physical, _state.Enemies.First(), SkillType.Core, DamageSource.Whirlwind);

        result.Should().Be(1.25);
    }

    [Fact]
    public void Includes_EdgemastersAspect_Bonus()
    {
        var mockEdgemastersAspect = TestHelpers.CreateMock<EdgemastersAspect>();
        mockEdgemastersAspect.Setup(m => m.GetDamageBonus(_state, SkillType.Core)).Returns(1.2);
        _state.Config.Gear.Chest.Aspect = mockEdgemastersAspect.Object;

        var result = _calculator.Calculate(_state, DamageType.Physical, _state.Enemies.First(), SkillType.Core, DamageSource.Whirlwind);

        result.Should().Be(1.2);
    }

    [Fact]
    public void Includes_ViolentWhirlwind_Bonus()
    {
        _state.Player.Auras.Add(Aura.ViolentWhirlwind);
        var result = _calculator.Calculate(_state, DamageType.Physical, _state.Enemies.First(), SkillType.Core, DamageSource.Whirlwind);

        result.Should().Be(1.3);
    }

    [Fact]
    public void Includes_EnhancedLungingStrike_Bonus()
    {
        _state.Config.Skills.Add(Skill.EnhancedLungingStrike, 1);
        _state.Enemies.First().MaxLife = 1000;
        _state.Enemies.First().Life = 1000;
        var result = _calculator.Calculate(_state, DamageType.Physical, _state.Enemies.First(), SkillType.Basic, DamageSource.LungingStrike);

        result.Should().Be(1.3);
    }

    [Fact]
    public void EnhancedLungingStrike_Bonus_Only_Applies_When_Enemy_Healthy()
    {
        _state.Config.Skills.Add(Skill.EnhancedLungingStrike, 1);
        _state.Enemies.First().MaxLife = 1000;
        _state.Enemies.First().Life = 500;
        var result = _calculator.Calculate(_state, DamageType.Physical, _state.Enemies.First(), SkillType.Basic, DamageSource.LungingStrike);

        result.Should().Be(1.0);
    }

    [Fact]
    public void Multiplies_Bonuses_Together()
    {
        _state.Player.Auras.Add(Aura.WarCry);
        _mockWarCry.Setup(m => m.GetDamageBonus(_state)).Returns(1.21);
        _state.Config.Skills.Add(Skill.UnbridledRage, 1);
        _mockPitFighter.Setup(m => m.GetCloseDamageBonus(_state)).Returns(1.09);

        _mockAdditiveDamageBonusCalculator.Setup(m => m.Calculate(_state, DamageType.Physical, _state.Enemies.First())).Returns(1.2);
        _mockVulnerableDamageBonusCalculator.Setup(m => m.Calculate(_state, _state.Enemies.First())).Returns(1.2);
        _mockStrengthCalculator.Setup(m => m.Calculate(_state)).Returns(50.0);

        var result = _calculator.Calculate(_state, DamageType.Physical, _state.Enemies.First(), SkillType.Core, DamageSource.Whirlwind);

        result.Should().BeApproximately(3.9883536, 0.0000001); // 1.2 * 1.2 * 1.05 * 1.09 * 1.21 * 2.0
    }
}
