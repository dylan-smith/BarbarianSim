using BarbarianSim.Aspects;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.Aspects;

public sealed class AspectOfBerserkRippingTests
{
    private readonly Mock<SimLogger> _mockSimLogger = TestHelpers.CreateMock<SimLogger>();
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly AspectOfBerserkRipping _aspect;

    public AspectOfBerserkRippingTests()
    {
        _aspect = new(_mockSimLogger.Object)
        {
            Damage = 30
        };
        _state.Config.Gear.Helm.Aspect = _aspect;
    }

    [Fact]
    public void Creates_BleedAppliedEvent()
    {
        var dmg = new DirectDamageEvent(123.0, null, 500.0, DamageType.Physical, DamageSource.LungingStrike, SkillType.Basic, 10, null, _state.Enemies.First());
        _state.Player.Auras.Add(Aura.Berserking);

        _aspect.ProcessEvent(dmg, _state);

        _state.Events.Should().ContainSingle(e => e is BleedAppliedEvent);
        _state.Events.OfType<BleedAppliedEvent>().First().Timestamp.Should().Be(123);
        _state.Events.OfType<BleedAppliedEvent>().First().Damage.Should().Be(150);
        _state.Events.OfType<BleedAppliedEvent>().First().Duration.Should().Be(AspectOfBerserkRipping.BLEED_DURATION);
        _state.Events.OfType<BleedAppliedEvent>().First().Target.Should().Be(_state.Enemies.First());
    }

    [Fact]
    public void Does_Nothing_When_Not_Equipped()
    {
        _state.Config.Gear.Helm.Aspect = null;
        var dmg = new DirectDamageEvent(123.0, null, 500.0, DamageType.Physical, DamageSource.LungingStrike, SkillType.Basic, 10, null, _state.Enemies.First());
        _state.Player.Auras.Add(Aura.Berserking);

        _aspect.ProcessEvent(dmg, _state);

        _state.Events.Should().NotContain(e => e is BleedAppliedEvent);
    }

    [Fact]
    public void Does_Nothing_When_Not_Berserking()
    {
        var dmg = new DirectDamageEvent(123.0, null, 500.0, DamageType.Physical, DamageSource.LungingStrike, SkillType.Basic, 10, null, _state.Enemies.First());

        _aspect.ProcessEvent(dmg, _state);

        _state.Events.Should().NotContain(e => e is BleedAppliedEvent);
    }
}
