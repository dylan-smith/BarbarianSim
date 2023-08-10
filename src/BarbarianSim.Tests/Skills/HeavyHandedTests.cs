using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Skills;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.Skills;

public class HeavyHandedTests
{
    private readonly Mock<SimLogger> _mockSimLogger = TestHelpers.CreateMock<SimLogger>();
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly HeavyHanded _skill;

    public HeavyHandedTests() => _skill = new(_mockSimLogger.Object);

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 5)]
    [InlineData(2, 10)]
    [InlineData(3, 15)]
    [InlineData(4, 15)]
    public void Skill_Points_Determines_CritDamage(int skillPoints, double critDamage)
    {
        _state.Config.Skills.Add(Skill.HeavyHanded, skillPoints);

        _skill.GetCriticalStrikeDamage(_state, Expertise.TwoHandedSword).Should().Be(critDamage);
    }

    [Theory]
    [InlineData(3, 0, Expertise.NA)]
    [InlineData(3, 0, Expertise.OneHandedAxe)]
    [InlineData(3, 0, Expertise.OneHandedMace)]
    [InlineData(3, 0, Expertise.OneHandedSword)]
    [InlineData(3, 15, Expertise.Polearm)]
    [InlineData(3, 15, Expertise.TwoHandedAxe)]
    [InlineData(3, 15, Expertise.TwoHandedMace)]
    [InlineData(3, 15, Expertise.TwoHandedSword)]
    public void Only_Applies_For_TwoHandedWeapons(int skillPoints, double critDamage, Expertise expertise)
    {
        _state.Config.Skills.Add(Skill.HeavyHanded, skillPoints);

        _skill.GetCriticalStrikeDamage(_state, expertise).Should().Be(critDamage);
    }
}
