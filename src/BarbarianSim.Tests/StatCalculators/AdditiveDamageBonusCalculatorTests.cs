using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public sealed class AdditiveDamageBonusCalculatorTests : IDisposable
{
    public void Dispose() => BaseStatCalculator.ClearMocks();

    public AdditiveDamageBonusCalculatorTests()
    {
        BaseStatCalculator.InjectMock(typeof(PhysicalDamageCalculator), new FakeStatCalculator(0.0));
        BaseStatCalculator.InjectMock(typeof(DamageToCloseCalculator), new FakeStatCalculator(0.0));
        BaseStatCalculator.InjectMock(typeof(DamageToInjuredCalculator), new FakeStatCalculator(0.0));
        BaseStatCalculator.InjectMock(typeof(DamageToSlowedCalculator), new FakeStatCalculator(0.0));
        BaseStatCalculator.InjectMock(typeof(DamageToCrowdControlledCalculator), new FakeStatCalculator(0.0));
        BaseStatCalculator.InjectMock(typeof(BerserkingDamageCalculator), new FakeStatCalculator(0.0));
    }

    [Fact]
    public void Returns_1_When_No_Additive_Damage_Bonuses()
    {
        var state = new SimulationState(new SimulationConfig());

        var result = AdditiveDamageBonusCalculator.Calculate(state, DamageType.Physical);

        result.Should().Be(1.0);
    }

    [Fact]
    public void Includes_Physical_Damage()
    {
        var state = new SimulationState(new SimulationConfig());

        BaseStatCalculator.InjectMock(typeof(PhysicalDamageCalculator), new FakeStatCalculator(12.0));

        var result = AdditiveDamageBonusCalculator.Calculate(state, DamageType.Physical);

        result.Should().Be(1.12);
    }

    [Fact]
    public void Includes_Damage_To_Close()
    {
        var state = new SimulationState(new SimulationConfig());

        BaseStatCalculator.InjectMock(typeof(DamageToCloseCalculator), new FakeStatCalculator(12.0));

        var result = AdditiveDamageBonusCalculator.Calculate(state, DamageType.Physical);

        result.Should().Be(1.12);
    }

    [Fact]
    public void Includes_Damage_To_Injured()
    {
        var state = new SimulationState(new SimulationConfig());

        BaseStatCalculator.InjectMock(typeof(DamageToInjuredCalculator), new FakeStatCalculator(12.0));

        var result = AdditiveDamageBonusCalculator.Calculate(state, DamageType.Physical);

        result.Should().Be(1.12);
    }

    [Fact]
    public void Includes_Damage_To_Slowed()
    {
        var state = new SimulationState(new SimulationConfig());

        BaseStatCalculator.InjectMock(typeof(DamageToSlowedCalculator), new FakeStatCalculator(12.0));

        var result = AdditiveDamageBonusCalculator.Calculate(state, DamageType.Physical);

        result.Should().Be(1.12);
    }

    [Fact]
    public void Includes_Damage_To_Crowd_Controlled()
    {
        var state = new SimulationState(new SimulationConfig());

        BaseStatCalculator.InjectMock(typeof(DamageToCrowdControlledCalculator), new FakeStatCalculator(12.0));

        var result = AdditiveDamageBonusCalculator.Calculate(state, DamageType.Physical);

        result.Should().Be(1.12);
    }

    [Fact]
    public void Includes_BerserkingDamage()
    {
        var state = new SimulationState(new SimulationConfig());

        BaseStatCalculator.InjectMock(typeof(BerserkingDamageCalculator), new FakeStatCalculator(25.0));

        var result = AdditiveDamageBonusCalculator.Calculate(state, DamageType.Physical);

        result.Should().Be(1.25);
    }

    [Fact]
    public void Adds_All_Additive_Damage_Bonuses()
    {
        var state = new SimulationState(new SimulationConfig());

        BaseStatCalculator.InjectMock(typeof(PhysicalDamageCalculator), new FakeStatCalculator(12.0));
        BaseStatCalculator.InjectMock(typeof(DamageToCloseCalculator), new FakeStatCalculator(12.0));
        BaseStatCalculator.InjectMock(typeof(DamageToInjuredCalculator), new FakeStatCalculator(12.0));
        BaseStatCalculator.InjectMock(typeof(DamageToSlowedCalculator), new FakeStatCalculator(12.0));
        BaseStatCalculator.InjectMock(typeof(DamageToCrowdControlledCalculator), new FakeStatCalculator(12.0));
        BaseStatCalculator.InjectMock(typeof(BerserkingDamageCalculator), new FakeStatCalculator(12.0));

        var result = AdditiveDamageBonusCalculator.Calculate(state, DamageType.Physical);

        result.Should().Be(1.72);
    }
}
