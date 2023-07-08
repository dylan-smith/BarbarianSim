using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Events;

public sealed class RaidLeaderProcEventTests : IDisposable
{
    public void Dispose() => BaseStatCalculator.ClearMocks();

    [Fact]
    public void Creates_Healing_Events_For_Each_Second()
    {
        var state = new SimulationState(new SimulationConfig());
        var raidLeaderProcEvent = new RaidLeaderProcEvent(123.0, 6.0);

        raidLeaderProcEvent.ProcessEvent(state);

        raidLeaderProcEvent.HealingEvents.Should().HaveCount(6);
        state.Events.OfType<HealingEvent>().Should().HaveCount(6);
        raidLeaderProcEvent.HealingEvents.First().Timestamp.Should().Be(124);
        raidLeaderProcEvent.HealingEvents.Last().Timestamp.Should().Be(129);
    }

    [Fact]
    public void Heal_Percentage_Based_On_RaidLeader_Skill()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.RaidLeader, 3);
        var raidLeaderProcEvent = new RaidLeaderProcEvent(123.0, 6.0);
        BaseStatCalculator.InjectMock(typeof(MaxLifeCalculator), new FakeStatCalculator(1000));

        raidLeaderProcEvent.ProcessEvent(state);

        raidLeaderProcEvent.HealingEvents.All(e => e.BaseAmountHealed == 30).Should().BeTrue();
    }

    [Fact]
    public void Rounds_Down_Duration()
    {
        var state = new SimulationState(new SimulationConfig());
        var raidLeaderProcEvent = new RaidLeaderProcEvent(123.0, 3.96);

        raidLeaderProcEvent.ProcessEvent(state);

        raidLeaderProcEvent.HealingEvents.Should().HaveCount(3);
    }
}
