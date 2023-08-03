using BarbarianSim.Arsenal;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.Arsenal;

public class TwoHandedMaceExpertiseTests
{
    private readonly Mock<RandomGenerator> _mockRandomGenerator = TestHelpers.CreateMock<RandomGenerator>();
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly TwoHandedMaceExpertise _expertise;

    public TwoHandedMaceExpertiseTests()
    {
        _mockRandomGenerator.Setup(m => m.Roll(RollType.TwoHandedMaceExpertise)).Returns(0);
        _expertise = new(_mockRandomGenerator.Object);
    }

    [Fact]
    public void Creates_TwoHandedMaceExpertiseProcEvent_Using_Mace()
    {
        _state.Config.Gear.TwoHandBludgeoning.Expertise = Expertise.TwoHandedMace;
        _state.Config.PlayerSettings.ExpertiseTechnique = Expertise.NA;
        var luckyHitEvent = new LuckyHitEvent(123, SkillType.None, _state.Enemies.First(), _state.Config.Gear.TwoHandBludgeoning);

        _expertise.ProcessEvent(luckyHitEvent, _state);

        _state.Events.Should().ContainSingle(e => e is TwoHandedMaceExpertiseProcEvent);
        _state.Events.OfType<TwoHandedMaceExpertiseProcEvent>().Single().Timestamp.Should().Be(123);
    }

    [Fact]
    public void Creates_TwoHandedMaceExpertiseProcEvent_When_Technique_Is_Mace()
    {
        _state.Config.Gear.TwoHandSlashing.Expertise = Expertise.NA;
        _state.Config.PlayerSettings.ExpertiseTechnique = Expertise.TwoHandedMace;
        var luckyHitEvent = new LuckyHitEvent(123, SkillType.None, _state.Enemies.First(), _state.Config.Gear.TwoHandSlashing);

        _expertise.ProcessEvent(luckyHitEvent, _state);

        _state.Events.Should().ContainSingle(e => e is TwoHandedMaceExpertiseProcEvent);
        _state.Events.OfType<TwoHandedMaceExpertiseProcEvent>().Single().Timestamp.Should().Be(123);
    }

    [Fact]
    public void Only_Procs_10_Percent()
    {
        _mockRandomGenerator.Setup(m => m.Roll(RollType.TwoHandedMaceExpertise)).Returns(0.11);
        _state.Config.Gear.TwoHandSlashing.Expertise = Expertise.NA;
        _state.Config.PlayerSettings.ExpertiseTechnique = Expertise.TwoHandedMace;
        var luckyHitEvent = new LuckyHitEvent(123, SkillType.None, _state.Enemies.First(), _state.Config.Gear.TwoHandSlashing);

        _expertise.ProcessEvent(luckyHitEvent, _state);

        _state.Events.Should().NotContain(e => e is TwoHandedMaceExpertiseProcEvent);
    }

    [Fact]
    public void GetCriticalStrikeDamageMultiplier_When_Weapon_Is_Mace()
    {
        _state.Config.Gear.TwoHandBludgeoning.Expertise = Expertise.TwoHandedMace;
        _state.Config.PlayerSettings.ExpertiseTechnique = Expertise.NA;
        _state.Enemies.First().Auras.Add(Aura.Stun);
        _state.Player.Auras.Add(Aura.Berserking);

        var multiplier = _expertise.GetCriticalStrikeDamageMultiplier(_state, _state.Config.Gear.TwoHandBludgeoning, _state.Enemies.First());

        multiplier.Should().Be(1.15);
    }

    [Fact]
    public void GetCriticalStrikeDamageMultiplier_Returns_1_When_Weapon_Not_Mace()
    {
        _state.Config.Gear.TwoHandSlashing.Expertise = Expertise.NA;
        _state.Config.PlayerSettings.ExpertiseTechnique = Expertise.TwoHandedMace;
        _state.Enemies.First().Auras.Add(Aura.Stun);
        _state.Player.Auras.Add(Aura.Berserking);

        var multiplier = _expertise.GetCriticalStrikeDamageMultiplier(_state, _state.Config.Gear.TwoHandSlashing, _state.Enemies.First());

        multiplier.Should().Be(1);
    }

    [Fact]
    public void GetCriticalStrikeDamageMultiplier_Returns_1_When_Player_Not_Berserking()
    {
        _state.Config.Gear.TwoHandBludgeoning.Expertise = Expertise.TwoHandedMace;
        _state.Config.PlayerSettings.ExpertiseTechnique = Expertise.NA;
        _state.Enemies.First().Auras.Add(Aura.Stun);

        var multiplier = _expertise.GetCriticalStrikeDamageMultiplier(_state, _state.Config.Gear.TwoHandBludgeoning, _state.Enemies.First());

        multiplier.Should().Be(1);
    }

    [Fact]
    public void GetCriticalStrikeDamageMultiplier_Returns_1_When_Enemy_Not_Stunned_Or_Vulnerable()
    {
        _state.Config.Gear.TwoHandBludgeoning.Expertise = Expertise.TwoHandedMace;
        _state.Config.PlayerSettings.ExpertiseTechnique = Expertise.NA;
        _state.Player.Auras.Add(Aura.Berserking);

        var multiplier = _expertise.GetCriticalStrikeDamageMultiplier(_state, _state.Config.Gear.TwoHandBludgeoning, _state.Enemies.First());

        multiplier.Should().Be(1);
    }

    [Fact]
    public void GetCriticalStrikeDamageMultiplier_When_Enemy_Vulnerable()
    {
        _state.Config.Gear.TwoHandBludgeoning.Expertise = Expertise.TwoHandedMace;
        _state.Config.PlayerSettings.ExpertiseTechnique = Expertise.NA;
        _state.Enemies.First().Auras.Add(Aura.Vulnerable);
        _state.Player.Auras.Add(Aura.Berserking);

        var multiplier = _expertise.GetCriticalStrikeDamageMultiplier(_state, _state.Config.Gear.TwoHandBludgeoning, _state.Enemies.First());

        multiplier.Should().Be(1.15);
    }
}
