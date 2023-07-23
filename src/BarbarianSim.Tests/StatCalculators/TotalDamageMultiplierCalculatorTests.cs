using BarbarianSim.Abilities;
using BarbarianSim.Aspects;
using BarbarianSim.Config;
using BarbarianSim.Enums;
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
    private readonly Mock<SupremeWrathOfTheBerserker> _mockSupremeWrathOfTheBerserker = TestHelpers.CreateMock<SupremeWrathOfTheBerserker>();
    private readonly Mock<UnbridledRage> _mockUnbridledRage = TestHelpers.CreateMock<UnbridledRage>();
    private readonly Mock<ViolentWhirlwind> _mockViolentWhirlwind = TestHelpers.CreateMock<ViolentWhirlwind>();
    private readonly Mock<EnhancedLungingStrike> _mockEnhancedLungingStrike = TestHelpers.CreateMock<EnhancedLungingStrike>();
    private readonly Mock<EdgemastersAspect> _mockEdgemastersAspect = TestHelpers.CreateMock<EdgemastersAspect>();
    private readonly Mock<AspectOfLimitlessRage> _mockAspectOfLimitlessRage = TestHelpers.CreateMock<AspectOfLimitlessRage>();
    private readonly Mock<ConceitedAspect> _mockConceitedAspect = TestHelpers.CreateMock<ConceitedAspect>();
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly TotalDamageMultiplierCalculator _calculator;

    public TotalDamageMultiplierCalculatorTests()
    {
        _mockAdditiveDamageBonusCalculator.Setup(m => m.Calculate(It.IsAny<SimulationState>(), It.IsAny<DamageType>(), It.IsAny<EnemyState>())).Returns(1.0);
        _mockVulnerableDamageBonusCalculator.Setup(m => m.Calculate(It.IsAny<SimulationState>(), It.IsAny<EnemyState>())).Returns(1.0);
        _mockStrengthCalculator.Setup(m => m.Calculate(It.IsAny<SimulationState>())).Returns(0.0);
        _mockPitFighter.Setup(m => m.GetCloseDamageBonus(It.IsAny<SimulationState>())).Returns(1.0);
        _mockWarCry.Setup(m => m.GetDamageBonus(It.IsAny<SimulationState>())).Returns(1.0);
        _mockSupremeWrathOfTheBerserker.Setup(m => m.GetBerserkDamageBonus(It.IsAny<SimulationState>())).Returns(1.0);
        _mockUnbridledRage.Setup(m => m.GetDamageBonus(It.IsAny<SimulationState>(), It.IsAny<SkillType>())).Returns(1.0);
        _mockViolentWhirlwind.Setup(m => m.GetDamageBonus(It.IsAny<SimulationState>(), It.IsAny<DamageSource>())).Returns(1.0);
        _mockEnhancedLungingStrike.Setup(m => m.GetDamageBonus(It.IsAny<SimulationState>(), It.IsAny<DamageSource>(), It.IsAny<EnemyState>())).Returns(1.0);
        _mockEdgemastersAspect.Setup(m => m.GetDamageBonus(It.IsAny<SimulationState>(), It.IsAny<SkillType>())).Returns(1.0);
        _mockAspectOfLimitlessRage.Setup(m => m.GetDamageBonus(It.IsAny<SimulationState>(), It.IsAny<SkillType>())).Returns(1.0);
        _mockConceitedAspect.Setup(m => m.GetDamageBonus(It.IsAny<SimulationState>())).Returns(1.0);

        _calculator = new TotalDamageMultiplierCalculator(_mockAdditiveDamageBonusCalculator.Object,
                                                          _mockVulnerableDamageBonusCalculator.Object,
                                                          _mockStrengthCalculator.Object,
                                                          _mockPitFighter.Object,
                                                          _mockWarCry.Object,
                                                          _mockSupremeWrathOfTheBerserker.Object,
                                                          _mockUnbridledRage.Object,
                                                          _mockViolentWhirlwind.Object,
                                                          _mockEnhancedLungingStrike.Object,
                                                          _mockEdgemastersAspect.Object,
                                                          _mockAspectOfLimitlessRage.Object,
                                                          _mockConceitedAspect.Object);
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
        _mockWarCry.Setup(m => m.GetDamageBonus(_state)).Returns(1.21);

        var result = _calculator.Calculate(_state, DamageType.Physical, _state.Enemies.First(), SkillType.Basic, DamageSource.LungingStrike);

        result.Should().Be(1.21);
    }

    [Fact]
    public void Includes_UnbridledRage_Bonus()
    {
        _mockUnbridledRage.Setup(m => m.GetDamageBonus(_state, SkillType.Core)).Returns(2.0);

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
        _mockSupremeWrathOfTheBerserker.Setup(m => m.GetBerserkDamageBonus(_state)).Returns(1.25);

        var result = _calculator.Calculate(_state, DamageType.Physical, _state.Enemies.First(), SkillType.Core, DamageSource.Whirlwind);

        result.Should().Be(1.25);
    }

    [Fact]
    public void Includes_EdgemastersAspect_Bonus()
    {
        _mockEdgemastersAspect.Setup(m => m.GetDamageBonus(_state, SkillType.Core)).Returns(1.2);
        _state.Config.Gear.Chest.Aspect = _mockEdgemastersAspect.Object;

        var result = _calculator.Calculate(_state, DamageType.Physical, _state.Enemies.First(), SkillType.Core, DamageSource.Whirlwind);

        result.Should().Be(1.2);
    }

    [Fact]
    public void Includes_ViolentWhirlwind_Bonus()
    {
        _mockViolentWhirlwind.Setup(m => m.GetDamageBonus(_state, DamageSource.Whirlwind)).Returns(1.3);
        var result = _calculator.Calculate(_state, DamageType.Physical, _state.Enemies.First(), SkillType.Core, DamageSource.Whirlwind);

        result.Should().Be(1.3);
    }

    [Fact]
    public void Includes_EnhancedLungingStrike_Bonus()
    {
        _mockEnhancedLungingStrike.Setup(m => m.GetDamageBonus(_state, DamageSource.LungingStrike, _state.Enemies.First())).Returns(1.3);
        var result = _calculator.Calculate(_state, DamageType.Physical, _state.Enemies.First(), SkillType.Basic, DamageSource.LungingStrike);

        result.Should().Be(1.3);
    }

    [Fact]
    public void Includes_AspectOfLimitlessRage_Bonus()
    {
        _mockAspectOfLimitlessRage.Setup(m => m.GetDamageBonus(_state, SkillType.Core)).Returns(1.2);
        _state.Config.Gear.Chest.Aspect = _mockAspectOfLimitlessRage.Object;

        var result = _calculator.Calculate(_state, DamageType.Physical, _state.Enemies.First(), SkillType.Core, DamageSource.Whirlwind);

        result.Should().Be(1.2);
    }

    [Fact]
    public void Includes_ConceitedAspect_Bonus()
    {
        _mockConceitedAspect.Setup(m => m.GetDamageBonus(_state)).Returns(1.25);
        _state.Config.Gear.Chest.Aspect = _mockConceitedAspect.Object;

        var result = _calculator.Calculate(_state, DamageType.Physical, _state.Enemies.First(), SkillType.Core, DamageSource.Whirlwind);

        result.Should().Be(1.25);
    }

    [Fact]
    public void Multiplies_Bonuses_Together()
    {
        _mockWarCry.Setup(m => m.GetDamageBonus(_state)).Returns(1.21);
        _mockUnbridledRage.Setup(m => m.GetDamageBonus(_state, SkillType.Core)).Returns(2.0);
        _mockPitFighter.Setup(m => m.GetCloseDamageBonus(_state)).Returns(1.09);

        _mockAdditiveDamageBonusCalculator.Setup(m => m.Calculate(_state, DamageType.Physical, _state.Enemies.First())).Returns(1.2);
        _mockVulnerableDamageBonusCalculator.Setup(m => m.Calculate(_state, _state.Enemies.First())).Returns(1.2);
        _mockStrengthCalculator.Setup(m => m.Calculate(_state)).Returns(50.0);

        var result = _calculator.Calculate(_state, DamageType.Physical, _state.Enemies.First(), SkillType.Core, DamageSource.Whirlwind);

        result.Should().BeApproximately(3.9883536, 0.0000001); // 1.2 * 1.2 * 1.05 * 1.09 * 1.21 * 2.0
    }
}
