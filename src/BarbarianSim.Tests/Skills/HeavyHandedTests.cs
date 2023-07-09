using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Skills;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Skills;

public class HeavyHandedTests
{
    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 3)]
    [InlineData(2, 6)]
    [InlineData(3, 9)]
    [InlineData(4, 9)]
    public void Skill_Points_Determines_CritDamage(int skillPoints, double critDamage)
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.HeavyHanded, skillPoints);

        HeavyHanded.GetCriticalStrikeDamage(state, Expertise.TwoHandedSword).Should().Be(critDamage);
    }

    [Theory]
    [InlineData(3, 0, Expertise.NA)]
    [InlineData(3, 0, Expertise.OneHandedAxe)]
    [InlineData(3, 0, Expertise.OneHandedMace)]
    [InlineData(3, 0, Expertise.OneHandedSword)]
    [InlineData(3, 9, Expertise.Polearm)]
    [InlineData(3, 9, Expertise.TwoHandedAxe)]
    [InlineData(3, 9, Expertise.TwoHandedMace)]
    [InlineData(3, 9, Expertise.TwoHandedSword)]
    public void Only_Applies_For_TwoHandedWeapons(int skillPoints, double critDamage, Expertise expertise)
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.HeavyHanded, skillPoints);

        HeavyHanded.GetCriticalStrikeDamage(state, expertise).Should().Be(critDamage);
    }
}
