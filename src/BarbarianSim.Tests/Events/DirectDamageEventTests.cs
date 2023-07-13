using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Events;

public sealed class DirectDamageEventTests : IDisposable
{
    public void Dispose()
    {
        BaseStatCalculator.ClearMocks();
        RandomGenerator.ClearMock();
    }

    private readonly FakeRandomGenerator _fakeRandomGenerator = new();

    public DirectDamageEventTests()
    {
        BaseStatCalculator.InjectMock(typeof(TotalDamageMultiplierCalculator), new FakeStatCalculator(1.0, DamageType.Physical, SkillType.Core));
        BaseStatCalculator.InjectMock(typeof(CritChanceCalculator), new FakeStatCalculator(0.0, DamageType.Physical));
        BaseStatCalculator.InjectMock(typeof(CritDamageCalculator), new FakeStatCalculator(1.5));
        RandomGenerator.InjectMock(_fakeRandomGenerator);
        _fakeRandomGenerator.FakeRoll(RollType.LuckyHit, 1.0);
        _fakeRandomGenerator.FakeRoll(RollType.CriticalStrike, 1.0);
    }

    [Fact]
    public void Creates_DamageEvent()
    {
        var state = new SimulationState(new SimulationConfig());
        var directDamageEvent = new DirectDamageEvent(123, 1200, DamageType.Physical, DamageSource.Whirlwind, SkillType.Core, 20, Expertise.TwoHandedSword, state.Enemies.First());

        directDamageEvent.ProcessEvent(state);

        directDamageEvent.DamageEvent.Should().NotBeNull();
        state.Events.Should().Contain(directDamageEvent.DamageEvent);
        directDamageEvent.DamageEvent.Timestamp.Should().Be(123);
        directDamageEvent.DamageEvent.DamageType.Should().Be(DamageType.Physical | DamageType.Direct);
        directDamageEvent.DamageEvent.DamageSource.Should().Be(DamageSource.Whirlwind);
        directDamageEvent.DamageEvent.SkillType.Should().Be(SkillType.Core);
        directDamageEvent.DamageEvent.Damage.Should().Be(1200);
    }

    [Fact]
    public void Applies_TotalDamageMultiplier_To_Damage()
    {
        var state = new SimulationState(new SimulationConfig());

        BaseStatCalculator.InjectMock(typeof(TotalDamageMultiplierCalculator), new FakeStatCalculator(4.5, DamageType.Physical, SkillType.Core));
        var directDamageEvent = new DirectDamageEvent(123, 100, DamageType.Physical, DamageSource.Whirlwind, SkillType.Core, 20, Expertise.TwoHandedSword, state.Enemies.First());

        directDamageEvent.ProcessEvent(state);

        directDamageEvent.DamageEvent.Damage.Should().Be(450);
    }

    [Fact]
    public void Critical_Strike_Applies_CritChance_Bonus()
    {
        var state = new SimulationState(new SimulationConfig());
        BaseStatCalculator.InjectMock(typeof(CritChanceCalculator), new FakeStatCalculator(0.7, DamageType.Physical));
        _fakeRandomGenerator.FakeRoll(RollType.CriticalStrike, 0.69);
        var directDamageEvent = new DirectDamageEvent(123, 100, DamageType.Physical, DamageSource.Whirlwind, SkillType.Core, 20, Expertise.TwoHandedSword, state.Enemies.First());

        directDamageEvent.ProcessEvent(state);

        state.Events.Any(e => e is DamageEvent).Should().BeTrue();
        directDamageEvent.DamageEvent.Timestamp.Should().Be(123);
        directDamageEvent.DamageEvent.DamageType.Should().Be(DamageType.Physical | DamageType.Direct | DamageType.CriticalStrike);
        directDamageEvent.DamageEvent.Damage.Should().Be(150);
    }

    [Fact]
    public void Critical_Strike_Applies_CritDamaage_Bonus()
    {
        var state = new SimulationState(new SimulationConfig());
        _fakeRandomGenerator.FakeRoll(RollType.CriticalStrike, 0.0);
        BaseStatCalculator.InjectMock(typeof(CritDamageCalculator), new FakeStatCalculator(3.5));
        var directDamageEvent = new DirectDamageEvent(123, 100, DamageType.Physical, DamageSource.Whirlwind, SkillType.Core, 20, Expertise.TwoHandedSword, state.Enemies.First());

        directDamageEvent.ProcessEvent(state);

        var damageEvent = (DamageEvent)state.Events.Single(e => e is DamageEvent);
        directDamageEvent.DamageEvent.Damage.Should().Be(350);
    }

    [Fact]
    public void Uses_Weapon_Expertise_For_CritDamageCalculator()
    {
        var state = new SimulationState(new SimulationConfig());
        _fakeRandomGenerator.FakeRoll(RollType.CriticalStrike, 0.0);
        BaseStatCalculator.InjectMock(typeof(CritDamageCalculator), new FakeStatCalculator(3.5, Expertise.TwoHandedAxe));
        var directDamageEvent = new DirectDamageEvent(123, 1200, DamageType.Physical, DamageSource.Whirlwind, SkillType.Core, 20, Expertise.TwoHandedSword, state.Enemies.First());

        directDamageEvent.ProcessEvent(state);

        directDamageEvent.DamageEvent.Damage.Should().Be(0);
    }

    [Fact]
    public void LuckyHit_20_Percent_Chance()
    {
        var state = new SimulationState(new SimulationConfig());
        var directDamageEvent = new DirectDamageEvent(123, 1200, DamageType.Physical, DamageSource.Whirlwind, SkillType.Core, 20, Expertise.TwoHandedSword, state.Enemies.First());
        _fakeRandomGenerator.FakeRoll(RollType.LuckyHit, 0.19);

        directDamageEvent.ProcessEvent(state);

        state.Events.Should().Contain(directDamageEvent.LuckyHitEvent);
        state.Events.Should().ContainSingle(e => e is LuckyHitEvent);
        directDamageEvent.LuckyHitEvent.Timestamp.Should().Be(123);
        directDamageEvent.LuckyHitEvent.SkillType.Should().Be(SkillType.Core);
        directDamageEvent.LuckyHitEvent.Target.Should().Be(state.Enemies.First());
    }

    [Fact]
    public void LuckyHit_Includes_Increased_Chance_From_Gear()
    {
        var state = new SimulationState(new SimulationConfig());
        BaseStatCalculator.InjectMock(typeof(LuckyHitChanceCalculator), new FakeStatCalculator(15));
        var directDamageEvent = new DirectDamageEvent(123, 1200, DamageType.Physical, DamageSource.Whirlwind, SkillType.Core, 20, Expertise.TwoHandedSword, state.Enemies.First());
        _fakeRandomGenerator.FakeRoll(RollType.LuckyHit, 0.34);

        directDamageEvent.ProcessEvent(state);

        state.Events.Should().ContainSingle(e => e is LuckyHitEvent);
    }
}
