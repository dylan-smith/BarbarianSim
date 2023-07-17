using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.Skills;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.Skills;

public class EnhancedLungingStrikeTests
{
    private readonly Mock<MaxLifeCalculator> _mockMaxLifeCalculator = TestHelpers.CreateMock<MaxLifeCalculator>();
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly EnhancedLungingStrike _skill;

    public EnhancedLungingStrikeTests()
    {
        _mockMaxLifeCalculator.Setup(m => m.Calculate(It.IsAny<SimulationState>())).Returns(1200);
        _skill = new EnhancedLungingStrike(_mockMaxLifeCalculator.Object);
    }

    [Fact]
    public void Creates_HealingEvent_When_Enemy_Healthy()
    {
        _state.Enemies.First().MaxLife = 1000;
        _state.Enemies.First().Life = 1000;
        _state.Config.Skills.Add(Skill.EnhancedLungingStrike, 1);
        var lungingStrikeEvent = new LungingStrikeEvent(123, _state.Enemies.First());

        _skill.ProcessEvent(lungingStrikeEvent, _state);

        _state.Events.Should().ContainSingle(e => e is HealingEvent);
        _state.Events.OfType<HealingEvent>().First().BaseAmountHealed.Should().Be(1200 * 0.02);
        _state.Events.OfType<HealingEvent>().First().Timestamp.Should().Be(123);
    }

    [Fact]
    public void EnhancedLungingStrike_Does_Not_Create_HealingEvent_When_Enemy_Not_Healthy()
    {
        _state.Enemies.First().MaxLife = 1000;
        _state.Enemies.First().Life = 600;
        _state.Config.Skills.Add(Skill.EnhancedLungingStrike, 1);
        var lungingStrikeEvent = new LungingStrikeEvent(123, _state.Enemies.First());

        _skill.ProcessEvent(lungingStrikeEvent, _state);

        _state.Events.Should().NotContain(e => e is HealingEvent);
    }
}
