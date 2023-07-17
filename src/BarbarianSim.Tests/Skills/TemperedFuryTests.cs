using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Skills;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Skills;

public class TemperedFuryTests
{
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly TemperedFury _skill = new();

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 3)]
    [InlineData(2, 6)]
    [InlineData(3, 9)]
    [InlineData(4, 9)]
    public void Skill_Points_Determines_Max_Fury(int skillPoints, double maxFury)
    {
        _state.Config.Skills.Add(Skill.TemperedFury, skillPoints);

        _skill.GetMaximumFury(_state).Should().Be(maxFury);
    }
}
