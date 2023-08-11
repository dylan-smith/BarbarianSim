using BarbarianSim.Abilities;
using BarbarianSim.Arsenal;
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
    private readonly Mock<TwoHandedWeaponDamageMultiplicativeCalculator> _mockTwoHandedWeaponDamageMultiplicativeCalculator = TestHelpers.CreateMock<TwoHandedWeaponDamageMultiplicativeCalculator>();
    private readonly Mock<VulnerableDamageBonusCalculator> _mockVulnerableDamageBonusCalculator = TestHelpers.CreateMock<VulnerableDamageBonusCalculator>();
    private readonly Mock<StrengthCalculator> _mockStrengthCalculator = TestHelpers.CreateMock<StrengthCalculator>();
    private readonly Mock<PitFighter> _mockPitFighter = TestHelpers.CreateMock<PitFighter>();
    private readonly Mock<WarCry> _mockWarCry = TestHelpers.CreateMock<WarCry>();
    private readonly Mock<PowerWarCry> _mockPowerWarCry = TestHelpers.CreateMock<PowerWarCry>();
    private readonly Mock<SupremeWrathOfTheBerserker> _mockSupremeWrathOfTheBerserker = TestHelpers.CreateMock<SupremeWrathOfTheBerserker>();
    private readonly Mock<UnbridledRage> _mockUnbridledRage = TestHelpers.CreateMock<UnbridledRage>();
    private readonly Mock<ViolentWhirlwind> _mockViolentWhirlwind = TestHelpers.CreateMock<ViolentWhirlwind>();
    private readonly Mock<EnhancedLungingStrike> _mockEnhancedLungingStrike = TestHelpers.CreateMock<EnhancedLungingStrike>();
    private readonly Mock<EdgemastersAspect> _mockEdgemastersAspect = TestHelpers.CreateMock<EdgemastersAspect>();
    private readonly Mock<AspectOfLimitlessRage> _mockAspectOfLimitlessRage = TestHelpers.CreateMock<AspectOfLimitlessRage>();
    private readonly Mock<ConceitedAspect> _mockConceitedAspect = TestHelpers.CreateMock<ConceitedAspect>();
    private readonly Mock<AspectOfTheExpectant> _mockAspectOfTheExpectant = TestHelpers.CreateMock<AspectOfTheExpectant>();
    private readonly Mock<ExploitersAspect> _mockExploitersAspect = TestHelpers.CreateMock<ExploitersAspect>();
    private readonly Mock<PenitentGreaves> _mockPenitentGreaves = TestHelpers.CreateMock<PenitentGreaves>();
    private readonly Mock<RamaladnisMagnumOpus> _mockRamaladnisMagnumOpus = TestHelpers.CreateMock<RamaladnisMagnumOpus>();
    private readonly Mock<BerserkingDamageCalculator> _mockBerserkingDamageCalculator = TestHelpers.CreateMock<BerserkingDamageCalculator>();
    private readonly Mock<PolearmExpertise> _mockPolearmExpertise = TestHelpers.CreateMock<PolearmExpertise>();
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly TotalDamageMultiplierCalculator _calculator;

    public TotalDamageMultiplierCalculatorTests()
    {
        _mockAdditiveDamageBonusCalculator.Setup(m => m.Calculate(It.IsAny<SimulationState>(), It.IsAny<DamageType>(), It.IsAny<EnemyState>())).Returns(1.0);
        _mockTwoHandedWeaponDamageMultiplicativeCalculator.Setup(m => m.Calculate(It.IsAny<SimulationState>(), It.IsAny<GearItem>())).Returns(1.0);
        _mockVulnerableDamageBonusCalculator.Setup(m => m.Calculate(It.IsAny<SimulationState>(), It.IsAny<EnemyState>(), It.IsAny<GearItem>())).Returns(1.0);
        _mockStrengthCalculator.Setup(m => m.GetDamageMultiplier(It.IsAny<SimulationState>(), It.IsAny<SkillType>())).Returns(1.0);
        _mockPitFighter.Setup(m => m.GetCloseDamageBonus(It.IsAny<SimulationState>())).Returns(1.0);
        _mockWarCry.Setup(m => m.GetDamageBonus(It.IsAny<SimulationState>())).Returns(1.0);
        _mockPowerWarCry.Setup(m => m.GetDamageBonus(It.IsAny<SimulationState>())).Returns(1.0);
        _mockSupremeWrathOfTheBerserker.Setup(m => m.GetBerserkDamageBonus(It.IsAny<SimulationState>())).Returns(1.0);
        _mockUnbridledRage.Setup(m => m.GetDamageBonus(It.IsAny<SimulationState>(), It.IsAny<SkillType>())).Returns(1.0);
        _mockViolentWhirlwind.Setup(m => m.GetDamageBonus(It.IsAny<SimulationState>(), It.IsAny<DamageSource>())).Returns(1.0);
        _mockEnhancedLungingStrike.Setup(m => m.GetDamageBonus(It.IsAny<SimulationState>(), It.IsAny<DamageSource>(), It.IsAny<EnemyState>())).Returns(1.0);
        _mockEdgemastersAspect.Setup(m => m.GetDamageBonus(It.IsAny<SimulationState>(), It.IsAny<SkillType>())).Returns(1.0);
        _mockAspectOfLimitlessRage.Setup(m => m.GetDamageBonus(It.IsAny<SimulationState>(), It.IsAny<SkillType>())).Returns(1.0);
        _mockConceitedAspect.Setup(m => m.GetDamageBonus(It.IsAny<SimulationState>())).Returns(1.0);
        _mockAspectOfTheExpectant.Setup(m => m.GetDamageBonus(It.IsAny<SimulationState>(), It.IsAny<SkillType>())).Returns(1.0);
        _mockExploitersAspect.Setup(m => m.GetDamageBonus(It.IsAny<SimulationState>(), It.IsAny<EnemyState>())).Returns(1.0);
        _mockPenitentGreaves.Setup(m => m.GetDamageBonus(It.IsAny<SimulationState>())).Returns(1.0);
        _mockRamaladnisMagnumOpus.Setup(m => m.GetDamageBonus(It.IsAny<SimulationState>(), It.IsAny<GearItem>())).Returns(1.0);
        _mockBerserkingDamageCalculator.Setup(m => m.Calculate(It.IsAny<SimulationState>())).Returns(1.0);
        _mockPolearmExpertise.Setup(m => m.GetHealthyDamageMultiplier(It.IsAny<SimulationState>(), It.IsAny<GearItem>())).Returns(1.0);

        _calculator = new TotalDamageMultiplierCalculator(_mockAdditiveDamageBonusCalculator.Object,
                                                          _mockTwoHandedWeaponDamageMultiplicativeCalculator.Object,
                                                          _mockVulnerableDamageBonusCalculator.Object,
                                                          _mockStrengthCalculator.Object,
                                                          _mockPitFighter.Object,
                                                          _mockWarCry.Object,
                                                          _mockPowerWarCry.Object,
                                                          _mockSupremeWrathOfTheBerserker.Object,
                                                          _mockUnbridledRage.Object,
                                                          _mockViolentWhirlwind.Object,
                                                          _mockEnhancedLungingStrike.Object,
                                                          _mockEdgemastersAspect.Object,
                                                          _mockAspectOfLimitlessRage.Object,
                                                          _mockConceitedAspect.Object,
                                                          _mockAspectOfTheExpectant.Object,
                                                          _mockExploitersAspect.Object,
                                                          _mockPenitentGreaves.Object,
                                                          _mockRamaladnisMagnumOpus.Object,
                                                          _mockBerserkingDamageCalculator.Object,
                                                          _mockPolearmExpertise.Object);
    }

    [Fact]
    public void Returns_1_When_No_Extra_Bonuses()
    {
        var result = _calculator.Calculate(_state, DamageType.Physical, _state.Enemies.First(), SkillType.Basic, DamageSource.LungingStrike, null);

        result.Should().Be(1.0);
    }

    [Fact]
    public void Includes_Additive_Damage()
    {
        _mockAdditiveDamageBonusCalculator.Setup(m => m.Calculate(_state, DamageType.Physical, _state.Enemies.First())).Returns(1.12);

        var result = _calculator.Calculate(_state, DamageType.Physical, _state.Enemies.First(), SkillType.Basic, DamageSource.LungingStrike, null);

        result.Should().Be(1.12);
    }

    [Fact]
    public void Includes_TwoHandedWeaponDamageMultiplicative()
    {
        _state.Config.Gear.TwoHandSlashing.Expertise = Expertise.TwoHandedSword;
        _mockTwoHandedWeaponDamageMultiplicativeCalculator.Setup(m => m.Calculate(_state, _state.Config.Gear.TwoHandSlashing)).Returns(1.12);

        var result = _calculator.Calculate(_state, DamageType.Physical, _state.Enemies.First(), SkillType.Basic, DamageSource.LungingStrike, _state.Config.Gear.TwoHandSlashing);

        result.Should().Be(1.12);
    }

    [Fact]
    public void Includes_Vulnerable_Damage()
    {
        _mockVulnerableDamageBonusCalculator.Setup(m => m.Calculate(_state, _state.Enemies.First(), _state.Config.Gear.TwoHandSlashing)).Returns(1.12);

        var result = _calculator.Calculate(_state, DamageType.Physical, _state.Enemies.First(), SkillType.Basic, DamageSource.LungingStrike, _state.Config.Gear.TwoHandSlashing);

        result.Should().Be(1.12);
    }

    [Fact]
    public void Includes_Strength_Bonus()
    {
        _mockStrengthCalculator.Setup(m => m.GetDamageMultiplier(_state, SkillType.Basic)).Returns(1.042);

        var result = _calculator.Calculate(_state, DamageType.Physical, _state.Enemies.First(), SkillType.Basic, DamageSource.LungingStrike, null);

        result.Should().Be(1.042);
    }

    [Fact]
    public void Includes_WarCry_Bonus()
    {
        _mockWarCry.Setup(m => m.GetDamageBonus(_state)).Returns(1.21);

        var result = _calculator.Calculate(_state, DamageType.Physical, _state.Enemies.First(), SkillType.Basic, DamageSource.LungingStrike, null);

        result.Should().Be(1.21);
    }

    [Fact]
    public void Includes_PowerWarCry_Bonus()
    {
        _mockPowerWarCry.Setup(m => m.GetDamageBonus(_state)).Returns(1.1);

        var result = _calculator.Calculate(_state, DamageType.Physical, _state.Enemies.First(), SkillType.Basic, DamageSource.LungingStrike, null);

        result.Should().Be(1.1);
    }

    [Fact]
    public void Includes_UnbridledRage_Bonus()
    {
        _mockUnbridledRage.Setup(m => m.GetDamageBonus(_state, SkillType.Core)).Returns(2.0);

        var result = _calculator.Calculate(_state, DamageType.Physical, _state.Enemies.First(), SkillType.Core, DamageSource.Whirlwind, null);

        result.Should().Be(2);
    }

    [Fact]
    public void UnbridledRage_Only_Applies_To_Core_Skills()
    {
        _state.Config.Skills.Add(Skill.UnbridledRage, 1);

        var result = _calculator.Calculate(_state, DamageType.Physical, _state.Enemies.First(), SkillType.Basic, DamageSource.LungingStrike, null);

        result.Should().Be(1);
    }

    [Fact]
    public void Includes_PitFighter_Bonus()
    {
        _mockPitFighter.Setup(m => m.GetCloseDamageBonus(_state)).Returns(1.03);

        var result = _calculator.Calculate(_state, DamageType.Physical, _state.Enemies.First(), SkillType.Core, DamageSource.Whirlwind, null);

        result.Should().Be(1.03);
    }

    [Fact]
    public void Includes_WrathOfTheBerserker_Bonus()
    {
        _mockSupremeWrathOfTheBerserker.Setup(m => m.GetBerserkDamageBonus(_state)).Returns(1.25);

        var result = _calculator.Calculate(_state, DamageType.Physical, _state.Enemies.First(), SkillType.Core, DamageSource.Whirlwind, null);

        result.Should().Be(1.25);
    }

    [Fact]
    public void Includes_EdgemastersAspect_Bonus()
    {
        _mockEdgemastersAspect.Setup(m => m.GetDamageBonus(_state, SkillType.Core)).Returns(1.2);
        _state.Config.Gear.Chest.Aspect = _mockEdgemastersAspect.Object;

        var result = _calculator.Calculate(_state, DamageType.Physical, _state.Enemies.First(), SkillType.Core, DamageSource.Whirlwind, null);

        result.Should().Be(1.2);
    }

    [Fact]
    public void Includes_ViolentWhirlwind_Bonus()
    {
        _mockViolentWhirlwind.Setup(m => m.GetDamageBonus(_state, DamageSource.Whirlwind)).Returns(1.3);
        var result = _calculator.Calculate(_state, DamageType.Physical, _state.Enemies.First(), SkillType.Core, DamageSource.Whirlwind, null);

        result.Should().Be(1.3);
    }

    [Fact]
    public void Includes_EnhancedLungingStrike_Bonus()
    {
        _mockEnhancedLungingStrike.Setup(m => m.GetDamageBonus(_state, DamageSource.LungingStrike, _state.Enemies.First())).Returns(1.3);
        var result = _calculator.Calculate(_state, DamageType.Physical, _state.Enemies.First(), SkillType.Basic, DamageSource.LungingStrike, null);

        result.Should().Be(1.3);
    }

    [Fact]
    public void Includes_AspectOfLimitlessRage_Bonus()
    {
        _mockAspectOfLimitlessRage.Setup(m => m.GetDamageBonus(_state, SkillType.Core)).Returns(1.2);
        _state.Config.Gear.Chest.Aspect = _mockAspectOfLimitlessRage.Object;

        var result = _calculator.Calculate(_state, DamageType.Physical, _state.Enemies.First(), SkillType.Core, DamageSource.Whirlwind, null);

        result.Should().Be(1.2);
    }

    [Fact]
    public void Includes_ConceitedAspect_Bonus()
    {
        _mockConceitedAspect.Setup(m => m.GetDamageBonus(_state)).Returns(1.25);
        _state.Config.Gear.Chest.Aspect = _mockConceitedAspect.Object;

        var result = _calculator.Calculate(_state, DamageType.Physical, _state.Enemies.First(), SkillType.Core, DamageSource.Whirlwind, null);

        result.Should().Be(1.25);
    }

    [Fact]
    public void Includes_AspectOfTheExpectant_Bonus()
    {
        _mockAspectOfTheExpectant.Setup(m => m.GetDamageBonus(_state, SkillType.Core)).Returns(1.25);
        _state.Config.Gear.Chest.Aspect = _mockAspectOfTheExpectant.Object;

        var result = _calculator.Calculate(_state, DamageType.Physical, _state.Enemies.First(), SkillType.Core, DamageSource.Whirlwind, null);

        result.Should().Be(1.25);
    }

    [Fact]
    public void Includes_ExploitersAspect_Bonus()
    {
        _mockExploitersAspect.Setup(m => m.GetDamageBonus(_state, _state.Enemies.First())).Returns(1.25);
        _state.Config.Gear.Chest.Aspect = _mockExploitersAspect.Object;

        var result = _calculator.Calculate(_state, DamageType.Physical, _state.Enemies.First(), SkillType.Core, DamageSource.Whirlwind, null);

        result.Should().Be(1.25);
    }

    [Fact]
    public void Includes_PenitentGreaves_Bonus()
    {
        _mockPenitentGreaves.Setup(m => m.GetDamageBonus(_state)).Returns(1.25);
        _state.Config.Gear.Chest.Aspect = _mockPenitentGreaves.Object;

        var result = _calculator.Calculate(_state, DamageType.Physical, _state.Enemies.First(), SkillType.Core, DamageSource.Whirlwind, null);

        result.Should().Be(1.25);
    }

    [Fact]
    public void Includes_RamaladnisMagnumOpus_Bonus()
    {
        var weapon = new GearItem() { Expertise = Expertise.Polearm };
        _mockRamaladnisMagnumOpus.Setup(m => m.GetDamageBonus(_state, weapon)).Returns(1.25);
        _state.Config.Gear.Chest.Aspect = _mockRamaladnisMagnumOpus.Object;

        var result = _calculator.Calculate(_state, DamageType.Physical, _state.Enemies.First(), SkillType.Core, DamageSource.Whirlwind, weapon);

        result.Should().Be(1.25);
    }

    [Fact]
    public void Includes_Berserking_Bonus()
    {
        var weapon = new GearItem() { Expertise = Expertise.Polearm };
        _mockBerserkingDamageCalculator.Setup(m => m.Calculate(_state)).Returns(1.25);

        var result = _calculator.Calculate(_state, DamageType.Physical, _state.Enemies.First(), SkillType.Core, DamageSource.Whirlwind, weapon);

        result.Should().Be(1.25);
    }

    [Fact]
    public void Includes_PolearmExpertise_Bonus()
    {
        var weapon = new GearItem() { Expertise = Expertise.Polearm };
        _mockPolearmExpertise.Setup(m => m.GetHealthyDamageMultiplier(_state, weapon)).Returns(1.25);

        var result = _calculator.Calculate(_state, DamageType.Physical, _state.Enemies.First(), SkillType.Core, DamageSource.Whirlwind, weapon);

        result.Should().Be(1.25);
    }

    [Fact]
    public void Multiplies_Bonuses_Together()
    {
        _mockWarCry.Setup(m => m.GetDamageBonus(_state)).Returns(1.21);
        _mockUnbridledRage.Setup(m => m.GetDamageBonus(_state, SkillType.Core)).Returns(2.0);
        _mockPitFighter.Setup(m => m.GetCloseDamageBonus(_state)).Returns(1.09);

        _mockAdditiveDamageBonusCalculator.Setup(m => m.Calculate(_state, DamageType.Physical, _state.Enemies.First())).Returns(1.2);
        _mockVulnerableDamageBonusCalculator.Setup(m => m.Calculate(_state, _state.Enemies.First(), null)).Returns(1.2);
        _mockStrengthCalculator.Setup(m => m.GetDamageMultiplier(_state, SkillType.Core)).Returns(1.05);

        var result = _calculator.Calculate(_state, DamageType.Physical, _state.Enemies.First(), SkillType.Core, DamageSource.Whirlwind, null);

        result.Should().BeApproximately(3.9883536, 0.0000001); // 1.2 * 1.2 * 1.05 * 1.09 * 1.21 * 2.0
    }
}
