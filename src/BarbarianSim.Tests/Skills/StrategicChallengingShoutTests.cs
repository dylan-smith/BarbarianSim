using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Skills;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.Skills;

public class StrategicChallengingShoutTests
{
    private readonly Mock<MaxLifeCalculator> _mockMaxLifeCalculator = TestHelpers.CreateMock<MaxLifeCalculator>();
    private readonly Mock<SimLogger> _mockSimLogger = TestHelpers.CreateMock<SimLogger>();
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly StrategicChallengingShout _skill;

    public StrategicChallengingShoutTests()
    {
        _mockMaxLifeCalculator.Setup(m => m.Calculate(It.IsAny<SimulationState>())).Returns(1200);
        _skill = new StrategicChallengingShout(_mockMaxLifeCalculator.Object, _mockSimLogger.Object);
    }

    [Fact]
    public void GetThorns_When_Active()
    {
        _state.Config.Skills.Add(Skill.StrategicChallengingShout, 1);
        _state.Player.Auras.Add(Aura.ChallengingShout);

        _skill.GetThorns(_state).Should().Be(360);
    }

    [Fact]
    public void GetThorns_Returns_0_When_Not_Skilled()
    {
        _state.Player.Auras.Add(Aura.ChallengingShout);

        _skill.GetThorns(_state).Should().Be(0);
    }

    [Fact]
    public void GetThorns_Returns_0_When_No_Aura()
    {
        _state.Config.Skills.Add(Skill.StrategicChallengingShout, 1);

        _skill.GetThorns(_state).Should().Be(0);
    }
}
