using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Events;

public class GohrsDevastatingGripsProcEventTests
{
    [Fact]
    public void Creates_DamageEvents()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.EnemySettings.NumberOfEnemies = 2;
        var e = new GohrsDevastatingGripsProcEvent(123.0, 1200.0);

        e.ProcessEvent(state);

        state.Events.Should().HaveCount(2);
        state.Events.Should().Contain(e.DamageEvents[0]);
        state.Events.Should().Contain(e.DamageEvents[1]);
        e.DamageEvents[0].Damage.Should().Be(1200.0);
        e.DamageEvents[1].Damage.Should().Be(1200.0);
        e.DamageEvents[0].DamageSource.Should().Be(DamageSource.GohrsDevastatingGrips);
        e.DamageEvents[1].DamageSource.Should().Be(DamageSource.GohrsDevastatingGrips);
    }
}
