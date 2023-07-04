using BarbarianSim.Config;
using BarbarianSim.StatCalculators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BarbarianSim.Tests.StatCalculators;

[TestClass]
public class DamageToSlowedCalculatorTests
{
    [TestMethod]
    public void Includes_Damage_To_Slowed_When_Enemy_Is_Slowed()
    {
        var config = new SimulationConfig();
        config.Gear.Helm.DamageToSlowed = 12.0;
        var state = new SimulationState(config);
        state.Enemy.Auras.Add(Aura.Slow);

        var result = DamageToSlowedCalculator.Calculate(state);

        Assert.AreEqual(12, result);
    }

    [TestMethod]
    public void Returns_0_When_Enemy_Is_Not_Slowed()
    {
        var config = new SimulationConfig();
        config.Gear.Helm.DamageToSlowed = 12.0;
        var state = new SimulationState(config);
        state.Enemy.Auras.Add(Aura.Stun);

        var result = DamageToSlowedCalculator.Calculate(state);

        Assert.AreEqual(0, result);
    }
}
