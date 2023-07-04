using BarbarianSim.Config;
using BarbarianSim.StatCalculators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BarbarianSim.Tests.StatCalculators;

[TestClass]
public class PhysicalDamageCalculatorTests
{
    [TestMethod]
    public void Includes_Stats_From_Gear()
    {
        var config = new SimulationConfig();
        config.Gear.Helm.PhysicalDamage = 12.0;
        var state = new SimulationState(config);

        var result = PhysicalDamageCalculator.Calculate(state, DamageType.Physical);

        Assert.AreEqual(12, result);
    }

    [TestMethod]
    public void Returns_0_For_Non_Physical_Damage_Type()
    {
        var config = new SimulationConfig();
        config.Gear.Helm.PhysicalDamage = 12.0;
        var state = new SimulationState(config);

        var result = PhysicalDamageCalculator.Calculate(state, DamageType.Direct);

        Assert.AreEqual(0, result);
    }
}
