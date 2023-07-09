using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Skills;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Skills;

public class TemperedFuryTests
{
    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 3)]
    [InlineData(2, 6)]
    [InlineData(3, 9)]
    [InlineData(4, 9)]
    public void Skill_Points_Determines_Max_Fury(int skillPoints, double maxFury)
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.TemperedFury, skillPoints);

        TemperedFury.GetMaximumFury(state).Should().Be(maxFury);
    }
}
