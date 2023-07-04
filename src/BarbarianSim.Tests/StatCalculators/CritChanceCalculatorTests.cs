using BarbarianSim.Config;
using BarbarianSim.StatCalculators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BarbarianSim.Tests.StatCalculators;

[TestClass]
public class CritChanceCalculatorTests
{
    [TestCleanup]
    public void TestCleanup() => BaseStatCalculator.ClearMocks();

    [TestMethod]
    public void Includes_Stats_From_Gear()
    {
        var config = new SimulationConfig();
        config.Gear.Helm.CritChance = 12.0;
        var state = new SimulationState(config);

        var result = CritChanceCalculator.Calculate(state, DamageType.Physical);

        Assert.AreEqual(0.12, result);
    }

    [TestMethod]
    public void Includes_Crit_Chance_Physical_Against_Elites()
    {
        var state = new SimulationState(new SimulationConfig());

        BaseStatCalculator.InjectMock(typeof(CritChancePhysicalAgainstElitesCalculator), new FakeStatCalculator(12.0));

        var result = CritChanceCalculator.Calculate(state, DamageType.Physical);

        Assert.AreEqual(0.12, result);
    }
}
