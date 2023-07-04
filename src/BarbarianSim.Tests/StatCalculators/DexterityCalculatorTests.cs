using BarbarianSim.Config;
using BarbarianSim.StatCalculators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BarbarianSim.Tests.StatCalculators;

[TestClass]
public class DexterityCalculatorTests
{
    [TestMethod]
    public void Includes_Base_Value()
    {
        var config = new SimulationConfig();
        config.PlayerSettings.Level = 1;
        var state = new SimulationState(config);

        var result = DexterityCalculator.Calculate(state);

        Assert.AreEqual(8, result);
    }

    [TestMethod]
    public void Includes_Dexterity_Gear_Bonus()
    {
        var config = new SimulationConfig();
        config.PlayerSettings.Level = 1;
        config.Gear.Helm.Dexterity = 42;
        var state = new SimulationState(config);

        var result = DexterityCalculator.Calculate(state);

        Assert.AreEqual(50, result);
    }

    [TestMethod]
    public void Includes_All_Stats_Gear_Bonus()
    {
        var config = new SimulationConfig();
        config.PlayerSettings.Level = 1;
        config.Gear.Helm.AllStats = 17;
        var state = new SimulationState(config);

        var result = DexterityCalculator.Calculate(state);

        Assert.AreEqual(25, result);
    }

    [TestMethod]
    public void Includes_Level_Bonus()
    {
        var config = new SimulationConfig();
        config.PlayerSettings.Level = 100;
        var state = new SimulationState(config);

        var result = DexterityCalculator.Calculate(state);

        Assert.AreEqual(107, result);
    }
}
