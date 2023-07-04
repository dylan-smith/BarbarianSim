using BarbarianSim.Config;
using BarbarianSim.StatCalculators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BarbarianSim.Tests.StatCalculators;

[TestClass]
public class AttackSpeedCalculatorTests
{
    [TestMethod]
    public void Includes_Stats_From_Gear()
    {
        var config = new SimulationConfig();
        config.Gear.Helm.AttackSpeed = 60.0;
        var state = new SimulationState(config);

        var result = AttackSpeedCalculator.Calculate(state);

        // 1 / 1.60 == 0.625
        Assert.AreEqual(0.625, result);
    }
}
