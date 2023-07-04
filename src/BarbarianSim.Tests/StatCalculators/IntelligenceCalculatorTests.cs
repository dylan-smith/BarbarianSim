using BarbarianSim.Config;
using BarbarianSim.StatCalculators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BarbarianSim.Tests.StatCalculators;

[TestClass]
public class IntelligenceCalculatorTests
{
    [TestMethod]
    public void Includes_Base_Value()
    {
        var config = new SimulationConfig();
        config.PlayerSettings.Level = 1;
        var state = new SimulationState(config);

        var result = IntelligenceCalculator.Calculate(state);

        Assert.AreEqual(7, result);
    }

    [TestMethod]
    public void Includes_Intelligence_Gear_Bonus()
    {
        var config = new SimulationConfig();
        config.PlayerSettings.Level = 1;
        config.Gear.Helm.Intelligence = 42;
        var state = new SimulationState(config);

        var result = IntelligenceCalculator.Calculate(state);

        Assert.AreEqual(49, result);
    }

    [TestMethod]
    public void Includes_All_Stats_Gear_Bonus()
    {
        var config = new SimulationConfig();
        config.PlayerSettings.Level = 1;
        config.Gear.Helm.AllStats = 17;
        var state = new SimulationState(config);

        var result = IntelligenceCalculator.Calculate(state);

        Assert.AreEqual(24, result);
    }

    [TestMethod]
    public void Includes_Level_Bonus()
    {
        var config = new SimulationConfig();
        config.PlayerSettings.Level = 100;
        var state = new SimulationState(config);

        var result = IntelligenceCalculator.Calculate(state);

        Assert.AreEqual(106, result);
    }
}
