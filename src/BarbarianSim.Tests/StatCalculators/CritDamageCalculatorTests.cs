using BarbarianSim.Config;
using BarbarianSim.StatCalculators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BarbarianSim.Tests.StatCalculators;

[TestClass]
public class CritDamageCalculatorTests
{
    [TestMethod]
    public void Includes_Stats_From_Gear()
    {
        var config = new SimulationConfig();
        config.Gear.Helm.CritDamage = 12.0;
        var state = new SimulationState(config);

        var result = CritDamageCalculator.Calculate(state);

        // 1.5 base + 0.12 from helm == 1.62
        Assert.AreEqual(1.62, result);
    }

    [TestMethod]
    public void Base_Crit_Is_50()
    {
        var config = new SimulationConfig();
        var state = new SimulationState(config);

        var result = CritDamageCalculator.Calculate(state);

        Assert.AreEqual(1.50, result);
    }
}
