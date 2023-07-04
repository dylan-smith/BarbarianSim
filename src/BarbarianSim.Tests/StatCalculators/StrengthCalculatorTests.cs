using BarbarianSim.Config;
using BarbarianSim.StatCalculators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BarbarianSim.Tests.StatCalculators;

[TestClass]
public class StrengthCalculatorTests
{
    [TestMethod]
    public void Includes_Base_Value()
    {
        var config = new SimulationConfig();
        config.PlayerSettings.Level = 1;
        var state = new SimulationState(config);

        var result = StrengthCalculator.Calculate(state);

        Assert.AreEqual(10, result);
    }

    [TestMethod]
    public void Includes_Strength_Gear_Bonus()
    {
        var config = new SimulationConfig();
        config.PlayerSettings.Level = 1;
        config.Gear.Helm.Strength = 42;
        var state = new SimulationState(config);

        var result = StrengthCalculator.Calculate(state);

        Assert.AreEqual(52, result);
    }
    
    [TestMethod]
    public void Includes_All_Stats_Gear_Bonus()
    {
        var config = new SimulationConfig();
        config.PlayerSettings.Level = 1;
        config.Gear.Helm.AllStats = 17;
        var state = new SimulationState(config);

        var result = StrengthCalculator.Calculate(state);

        Assert.AreEqual(27, result);
    }

    [TestMethod]
    public void Includes_Level_Bonus()
    {
        var config = new SimulationConfig();
        config.PlayerSettings.Level = 100;
        var state = new SimulationState(config);

        var result = StrengthCalculator.Calculate(state);

        Assert.AreEqual(109, result);
    }
}
