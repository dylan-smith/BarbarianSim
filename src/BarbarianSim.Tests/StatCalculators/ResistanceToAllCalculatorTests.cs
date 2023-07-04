using BarbarianSim.Config;
using BarbarianSim.StatCalculators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BarbarianSim.Tests.StatCalculators;

[TestClass]
public class ResistanceToAllCalculatorTests
{
    [TestCleanup]
    public void TestCleanup() => BaseStatCalculator.ClearMocks();

    [TestMethod]
    public void Returns_0_By_Default()
    {
        var state = new SimulationState(new SimulationConfig());
        BaseStatCalculator.InjectMock(typeof(IntelligenceCalculator), new FakeStatCalculator(0.0));

        var result = ResistanceToAllCalculator.Calculate(state);

        Assert.AreEqual(0.0, result);
    }

    [TestMethod]
    public void Includes_ResistanceToAll_Gear_Bonus()
    {
        var config = new SimulationConfig();
        config.Gear.Helm.ResistanceToAll = 42;
        var state = new SimulationState(config);
        BaseStatCalculator.InjectMock(typeof(IntelligenceCalculator), new FakeStatCalculator(0.0));

        var result = ResistanceToAllCalculator.Calculate(state);

        Assert.AreEqual(0.42, result);
    }

    [TestMethod]
    public void Includes_Intelligence_Bonus()
    {
        var state = new SimulationState(new SimulationConfig());
        BaseStatCalculator.InjectMock(typeof(IntelligenceCalculator), new FakeStatCalculator(400.0));

        var result = ResistanceToAllCalculator.Calculate(state);

        Assert.AreEqual(0.2, result);
    }
}
