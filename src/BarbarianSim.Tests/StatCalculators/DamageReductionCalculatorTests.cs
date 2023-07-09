using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public sealed class DamageReductionCalculatorTests : IDisposable
{
    public void Dispose() => BaseStatCalculator.ClearMocks();

    public DamageReductionCalculatorTests()
    {
        BaseStatCalculator.InjectMock(typeof(DamageReductionFromBleedingCalculator), new FakeStatCalculator(0.0));
        BaseStatCalculator.InjectMock(typeof(DamageReductionFromCloseCalculator), new FakeStatCalculator(0.0));
        BaseStatCalculator.InjectMock(typeof(DamageReductionWhileFortifiedCalculator), new FakeStatCalculator(0.0));
        BaseStatCalculator.InjectMock(typeof(DamageReductionWhileInjuredCalculator), new FakeStatCalculator(0.0));
    }

    [Fact]
    public void Returns_Base_DamageReduction_Of_10_Percent()
    {
        var state = new SimulationState(new SimulationConfig());

        var result = DamageReductionCalculator.Calculate(state, state.Enemies.First());

        result.Should().Be(0.9);
    }

    [Fact]
    public void Includes_DamageReduction_From_Gear()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Gear.Helm.DamageReduction = 12.0;

        var result = DamageReductionCalculator.Calculate(state, state.Enemies.First());

        result.Should().Be(0.792);
    }

    [Fact]
    public void Includes_DamageReductionFromBleeding()
    {
        var state = new SimulationState(new SimulationConfig());

        BaseStatCalculator.InjectMock(typeof(DamageReductionFromBleedingCalculator), new FakeStatCalculator(12.0));

        var result = DamageReductionCalculator.Calculate(state, state.Enemies.First());

        result.Should().Be(0.792);
    }

    [Fact]
    public void Includes_DamageReductionFromClose()
    {
        var state = new SimulationState(new SimulationConfig());

        BaseStatCalculator.InjectMock(typeof(DamageReductionFromCloseCalculator), new FakeStatCalculator(12.0));

        var result = DamageReductionCalculator.Calculate(state, state.Enemies.First());

        result.Should().Be(0.792);
    }

    [Fact]
    public void Includes_DamageReductionWhileFortified()
    {
        var state = new SimulationState(new SimulationConfig());

        BaseStatCalculator.InjectMock(typeof(DamageReductionWhileFortifiedCalculator), new FakeStatCalculator(12.0));

        var result = DamageReductionCalculator.Calculate(state, state.Enemies.First());

        result.Should().Be(0.792);
    }

    [Fact]
    public void Includes_DamageReductionWhileInjured()
    {
        var state = new SimulationState(new SimulationConfig());

        BaseStatCalculator.InjectMock(typeof(DamageReductionWhileInjuredCalculator), new FakeStatCalculator(12.0));

        var result = DamageReductionCalculator.Calculate(state, state.Enemies.First());

        result.Should().Be(0.792);
    }

    [Fact]
    public void Includes_Bonus_From_ChallengingShout()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Auras.Add(Aura.ChallengingShout);
        state.Config.Skills.Add(Skill.ChallengingShout, 5);

        var result = DamageReductionCalculator.Calculate(state, state.Enemies.First());

        result.Should().Be(0.468);
    }

    [Fact]
    public void Includes_Bonus_From_GutteralYell()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Auras.Add(Aura.GutteralYell);
        state.Config.Skills.Add(Skill.GutteralYell, 3);

        var result = DamageReductionCalculator.Calculate(state, state.Enemies.First());

        result.Should().Be(0.792); // 0.88 * 0.9
    }

    [Fact]
    public void Includes_Bonus_From_AggressiveResistance()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Auras.Add(Aura.Berserking);
        state.Config.Skills.Add(Skill.AggressiveResistance, 3);

        var result = DamageReductionCalculator.Calculate(state, state.Enemies.First());

        result.Should().BeApproximately(0.819, 0.0000001); // 0.88 * 0.9
    }

    [Fact]
    public void Multiplies_All_Damage_Reduction_Bonuses()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Auras.Add(Aura.ChallengingShout);
        state.Config.Skills.Add(Skill.ChallengingShout, 2);
        state.Player.Auras.Add(Aura.GutteralYell);
        state.Config.Skills.Add(Skill.GutteralYell, 3);
        state.Config.Skills.Add(Skill.AggressiveResistance, 1);
        state.Player.Auras.Add(Aura.Berserking);

        state.Config.Gear.Helm.DamageReduction = 8.0;

        BaseStatCalculator.InjectMock(typeof(DamageReductionFromBleedingCalculator), new FakeStatCalculator(8.0));
        BaseStatCalculator.InjectMock(typeof(DamageReductionFromCloseCalculator), new FakeStatCalculator(8.0));
        BaseStatCalculator.InjectMock(typeof(DamageReductionWhileFortifiedCalculator), new FakeStatCalculator(8.0));
        BaseStatCalculator.InjectMock(typeof(DamageReductionWhileInjuredCalculator), new FakeStatCalculator(8.0));

        var result = DamageReductionCalculator.Calculate(state, state.Enemies.First());

        result.Should().BeApproximately(0.293673, 0.000001); // 0.9 * 0.58 * 0.92 * 0.92 * 0.92 * 0.92 * 0.92 * 0.88 * 0.97
    }
}
