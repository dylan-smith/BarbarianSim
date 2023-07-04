using BarbarianSim.Config;
using BarbarianSim.StatCalculators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BarbarianSim.Tests.StatCalculators;

[TestClass]
public class CritChancePhysicalAgainstElitesCalculatorTests
{
    [TestCleanup]
    public void TestCleanup() => BaseStatCalculator.ClearMocks();

    [TestMethod]
    public void Includes_Stats_From_Gear()
    {
        var config = new SimulationConfig();
        config.Gear.Helm.CritChancePhysicalAgainstElites = 12.0;
        config.EnemySettings.IsElite = true;
        var state = new SimulationState(config);

        var result = CritChancePhysicalAgainstElitesCalculator.Calculate(state, DamageType.Physical);

        Assert.AreEqual(0.12, result);
    }

    [TestMethod]
    public void Returns_0_For_Non_Elites()
    {
        var config = new SimulationConfig();
        config.Gear.Helm.CritChancePhysicalAgainstElites = 12.0;
        config.EnemySettings.IsElite = false;
        var state = new SimulationState(config);

        var result = CritChancePhysicalAgainstElitesCalculator.Calculate(state, DamageType.Physical);

        Assert.AreEqual(0.0, result);
    }

    [TestMethod]
    public void Returns_0_For_Non_Physical_Damage()
    {
        var config = new SimulationConfig();
        config.Gear.Helm.CritChancePhysicalAgainstElites = 12.0;
        config.EnemySettings.IsElite = false;
        var state = new SimulationState(config);

        var result = CritChancePhysicalAgainstElitesCalculator.Calculate(state, DamageType.Direct);

        Assert.AreEqual(0.0, result);
    }
}
