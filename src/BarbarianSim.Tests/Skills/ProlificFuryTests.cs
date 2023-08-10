using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Skills;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.Skills;

public class ProlificFuryTests
{
    private readonly Mock<SimLogger> _mockSimLogger = TestHelpers.CreateMock<SimLogger>();
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly ProlificFury _skill;

    public ProlificFuryTests() => _skill = new(_mockSimLogger.Object);

    [Theory]
    [InlineData(0, 1.0)]
    [InlineData(1, 1.06)]
    [InlineData(2, 1.12)]
    [InlineData(3, 1.18)]
    [InlineData(4, 1.18)]
    public void Skill_Points_Determines_Max_Fury(int skillPoints, double furyGeneration)
    {
        _state.Config.Skills.Add(Skill.ProlificFury, skillPoints);
        _state.Player.Auras.Add(Aura.Berserking);

        _skill.GetFuryGeneration(_state).Should().Be(furyGeneration);
    }

    [Fact]
    public void Only_Activates_When_Berserking()
    {
        _state.Config.Skills.Add(Skill.ProlificFury, 3);

        _skill.GetFuryGeneration(_state).Should().Be(1.0);
    }
}
