using BarbarianSim.Config;
using BarbarianSim.StatCalculators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BarbarianSim.Tests.StatCalculators;

[TestClass]
public class VulnerableDamageBonusCalculatorTests
{
    [TestMethod]
    public void Base_Vulnerable_Damage_Is_20()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Enemy.Auras.Add(Aura.Vulnerable);

        var result = VulnerableDamageBonusCalculator.Calculate(state);

        Assert.AreEqual(1.2, result);
    }

    [TestMethod]
    public void Includes_Damage_To_Vulnerable_When_Enemy_Is_Vulnerable()
    {
        var config = new SimulationConfig();
        config.Gear.Helm.VulnerableDamage = 12.0;
        var state = new SimulationState(config);
        state.Enemy.Auras.Add(Aura.Vulnerable);

        var result = VulnerableDamageBonusCalculator.Calculate(state);

        Assert.AreEqual(1.32, result, 0.00001);
    }

    [TestMethod]
    public void Returns_1_When_Enemy_Is_Not_Vulnerable()
    {
        var config = new SimulationConfig();
        config.Gear.Helm.VulnerableDamage = 12.0;
        var state = new SimulationState(config);
        state.Enemy.Auras.Add(Aura.Stun);

        var result = VulnerableDamageBonusCalculator.Calculate(state);

        Assert.AreEqual(1.0, result);
    }
}
