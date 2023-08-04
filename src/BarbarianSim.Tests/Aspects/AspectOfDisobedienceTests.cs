using BarbarianSim.Aspects;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Aspects;

public sealed class AspectOfDisobedienceTests
{
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly AspectOfDisobedience _aspect = new();

    public AspectOfDisobedienceTests()
    {
        _state.Config.Gear.Helm.Aspect = _aspect;
        _aspect.ArmorIncrement = 0.5;
        _aspect.MaxArmorBonus = 30;
    }

    [Fact]
    public void Returns_Max_Bonus_When_Lots_Of_Damage_Events()
    {
        for (var i = 0; i < 100; i++)
        {
            _state.ProcessedEvents.Add(new DamageEvent(i / 100.0, null, 100, DamageType.DamageOverTime, DamageSource.Bleeding, SkillType.None, _state.Enemies.First()));
        }

        _state.CurrentTime = 3;

        _aspect.GetArmorBonus(_state).Should().Be(1.3);
    }

    [Fact]
    public void Only_Considers_Damage_In_Previous_4_Seconds()
    {
        _state.ProcessedEvents.Add(new DamageEvent(1, null, 100, DamageType.DamageOverTime, DamageSource.Bleeding, SkillType.None, _state.Enemies.First()));
        _state.ProcessedEvents.Add(new DamageEvent(2, null, 100, DamageType.DamageOverTime, DamageSource.Bleeding, SkillType.None, _state.Enemies.First()));
        _state.ProcessedEvents.Add(new DamageEvent(3, null, 100, DamageType.DamageOverTime, DamageSource.Bleeding, SkillType.None, _state.Enemies.First()));
        _state.ProcessedEvents.Add(new DamageEvent(4, null, 100, DamageType.DamageOverTime, DamageSource.Bleeding, SkillType.None, _state.Enemies.First()));
        _state.ProcessedEvents.Add(new DamageEvent(5, null, 100, DamageType.DamageOverTime, DamageSource.Bleeding, SkillType.None, _state.Enemies.First()));
        _state.ProcessedEvents.Add(new DamageEvent(6, null, 100, DamageType.DamageOverTime, DamageSource.Bleeding, SkillType.None, _state.Enemies.First()));

        _state.CurrentTime = 6.3;

        _aspect.GetArmorBonus(_state).Should().Be(1.02);
    }

    [Fact]
    public void Returns_1_When_No_Damage()
    {
        _state.ProcessedEvents.Add(new DamageEvent(1, null, 100, DamageType.DamageOverTime, DamageSource.Bleeding, SkillType.None, _state.Enemies.First()));
        _state.ProcessedEvents.Add(new DamageEvent(2, null, 100, DamageType.DamageOverTime, DamageSource.Bleeding, SkillType.None, _state.Enemies.First()));
        _state.ProcessedEvents.Add(new DamageEvent(3, null, 100, DamageType.DamageOverTime, DamageSource.Bleeding, SkillType.None, _state.Enemies.First()));
        _state.ProcessedEvents.Add(new DamageEvent(4, null, 100, DamageType.DamageOverTime, DamageSource.Bleeding, SkillType.None, _state.Enemies.First()));
        _state.ProcessedEvents.Add(new DamageEvent(5, null, 100, DamageType.DamageOverTime, DamageSource.Bleeding, SkillType.None, _state.Enemies.First()));
        _state.ProcessedEvents.Add(new DamageEvent(6, null, 100, DamageType.DamageOverTime, DamageSource.Bleeding, SkillType.None, _state.Enemies.First()));

        _state.CurrentTime = 12;

        _aspect.GetArmorBonus(_state).Should().Be(1);
    }

    [Fact]
    public void Returns_1_When_Not_Equipped()
    {
        _state.Config.Gear.Helm.Aspect = null;
        _state.ProcessedEvents.Add(new DamageEvent(6, null, 100, DamageType.DamageOverTime, DamageSource.Bleeding, SkillType.None, _state.Enemies.First()));

        _state.CurrentTime = 6.1;

        _aspect.GetArmorBonus(_state).Should().Be(1);
    }
}
