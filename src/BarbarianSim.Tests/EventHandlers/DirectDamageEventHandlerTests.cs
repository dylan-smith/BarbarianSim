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
    private readonly Mock<LuckyHitChanceCalculator> _mockLuckyHitChanceCalculator = TestHelpers.CreateMock<LuckyHitChanceCalculator>();
    private readonly Mock<RandomGenerator> _mockRandomGenerator = TestHelpers.CreateMock<RandomGenerator>();

    private readonly SimulationState _state = new SimulationState(new SimulationConfig());

    private readonly DirectDamageEventHandler _handler;

    public DirectDamageEventHandlerTests()
    {
        _mockTotalDamageMultiplierCalculator.Setup(x => x.Calculate(It.IsAny<SimulationState>(), It.IsAny<DamageType>(), It.IsAny<EnemyState>(), It.IsAny<SkillType>(), It.IsAny<DamageSource>()))
                                            .Returns(1.0);

        _mockCritChanceCalculator.Setup(x => x.Calculate(It.IsAny<SimulationState>(), It.IsAny<DamageType>()))
                                 .Returns(0.0);

        _mockCritDamageCalculator.Setup(x => x.Calculate(It.IsAny<SimulationState>(), It.IsAny<Expertise>()))
                                 .Returns(1.5);

        _mockLuckyHitChanceCalculator.Setup(x => x.Calculate(It.IsAny<SimulationState>()))
                                     .Returns(0.0);

        _mockRandomGenerator.Setup(x => x.Roll(It.IsAny<RollType>()))
                            .Returns(1.0);

        _handler = new DirectDamageEventHandler(_mockTotalDamageMultiplierCalculator.Object,
                                                _mockCritChanceCalculator.Object,
                                                _mockCritDamageCalculator.Object,
                                                _mockLuckyHitChanceCalculator.Object,
                                                _mockRandomGenerator.Object);
    }

    [Fact]
    public void Creates_DamageEvent()
    {
        var directDamageEvent = new DirectDamageEvent(123, 1200, DamageType.Physical, DamageSource.Whirlwind, SkillType.Core, 20, Expertise.TwoHandedSword, _state.Enemies.First());

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
        _mockTotalDamageMultiplierCalculator.Setup(m => m.Calculate(_state, DamageType.Physical, _state.Enemies.First(), SkillType.Core, DamageSource.Whirlwind))
                                            .Returns(4.5);

        var directDamageEvent = new DirectDamageEvent(123, 100, DamageType.Physical, DamageSource.Whirlwind, SkillType.Core, 20, Expertise.TwoHandedSword, _state.Enemies.First());

        _handler.ProcessEvent(directDamageEvent, _state);

        directDamageEvent.DamageEvent.Damage.Should().Be(450);
    }

    [Fact]
    public void Critical_Strike_Applies_CritChance_Bonus()
    {
        _mockCritChanceCalculator.Setup(m => m.Calculate(_state, DamageType.Physical))
                                 .Returns(0.7);

        _mockRandomGenerator.Setup(m => m.Roll(RollType.CriticalStrike))
                            .Returns(0.69);

        var directDamageEvent = new DirectDamageEvent(123, 100, DamageType.Physical, DamageSource.Whirlwind, SkillType.Core, 20, Expertise.TwoHandedSword, _state.Enemies.First());

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

        _mockCritDamageCalculator.Setup(m => m.Calculate(_state, Expertise.TwoHandedSword))
                                 .Returns(3.5);

        var directDamageEvent = new DirectDamageEvent(123, 100, DamageType.Physical, DamageSource.Whirlwind, SkillType.Core, 20, Expertise.TwoHandedSword, _state.Enemies.First());

        _handler.ProcessEvent(directDamageEvent, _state);

        var damageEvent = (DamageEvent)_state.Events.Single(e => e is DamageEvent);
        directDamageEvent.DamageEvent.Damage.Should().Be(350);
    }

    [Fact]
    public void Uses_Weapon_Expertise_For_CritDamageCalculator()
    {
        _mockRandomGenerator.Setup(m => m.Roll(RollType.CriticalStrike))
                            .Returns(0.0);

        _mockCritDamageCalculator.Setup(m => m.Calculate(_state, Expertise.TwoHandedAxe))
                                 .Returns(3.5);

        var directDamageEvent = new DirectDamageEvent(123, 1200, DamageType.Physical, DamageSource.Whirlwind, SkillType.Core, 20, Expertise.TwoHandedSword, _state.Enemies.First());

        _handler.ProcessEvent(directDamageEvent, _state);

        directDamageEvent.DamageEvent.Damage.Should().Be(1200 * 1.5);
    }

    [Fact]
    public void LuckyHit_20_Percent_Chance()
    {
        var directDamageEvent = new DirectDamageEvent(123, 1200, DamageType.Physical, DamageSource.Whirlwind, SkillType.Core, 20, Expertise.TwoHandedSword, _state.Enemies.First());
        _mockRandomGenerator.Setup(m => m.Roll(RollType.LuckyHit))
                            .Returns(0.19);

        _handler.ProcessEvent(directDamageEvent, _state);

        _state.Events.Should().Contain(directDamageEvent.LuckyHitEvent);
        _state.Events.Should().ContainSingle(e => e is LuckyHitEvent);
        directDamageEvent.LuckyHitEvent.Timestamp.Should().Be(123);
        directDamageEvent.LuckyHitEvent.SkillType.Should().Be(SkillType.Core);
        directDamageEvent.LuckyHitEvent.Target.Should().Be(_state.Enemies.First());
    }

    [Fact]
    public void LuckyHit_Includes_Increased_Chance_From_Gear()
    {
        _mockLuckyHitChanceCalculator.Setup(m => m.Calculate(_state))
                                     .Returns(15);

        _mockRandomGenerator.Setup(m => m.Roll(RollType.LuckyHit))
                            .Returns(0.34);

        var directDamageEvent = new DirectDamageEvent(123, 1200, DamageType.Physical, DamageSource.Whirlwind, SkillType.Core, 20, Expertise.TwoHandedSword, _state.Enemies.First());

        _handler.ProcessEvent(directDamageEvent, _state);

        _state.Events.Should().ContainSingle(e => e is LuckyHitEvent);
    }
}
