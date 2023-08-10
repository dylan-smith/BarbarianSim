using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Skills;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.Skills;

public class EnhancedChallengingShoutTests
{
    private readonly Mock<SimLogger> _mockSimLogger = TestHelpers.CreateMock<SimLogger>();
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly EnhancedChallengingShout _skill;

    public EnhancedChallengingShoutTests() => _skill = new(_mockSimLogger.Object);

    [Fact]
    public void Returns_20_Percent_Multiplier_When_Active()
    {
        _state.Config.Skills.Add(Skill.EnhancedChallengingShout, 1);
        _state.Player.Auras.Add(Aura.ChallengingShout);

        _skill.GetMaxLifeMultiplier(_state).Should().Be(1.2);
    }

    [Fact]
    public void Returns_1_If_Not_Skilled()
    {
        _state.Player.Auras.Add(Aura.ChallengingShout);

        _skill.GetMaxLifeMultiplier(_state).Should().Be(1.0);
    }

    [Fact]
    public void Returns_1_If_Shout_Not_Active()
    {
        _state.Config.Skills.Add(Skill.EnhancedChallengingShout, 1);

        _skill.GetMaxLifeMultiplier(_state).Should().Be(1.0);
    }
}
