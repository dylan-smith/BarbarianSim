using BarbarianSim.Abilities;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.Abilities;

public class RallyingCryTests
{
    private readonly Mock<SimLogger> _mockSimLogger = TestHelpers.CreateMock<SimLogger>();
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly RallyingCry _rallyingCry;

    public RallyingCryTests()
    {
        _rallyingCry = new(_mockSimLogger.Object);
    }

    [Fact]
    public void CanUse_Returns_True_If_Not_On_Cooldown()
    {
        _state.Config.Skills.Add(Skill.RallyingCry, 1);
        _rallyingCry.CanUse(_state).Should().BeTrue();
    }

    [Fact]
    public void CanUse_Returns_False_If_On_Cooldown()
    {
        _state.Config.Skills.Add(Skill.RallyingCry, 1);
        _state.Player.Auras.Add(Aura.RallyingCryCooldown);

        _rallyingCry.CanUse(_state).Should().BeFalse();
    }

    [Fact]
    public void CanUse_Returns_False_If_Not_Skilled()
    {
        _state.Config.Skills.Add(Skill.RallyingCry, 0);
        _rallyingCry.CanUse(_state).Should().BeFalse();
    }

    [Fact]
    public void Use_Creates_RallyingCryEvent()
    {
        _state.CurrentTime = 123;

        _rallyingCry.Use(_state);

        _state.Events.Should().ContainSingle(e => e is RallyingCryEvent);
        _state.Events.Cast<RallyingCryEvent>().First().Timestamp.Should().Be(123);
    }

    [Theory]
    [InlineData(0, 1.0)]
    [InlineData(1, 1.40)]
    [InlineData(2, 1.44)]
    [InlineData(3, 1.48)]
    [InlineData(4, 1.52)]
    [InlineData(5, 1.56)]
    [InlineData(6, 1.56)]
    public void Skill_Points_Determines_ResourceGeneration(int skillPoints, double resourceGeneration)
    {
        _state.Player.Auras.Add(Aura.RallyingCry);
        _state.Config.Skills.Add(Skill.RallyingCry, skillPoints);

        _rallyingCry.GetResourceGeneration(_state).Should().Be(resourceGeneration);
    }

    [Fact]
    public void Skill_Points_From_Gear_Are_Included()
    {
        _state.Player.Auras.Add(Aura.RallyingCry);
        _state.Config.Skills.Add(Skill.RallyingCry, 1);
        _state.Config.Gear.Helm.RallyingCry = 2;

        _rallyingCry.GetResourceGeneration(_state).Should().Be(1.48);
    }

    [Fact]
    public void Returns_1_When_RallyingCry_Not_Active()
    {
        _state.Config.Skills.Add(Skill.RallyingCry, 1);
        _state.Config.Gear.Helm.RallyingCry = 2;

        _rallyingCry.GetResourceGeneration(_state).Should().Be(1);
    }
}
