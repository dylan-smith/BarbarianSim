using BarbarianSim.Config;
using BarbarianSim.StatCalculators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BarbarianSim.Tests.StatCalculators;

[TestClass]
public class DamageToCloseCalculatorTests
{
    [TestMethod]
    public void Includes_Stats_From_Gear()
    {
        var config = new SimulationConfig();
        config.Gear.Helm.DamageToClose = 12.0;
        var state = new SimulationState(config);

        var result = DamageToCloseCalculator.Calculate(state);

        Assert.AreEqual(12, result);
    }
}
