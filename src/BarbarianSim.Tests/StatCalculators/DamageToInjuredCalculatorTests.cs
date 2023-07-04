using BarbarianSim.Config;
using BarbarianSim.StatCalculators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BarbarianSim.Tests.StatCalculators;

[TestClass]
public class DamageToInjuredCalculatorTests
{
    [TestMethod]
    public void Includes_Damage_To_Injured_When_Enemy_Is_Injured()
    {
        var config = new SimulationConfig();
        config.EnemySettings.Life = 1000;
        config.Gear.Helm.DamageToInjured = 12.0;
        var state = new SimulationState(config);
        state.Enemy.Life = 300;

        var result = DamageToInjuredCalculator.Calculate(state);

        Assert.AreEqual(12, result);
    }

    [TestMethod]
    public void Returns_0_When_Enemy_Is_Not_Injured()
    {
        var config = new SimulationConfig();
        config.EnemySettings.Life = 1000;
        config.Gear.Helm.DamageToInjured = 12.0;
        var state = new SimulationState(config);
        state.Enemy.Life = 1000;

        var result = DamageToInjuredCalculator.Calculate(state);

        Assert.AreEqual(0, result);
    }
}
