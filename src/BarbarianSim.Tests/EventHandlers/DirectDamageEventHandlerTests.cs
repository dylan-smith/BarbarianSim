using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.EventHandlers;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.EventHandlers;

public class DirectDamageEventHandlerTests
{
    private readonly Mock<TotalDamageMultiplierCalculator> _mockTotalDamageMultiplierCalculator = TestHelpers.CreateMock<TotalDamageMultiplierCalculator>();
    private readonly Mock<CritChanceCalculator> _mockCritChanceCalculator = TestHelpers.CreateMock<CritChanceCalculator>();
    private readonly Mock<CritDamageCalculator> _mockCritDamageCalculator = TestHelpers.CreateMock<CritDamageCalculator>();
    private readonly Mock<OverpowerDamageCalculator> _mockOverpowerDamageCalculator = TestHelpers.CreateMock<OverpowerDamageCalculator>();
    private readonly Mock<LuckyHitChanceCalculator> _mockLuckyHitChanceCalculator = TestHelpers.CreateMock<LuckyHitChanceCalculator>();
    private readonly Mock<RandomGenerator> _mockRandomGenerator = TestHelpers.CreateMock<RandomGenerator>();
    private readonly Mock<SimLogger> _mockSimLogger = TestHelpers.CreateMock<SimLogger>();

    private readonly SimulationState _state = new SimulationState(new SimulationConfig());

    private readonly DirectDamageEventHandler _handler;

    public DirectDamageEventHandlerTests()
    {
        _mockTotalDamageMultiplierCalculator.Setup(x => x.Calculate(It.IsAny<SimulationState>(), It.IsAny<DamageType>(), It.IsAny<EnemyState>(), It.IsAny<SkillType>(), It.IsAny<DamageSource>(), It.IsAny<GearItem>()))
                                            .Returns(1.0);

        _mockCritChanceCalculator.Setup(x => x.Calculate(It.IsAny<SimulationState>(), It.IsAny<DamageType>(), It.IsAny<EnemyState>(), It.IsAny<GearItem>()))
                                 .Returns(0.0);

        _mockCritDamageCalculator.Setup(x => x.Calculate(It.IsAny<SimulationState>(), It.IsAny<Expertise>(), It.IsAny<GearItem>(), It.IsAny<EnemyState>()))
                                 .Returns(1.5);

        _mockOverpowerDamageCalculator.Setup(x => x.Calculate(It.IsAny<SimulationState>()))
                                      .Returns(1.0);

        _mockLuckyHitChanceCalculator.Setup(x => x.Calculate(It.IsAny<SimulationState>(), It.IsAny<GearItem>()))
                                     .Returns(0.0);

        _mockRandomGenerator.Setup(x => x.Roll(It.IsAny<RollType>()))
                            .Returns(1.0);

        _handler = new DirectDamageEventHandler(_mockTotalDamageMultiplierCalculator.Object,
                                                _mockCritChanceCalculator.Object,
                                                _mockCritDamageCalculator.Object,
                                                _mockOverpowerDamageCalculator.Object,
                                                _mockLuckyHitChanceCalculator.Object,
                                                _mockRandomGenerator.Object,
                                                _mockSimLogger.Object);
    }

    [Fact]
    public void Creates_DamageEvent()
    {
        var directDamageEvent = new DirectDamageEvent(123, null, 1200, DamageType.Physical, DamageSource.Whirlwind, SkillType.Core, 20, null, _state.Enemies.First());

        _handler.ProcessEvent(directDamageEvent, _state);

        directDamageEvent.DamageEvent.Should().NotBeNull();
        _state.Events.Should().Contain(directDamageEvent.DamageEvent);
        directDamageEvent.DamageEvent.Timestamp.Should().Be(123);
        directDamageEvent.DamageEvent.DamageType.Should().Be(DamageType.Physical | DamageType.Direct);
        directDamageEvent.DamageEvent.DamageSource.Should().Be(DamageSource.Whirlwind);
        directDamageEvent.DamageEvent.SkillType.Should().Be(SkillType.Core);
        directDamageEvent.DamageEvent.Damage.Should().Be(1200);
    }

    [Fact]
    public void Applies_TotalDamageMultiplier_To_Damage()
    {
        var weapon = new GearItem() { MinDamage = 100, MaxDamage = 200, Expertise = Expertise.OneHandedAxe };
        _mockTotalDamageMultiplierCalculator.Setup(m => m.Calculate(_state, DamageType.Physical, _state.Enemies.First(), SkillType.Core, DamageSource.Whirlwind, weapon))
                                            .Returns(4.5);

        var directDamageEvent = new DirectDamageEvent(123, null, 100, DamageType.Physical, DamageSource.Whirlwind, SkillType.Core, 20, weapon, _state.Enemies.First());

        _handler.ProcessEvent(directDamageEvent, _state);

        directDamageEvent.DamageEvent.Damage.Should().Be(450);
    }

    [Fact]
    public void Critical_Strike_Applies_CritChance_Bonus()
    {
        _mockCritChanceCalculator.Setup(m => m.Calculate(_state, DamageType.Physical, _state.Enemies.First(), _state.Config.Gear.TwoHandBludgeoning))
                                 .Returns(0.7);

        _mockRandomGenerator.Setup(m => m.Roll(RollType.CriticalStrike))
                            .Returns(0.69);

        var directDamageEvent = new DirectDamageEvent(123, null, 100, DamageType.Physical, DamageSource.Whirlwind, SkillType.Core, 20, _state.Config.Gear.TwoHandBludgeoning, _state.Enemies.First());

        _handler.ProcessEvent(directDamageEvent, _state);

        _state.Events.Any(e => e is DamageEvent).Should().BeTrue();
        directDamageEvent.DamageEvent.Timestamp.Should().Be(123);
        directDamageEvent.DamageEvent.DamageType.Should().Be(DamageType.Physical | DamageType.Direct | DamageType.CriticalStrike);
        directDamageEvent.DamageEvent.Damage.Should().Be(150);
    }

    [Fact]
    public void Critical_Strike_Applies_CritDamaage_Bonus()
    {
        _mockRandomGenerator.Setup(m => m.Roll(RollType.CriticalStrike))
                            .Returns(0.0);

        _mockCritDamageCalculator.Setup(m => m.Calculate(_state, Expertise.NA, null, _state.Enemies.First()))
                                 .Returns(3.5);

        var directDamageEvent = new DirectDamageEvent(123, null, 100, DamageType.Physical, DamageSource.Whirlwind, SkillType.Core, 20, null, _state.Enemies.First());

        _handler.ProcessEvent(directDamageEvent, _state);

        directDamageEvent.DamageEvent.Damage.Should().Be(350);
    }

    [Fact]
    public void Critical_Strike_Applies_CritDamaage_Bonus_With_Weapon()
    {
        var weapon = new GearItem() { Expertise = Expertise.OneHandedAxe };
        _mockRandomGenerator.Setup(m => m.Roll(RollType.CriticalStrike))
                            .Returns(0.0);

        _mockCritDamageCalculator.Setup(m => m.Calculate(_state, Expertise.OneHandedAxe, weapon, _state.Enemies.First()))
                                 .Returns(3.5);

        var directDamageEvent = new DirectDamageEvent(123, null, 100, DamageType.Physical, DamageSource.Whirlwind, SkillType.Core, 20, weapon, _state.Enemies.First());

        _handler.ProcessEvent(directDamageEvent, _state);

        directDamageEvent.DamageEvent.Damage.Should().Be(350);
    }

    [Fact]
    public void Uses_Weapon_Expertise_For_CritDamageCalculator()
    {
        _mockRandomGenerator.Setup(m => m.Roll(RollType.CriticalStrike))
                            .Returns(0.0);

        _mockCritDamageCalculator.Setup(m => m.Calculate(_state, Expertise.TwoHandedAxe, null, _state.Enemies.First()))
                                 .Returns(3.5);

        var directDamageEvent = new DirectDamageEvent(123, null, 1200, DamageType.Physical, DamageSource.Whirlwind, SkillType.Core, 20, null, _state.Enemies.First());

        _handler.ProcessEvent(directDamageEvent, _state);

        directDamageEvent.DamageEvent.Damage.Should().Be(1200 * 1.5);
    }

    [Fact]
    public void Overpower_Applies_Overpower_Bonus()
    {
        _state.Player.Life = 100;
        _state.Player.Fortify = 0;

        _mockRandomGenerator.Setup(m => m.Roll(RollType.Overpower))
                            .Returns(0.0);

        _mockOverpowerDamageCalculator.Setup(m => m.Calculate(_state))
                                      .Returns(2.5);

        var directDamageEvent = new DirectDamageEvent(123, null, 1200, DamageType.Physical, DamageSource.Whirlwind, SkillType.Core, 20, null, _state.Enemies.First());

        _handler.ProcessEvent(directDamageEvent, _state);

        directDamageEvent.DamageEvent.Damage.Should().Be(1200 + (100 * 2.5));
    }

    [Fact]
    public void Overpower_Scales_With_Life()
    {
        _state.Player.Life = 500;
        _state.Player.Fortify = 0;

        _mockRandomGenerator.Setup(m => m.Roll(RollType.Overpower))
                            .Returns(0.0);

        _mockOverpowerDamageCalculator.Setup(m => m.Calculate(_state))
                                      .Returns(2.5);

        var directDamageEvent = new DirectDamageEvent(123, null, 1200, DamageType.Physical, DamageSource.Whirlwind, SkillType.Core, 20, null, _state.Enemies.First());

        _handler.ProcessEvent(directDamageEvent, _state);

        directDamageEvent.DamageEvent.Damage.Should().Be(1200 + (500 * 2.5));
    }

    [Fact]
    public void Overpower_Scales_With_Fortify()
    {
        _state.Player.Life = 500;
        _state.Player.Fortify = 300;

        _mockRandomGenerator.Setup(m => m.Roll(RollType.Overpower))
                            .Returns(0.0);

        _mockOverpowerDamageCalculator.Setup(m => m.Calculate(_state))
                                      .Returns(2.5);

        var directDamageEvent = new DirectDamageEvent(123, null, 1200, DamageType.Physical, DamageSource.Whirlwind, SkillType.Core, 20, null, _state.Enemies.First());

        _handler.ProcessEvent(directDamageEvent, _state);

        directDamageEvent.DamageEvent.Damage.Should().Be(1200 + ((500 + 300) * 2.5));
    }

    [Fact]
    public void Overpower_Has_3_Percent_Chance()
    {
        _state.Player.Life = 500;
        _state.Player.Fortify = 300;

        _mockRandomGenerator.Setup(m => m.Roll(RollType.Overpower))
                            .Returns(0.04);

        _mockOverpowerDamageCalculator.Setup(m => m.Calculate(_state))
                                      .Returns(2.5);

        var directDamageEvent = new DirectDamageEvent(123, null, 1200, DamageType.Physical, DamageSource.Whirlwind, SkillType.Core, 20, null, _state.Enemies.First());

        _handler.ProcessEvent(directDamageEvent, _state);

        directDamageEvent.DamageEvent.Damage.Should().Be(1200);
    }

    [Fact]
    public void Overpower_Updates_DamageType()
    {
        _mockRandomGenerator.Setup(m => m.Roll(RollType.Overpower))
                            .Returns(0.00);

        var directDamageEvent = new DirectDamageEvent(123, null, 1200, DamageType.Physical, DamageSource.Whirlwind, SkillType.Core, 20, null, _state.Enemies.First());

        _handler.ProcessEvent(directDamageEvent, _state);

        directDamageEvent.DamageEvent.DamageType.Should().HaveFlag(DamageType.Overpower);
    }

    [Fact]
    public void Overpower_Applies_Damage_Multipliers_To_Both_Base_And_Overpower_Damage()
    {
        _state.Player.Life = 500;
        _state.Player.Fortify = 300;

        _mockRandomGenerator.Setup(m => m.Roll(RollType.Overpower))
                            .Returns(0.0);

        _mockOverpowerDamageCalculator.Setup(m => m.Calculate(_state))
                                      .Returns(2.5);

        _mockTotalDamageMultiplierCalculator.Setup(m => m.Calculate(_state, DamageType.Physical, _state.Enemies.First(), SkillType.Core, DamageSource.Whirlwind, null))
                                            .Returns(4.0);

        var directDamageEvent = new DirectDamageEvent(123, null, 1200, DamageType.Physical, DamageSource.Whirlwind, SkillType.Core, 20, null, _state.Enemies.First());

        _handler.ProcessEvent(directDamageEvent, _state);

        directDamageEvent.DamageEvent.Damage.Should().Be((1200 * 4.0) + (800 * 4.0 * 2.5));
    }

    [Fact]
    public void LuckyHit_20_Percent_Chance()
    {
        var weapon = new GearItem() { Expertise = Expertise.OneHandedAxe };
        var directDamageEvent = new DirectDamageEvent(123, null, 1200, DamageType.Physical, DamageSource.Whirlwind, SkillType.Core, 20, weapon, _state.Enemies.First());
        _mockRandomGenerator.Setup(m => m.Roll(RollType.LuckyHit))
                            .Returns(0.19);

        _handler.ProcessEvent(directDamageEvent, _state);

        _state.Events.Should().Contain(directDamageEvent.LuckyHitEvent);
        _state.Events.Should().ContainSingle(e => e is LuckyHitEvent);
        directDamageEvent.LuckyHitEvent.Timestamp.Should().Be(123);
        directDamageEvent.LuckyHitEvent.SkillType.Should().Be(SkillType.Core);
        directDamageEvent.LuckyHitEvent.Target.Should().Be(_state.Enemies.First());
        directDamageEvent.LuckyHitEvent.Weapon.Should().Be(weapon);
    }

    [Fact]
    public void LuckyHit_Includes_Increased_Chance_From_Gear()
    {
        _mockLuckyHitChanceCalculator.Setup(m => m.Calculate(_state, _state.Config.Gear.TwoHandBludgeoning))
                                     .Returns(15);

        _mockRandomGenerator.Setup(m => m.Roll(RollType.LuckyHit))
                            .Returns(0.34);

        var directDamageEvent = new DirectDamageEvent(123, null, 1200, DamageType.Physical, DamageSource.Whirlwind, SkillType.Core, 20, _state.Config.Gear.TwoHandBludgeoning, _state.Enemies.First());

        _handler.ProcessEvent(directDamageEvent, _state);

        _state.Events.Should().ContainSingle(e => e is LuckyHitEvent);
    }
}
