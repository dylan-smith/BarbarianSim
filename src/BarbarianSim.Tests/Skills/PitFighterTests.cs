﻿using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Skills;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.Skills;

public class PitFighterTests
{
    private readonly Mock<SimLogger> _mockSimLogger = TestHelpers.CreateMock<SimLogger>();
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly PitFighter _skill;

    public PitFighterTests() => _skill = new(_mockSimLogger.Object);

    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 1.03)]
    [InlineData(2, 1.06)]
    [InlineData(3, 1.09)]
    [InlineData(4, 1.09)]
    public void Skill_Points_Determines_CloseDamageBonus(int skillPoints, double damageBonus)
    {
        _state.Config.Skills.Add(Skill.PitFighter, skillPoints);

        _skill.GetCloseDamageBonus(_state).Should().Be(damageBonus);
    }
}
