using BarbarianSim.Config;
using BarbarianSim.StatCalculators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BarbarianSim.Tests.StatCalculators;

[TestClass]
public class DodgeCalculatorTests
{
    [TestCleanup]
    public void TestCleanup() => BaseStatCalculator.ClearMocks();

    [TestMethod]
    public void Returns_0_By_Default()
    {
        var state = new SimulationState(new SimulationConfig());
        BaseStatCalculator.InjectMock(typeof(DexterityCalculator), new FakeStatCalculator(0.0));

        var result = DodgeCalculator.Calculate(state);

        Assert.AreEqual(0.0, result);
    }

    [TestMethod]
    public void Includes_Dodge_Gear_Bonus()
    {
        var config = new SimulationConfig();
        config.Gear.Helm.Dodge = 42;
        var state = new SimulationState(config);
        BaseStatCalculator.InjectMock(typeof(DexterityCalculator), new FakeStatCalculator(0.0));

        var result = DodgeCalculator.Calculate(state);

        Assert.AreEqual(0.42, result);
    }

    [TestMethod]
    public void Includes_Dexterity_Bonus()
    {
        var state = new SimulationState(new SimulationConfig());
        BaseStatCalculator.InjectMock(typeof(DexterityCalculator), new FakeStatCalculator(400.0));

        var result = DodgeCalculator.Calculate(state);

        Assert.AreEqual(0.04, result);
    }
}
