using BarbarianSim.Aspects;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.Aspects;

public sealed class AspectOfTheExpectantTests
{
    private readonly Mock<SimLogger> _mockSimLogger = TestHelpers.CreateMock<SimLogger>();
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly AspectOfTheExpectant _aspect;

    public AspectOfTheExpectantTests()
    {
        _aspect = new(_mockSimLogger.Object)
        {
            Damage = 10
        };
        _state.Config.Gear.Helm.Aspect = _aspect;
    }

    [Fact]
    public void GetDamageBonus_With_1_Stack()
    {
        var directDamageEvent = new DirectDamageEvent(123, null, 500, DamageType.Physical, DamageSource.LungingStrike, SkillType.Basic, 0, null, _state.Enemies.First());
        _state.ProcessedEvents.Add(directDamageEvent);

        _aspect.GetDamageBonus(_state, SkillType.Core).Should().Be(1.1);
    }

    [Fact]
    public void GetDamageBonus_With_Max_Stacks()
    {
        var directDamageEvent = new DirectDamageEvent(123, null, 500, DamageType.Physical, DamageSource.LungingStrike, SkillType.Basic, 0, null, _state.Enemies.First());
        _state.ProcessedEvents.Add(directDamageEvent);
        directDamageEvent = new DirectDamageEvent(124, null, 500, DamageType.Physical, DamageSource.LungingStrike, SkillType.Basic, 0, null, _state.Enemies.First());
        _state.ProcessedEvents.Add(directDamageEvent);
        directDamageEvent = new DirectDamageEvent(125, null, 500, DamageType.Physical, DamageSource.LungingStrike, SkillType.Basic, 0, null, _state.Enemies.First());
        _state.ProcessedEvents.Add(directDamageEvent);
        directDamageEvent = new DirectDamageEvent(126, null, 500, DamageType.Physical, DamageSource.LungingStrike, SkillType.Basic, 0, null, _state.Enemies.First());
        _state.ProcessedEvents.Add(directDamageEvent);

        _aspect.GetDamageBonus(_state, SkillType.Core).Should().Be(1.3);
    }

    [Fact]
    public void GetDamageBonus_Only_Includes_Stacks_Since_Last_Core_Skill()
    {
        var directDamageEvent = new DirectDamageEvent(123, null, 500, DamageType.Physical, DamageSource.LungingStrike, SkillType.Basic, 0, null, _state.Enemies.First());
        _state.ProcessedEvents.Add(directDamageEvent);
        directDamageEvent = new DirectDamageEvent(124, null, 500, DamageType.Physical, DamageSource.LungingStrike, SkillType.Core, 0, null, _state.Enemies.First());
        _state.ProcessedEvents.Add(directDamageEvent);
        directDamageEvent = new DirectDamageEvent(125, null, 500, DamageType.Physical, DamageSource.LungingStrike, SkillType.Basic, 0, null, _state.Enemies.First());
        _state.ProcessedEvents.Add(directDamageEvent);
        directDamageEvent = new DirectDamageEvent(126, null, 500, DamageType.Physical, DamageSource.LungingStrike, SkillType.Basic, 0, null, _state.Enemies.First());
        _state.ProcessedEvents.Add(directDamageEvent);

        _aspect.GetDamageBonus(_state, SkillType.Core).Should().Be(1.2);
    }
}
