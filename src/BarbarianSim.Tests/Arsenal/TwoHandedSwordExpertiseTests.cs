using BarbarianSim.Arsenal;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Arsenal;

public class TwoHandedSwordExpertiseTests
{
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly TwoHandedSwordExpertise _expertise = new();

    [Fact]
    public void Creates_BleedAppliedEvent_Using_Sword()
    {
        _state.Config.Gear.TwoHandSlashing.Expertise = Expertise.TwoHandedSword;
        _state.Config.PlayerSettings.ExpertiseTechnique = Expertise.NA;
        var directDamageEvent = new DirectDamageEvent(123, null, 500, DamageType.None, DamageSource.None, SkillType.None, 0, _state.Config.Gear.TwoHandSlashing, _state.Enemies.First());

        _expertise.ProcessEvent(directDamageEvent, _state);

        _state.Events.Should().ContainSingle(e => e is BleedAppliedEvent);
        _state.Events.OfType<BleedAppliedEvent>().Single().Timestamp.Should().Be(123);
        _state.Events.OfType<BleedAppliedEvent>().Single().Damage.Should().Be(500 * 0.2);
        _state.Events.OfType<BleedAppliedEvent>().Single().Duration.Should().Be(5);
        _state.Events.OfType<BleedAppliedEvent>().Single().Target.Should().Be(_state.Enemies.First());
    }

    [Fact]
    public void Creates_BleedAppliedEvent_When_Technique_Is_Sword()
    {
        _state.Config.Gear.TwoHandSlashing.Expertise = Expertise.NA;
        _state.Config.PlayerSettings.ExpertiseTechnique = Expertise.TwoHandedSword;
        var directDamageEvent = new DirectDamageEvent(123, null, 500, DamageType.None, DamageSource.None, SkillType.None, 0, _state.Config.Gear.TwoHandSlashing, _state.Enemies.First());

        _expertise.ProcessEvent(directDamageEvent, _state);

        _state.Events.Should().ContainSingle(e => e is BleedAppliedEvent);
        _state.Events.OfType<BleedAppliedEvent>().Single().Timestamp.Should().Be(123);
        _state.Events.OfType<BleedAppliedEvent>().Single().Damage.Should().Be(500 * 0.2);
        _state.Events.OfType<BleedAppliedEvent>().Single().Duration.Should().Be(5);
        _state.Events.OfType<BleedAppliedEvent>().Single().Target.Should().Be(_state.Enemies.First());
    }
}
