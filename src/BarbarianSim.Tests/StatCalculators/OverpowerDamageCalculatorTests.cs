using BarbarianSim.Config;
using BarbarianSim.StatCalculators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BarbarianSim.Tests.StatCalculators;

[TestClass]
public class OverpowerDamageCalculatorTests
{
    [TestCleanup]
    public void TestCleanup() => BaseStatCalculator.ClearMocks();

    [TestMethod]
    public void Returns_1_By_Default()
    {
        var state = new SimulationState(new SimulationConfig());
        BaseStatCalculator.InjectMock(typeof(WillpowerCalculator), new FakeStatCalculator(0.0));

        var result = OverpowerDamageCalculator.Calculate(state);

        Assert.AreEqual(1.0, result);
    }

    [TestMethod]
    public void Includes_OverpowerDamage_Gear_Bonus()
    {
        var config = new SimulationConfig();
        config.Gear.Helm.OverpowerDamage = 42;
        var state = new SimulationState(config);
        BaseStatCalculator.InjectMock(typeof(WillpowerCalculator), new FakeStatCalculator(0.0));

        var result = OverpowerDamageCalculator.Calculate(state);

        Assert.AreEqual(1.42, result);
    }

    [TestMethod]
    public void Includes_Willpower_Bonus()
    {
        var state = new SimulationState(new SimulationConfig());
        BaseStatCalculator.InjectMock(typeof(WillpowerCalculator), new FakeStatCalculator(400.0));

        var result = OverpowerDamageCalculator.Calculate(state);

        Assert.AreEqual(2.0, result);
    }
}
