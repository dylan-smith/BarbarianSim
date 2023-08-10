using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.Skills;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.Skills;

public class SupremeWrathOfTheBerserkerTests
{
    private readonly Mock<SimLogger> _mockSimLogger = TestHelpers.CreateMock<SimLogger>();
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly SupremeWrathOfTheBerserker _skill;

    public SupremeWrathOfTheBerserkerTests() => _skill = new(_mockSimLogger.Object);

    [Fact]
    public void GetBerserkDamageBonus_Returns_3x_When_157_Fury_Spent()
    {
        _state.ProcessedEvents.Add(new WrathOfTheBerserkerEvent(123));
        _state.ProcessedEvents.Add(new FurySpentEvent(127, null, 157, SkillType.None) { FurySpent = 157 });
        _state.Player.Auras.Add(Aura.WrathOfTheBerserker);
        _state.Player.Auras.Add(Aura.Berserking);
        _state.Config.Skills.Add(Skill.SupremeWrathOfTheBerserker, 1);

        _skill.GetBerserkDamageBonus(_state).Should().Be(1.25 * 1.25 * 1.25);
    }

    [Fact]
    public void GetBerserkDamageBonus_Sums_All_FurySpentEvents()
    {
        _state.ProcessedEvents.Add(new WrathOfTheBerserkerEvent(123));
        _state.ProcessedEvents.Add(new FurySpentEvent(123.5, null, 46, SkillType.None) { FurySpent = 46 });
        _state.ProcessedEvents.Add(new FurySpentEvent(127, null, 157, SkillType.None) { FurySpent = 157 });
        _state.Player.Auras.Add(Aura.WrathOfTheBerserker);
        _state.Player.Auras.Add(Aura.Berserking);
        _state.Config.Skills.Add(Skill.SupremeWrathOfTheBerserker, 1);

        _skill.GetBerserkDamageBonus(_state).Should().Be(1.25 * 1.25 * 1.25 * 1.25);
    }

    [Fact]
    public void GetBerserkDamageBonus_Excludes_Fury_Spent_Before_Wrath()
    {
        _state.ProcessedEvents.Add(new WrathOfTheBerserkerEvent(123));
        _state.ProcessedEvents.Add(new FurySpentEvent(122, null, 157, SkillType.None) { FurySpent = 157 });
        _state.ProcessedEvents.Add(new FurySpentEvent(127, null, 157, SkillType.None) { FurySpent = 157 });
        _state.Player.Auras.Add(Aura.WrathOfTheBerserker);
        _state.Player.Auras.Add(Aura.Berserking);
        _state.Config.Skills.Add(Skill.SupremeWrathOfTheBerserker, 1);

        _skill.GetBerserkDamageBonus(_state).Should().Be(1.25 * 1.25 * 1.25);
    }

    [Fact]
    public void GetBerserkDamageBonus_Returns_1_When_FurySpent_Less_Than_50()
    {
        _state.ProcessedEvents.Add(new WrathOfTheBerserkerEvent(123));
        _state.ProcessedEvents.Add(new FurySpentEvent(123.5, null, 12, SkillType.None) { FurySpent = 12 });
        _state.ProcessedEvents.Add(new FurySpentEvent(127, null, 25, SkillType.None) { FurySpent = 25 });
        _state.Player.Auras.Add(Aura.WrathOfTheBerserker);
        _state.Player.Auras.Add(Aura.Berserking);
        _state.Config.Skills.Add(Skill.SupremeWrathOfTheBerserker, 1);

        _skill.GetBerserkDamageBonus(_state).Should().Be(1.0);
    }

    [Fact]
    public void GetBerserkDamageBonus_Returns_1_When_Wrath_Not_Active()
    {
        _state.ProcessedEvents.Add(new WrathOfTheBerserkerEvent(123));
        _state.ProcessedEvents.Add(new FurySpentEvent(127, null, 157, SkillType.None) { FurySpent = 157 });
        _state.Player.Auras.Add(Aura.Berserking);
        _state.Config.Skills.Add(Skill.SupremeWrathOfTheBerserker, 1);

        _skill.GetBerserkDamageBonus(_state).Should().Be(1.0);
    }

    [Fact]
    public void GetBerserkDamageBonus_Returns_1_When_Not_Berserking()
    {
        _state.ProcessedEvents.Add(new WrathOfTheBerserkerEvent(123));
        _state.ProcessedEvents.Add(new FurySpentEvent(127, null, 157, SkillType.None) { FurySpent = 157 });
        _state.Player.Auras.Add(Aura.WrathOfTheBerserker);
        _state.Config.Skills.Add(Skill.SupremeWrathOfTheBerserker, 1);

        _skill.GetBerserkDamageBonus(_state).Should().Be(1.0);
    }

    [Fact]
    public void GetBerserkDamageBonus_Returns_1_When_Not_Skilled_In_SupremeWrathOfTheBerserker()
    {
        _state.ProcessedEvents.Add(new WrathOfTheBerserkerEvent(123));
        _state.ProcessedEvents.Add(new FurySpentEvent(127, null, 157, SkillType.None) { FurySpent = 157 });
        _state.Player.Auras.Add(Aura.WrathOfTheBerserker);
        _state.Player.Auras.Add(Aura.Berserking);

        _skill.GetBerserkDamageBonus(_state).Should().Be(1.0);
    }
}
