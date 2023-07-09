using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Skills;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Skills;

public class ProlificFuryTests
{
    [Theory]
    [InlineData(0, 1.0)]
    [InlineData(1, 1.06)]
    [InlineData(2, 1.12)]
    [InlineData(3, 1.18)]
    [InlineData(4, 1.18)]
    public void Skill_Points_Determines_Max_Fury(int skillPoints, double furyGeneration)
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.ProlificFury, skillPoints);
        state.Player.Auras.Add(Aura.Berserking);

        ProlificFury.GetFuryGeneration(state).Should().Be(furyGeneration);
    }

    [Fact]
    public void Only_Activates_When_Berserking()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.ProlificFury, 3);

        ProlificFury.GetFuryGeneration(state).Should().Be(1.0);
    }
}
