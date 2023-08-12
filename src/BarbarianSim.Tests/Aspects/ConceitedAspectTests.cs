using BarbarianSim.Aspects;
using BarbarianSim.Config;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.Aspects;

public class ConceitedAspectTests
{
    private readonly Mock<SimLogger> _mockSimLogger = TestHelpers.CreateMock<SimLogger>();
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly ConceitedAspect _aspect;

    public ConceitedAspectTests()
    {
        _aspect = new ConceitedAspect(_mockSimLogger.Object) { Damage = 25 };
        _state.Config.Gear.Helm.Aspect = _aspect;
    }

    [Fact]
    public void GetDamageBonus_When_Active()
    {
        _state.Player.Barriers.Add(new Barrier(1000));

        _aspect.GetDamageBonus(_state).Should().Be(1.25);
    }

    [Fact]
    public void GetDamageBonus_Returns_1_When_Not_Equipped()
    {
        _state.Config.Gear.Helm.Aspect = null;
        _state.Player.Barriers.Add(new Barrier(1000));

        _aspect.GetDamageBonus(_state).Should().Be(1.0);
    }

    [Fact]
    public void GetDamageBonus_Returns_1_When_No_Barriers()
    {
        _aspect.GetDamageBonus(_state).Should().Be(1.0);
    }
}
