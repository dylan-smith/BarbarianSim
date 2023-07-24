using BarbarianSim.Aspects;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Aspects;

public sealed class IronBloodAspectTests
{
    private readonly IronBloodAspect _aspect = new();

    public IronBloodAspectTests()
    {
        _aspect.DamageReduction = 4;
        _aspect.MaxDamageReduction = 20;
    }

    [Fact]
    public void GetDamageReductionBonus_With_3_Bleeding_Enemies()
    {
        var config = new SimulationConfig();
        config.Gear.Helm.Aspect = _aspect;
        config.EnemySettings.NumberOfEnemies = 3;
        var state = new SimulationState(config);

        state.Enemies[0].Auras.Add(Aura.Bleeding);
        state.Enemies[1].Auras.Add(Aura.Bleeding);
        state.Enemies[2].Auras.Add(Aura.Bleeding);

        _aspect.GetDamageReductionBonus(state).Should().Be(12);
    }

    [Fact]
    public void GetDamageReductionBonus_Returns_0_When_Not_Equipped()
    {
        var config = new SimulationConfig();
        config.EnemySettings.NumberOfEnemies = 3;
        var state = new SimulationState(config);

        state.Enemies[0].Auras.Add(Aura.Bleeding);
        state.Enemies[1].Auras.Add(Aura.Bleeding);
        state.Enemies[2].Auras.Add(Aura.Bleeding);

        _aspect.GetDamageReductionBonus(state).Should().Be(0);
    }

    [Fact]
    public void GetDamageReductionBonus_Ignores_Non_Bleeding_Enemies()
    {
        var config = new SimulationConfig();
        config.Gear.Helm.Aspect = _aspect;
        config.EnemySettings.NumberOfEnemies = 3;
        var state = new SimulationState(config);

        state.Enemies[0].Auras.Add(Aura.Bleeding);
        state.Enemies[2].Auras.Add(Aura.Bleeding);

        _aspect.GetDamageReductionBonus(state).Should().Be(8);
    }

    [Fact]
    public void GetDamageReductionBonus_Cannot_Go_Over_Max()
    {
        var config = new SimulationConfig();
        config.Gear.Helm.Aspect = _aspect;
        config.EnemySettings.NumberOfEnemies = 7;
        var state = new SimulationState(config);

        state.Enemies[0].Auras.Add(Aura.Bleeding);
        state.Enemies[1].Auras.Add(Aura.Bleeding);
        state.Enemies[2].Auras.Add(Aura.Bleeding);
        state.Enemies[3].Auras.Add(Aura.Bleeding);
        state.Enemies[4].Auras.Add(Aura.Bleeding);
        state.Enemies[5].Auras.Add(Aura.Bleeding);
        state.Enemies[6].Auras.Add(Aura.Bleeding);

        _aspect.GetDamageReductionBonus(state).Should().Be(20);
    }
}
