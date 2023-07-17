using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Skills;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Abilities;

public class BoomingVoiceTests
{
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly BoomingVoice _skill = new();

    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 1.08)]
    [InlineData(2, 1.16)]
    [InlineData(3, 1.24)]
    [InlineData(4, 1.24)]
    public void Skill_Points_Determines_DurationIncrease(int skillPoints, double durationIncrease)
    {
        _state.Config.Skills.Add(Skill.BoomingVoice, skillPoints);

        _skill.GetDurationIncrease(_state).Should().Be(durationIncrease);
    }
}
