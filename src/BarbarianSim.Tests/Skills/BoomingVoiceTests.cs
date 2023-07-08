using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Skills;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Abilities;

public class BoomingVoiceTests
{
    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 1.08)]
    [InlineData(2, 1.16)]
    [InlineData(3, 1.24)]
    [InlineData(4, 1.24)]
    public void Skill_Points_Determines_DurationIncrease(int skillPoints, double durationIncrease)
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.BoomingVoice, skillPoints);

        BoomingVoice.GetDurationIncrease(state).Should().Be(durationIncrease);
    }
}
