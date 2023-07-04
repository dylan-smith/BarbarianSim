using BarbarianSim.Config;
using BarbarianSim.StatCalculators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BarbarianSim.Tests.StatCalculators;

[TestClass]
public class ArmorCalculatorTests
{
    [TestCleanup]
    public void TestCleanup() => BaseStatCalculator.ClearMocks();

    [TestInitialize]
    public void TestInitialize()
    {
        BaseStatCalculator.InjectMock(typeof(StrengthCalculator), new FakeStatCalculator(0.0));
    }

    [TestMethod]
    public void Starts_At_Zero()
    {
        var config = new SimulationConfig();
        var state = new SimulationState(config);

        var result = ArmorCalculator.Calculate(state);

        Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Includes_Gear_Bonus()
    {
        var config = new SimulationConfig();
        config.Gear.Helm.Armor = 42;
        var state = new SimulationState(config);

        var result = ArmorCalculator.Calculate(state);

        Assert.AreEqual(42, result);
    }

    [TestMethod]
    public void Includes_Strength_Bonus()
    {
        var config = new SimulationConfig();
        var state = new SimulationState(config);

        BaseStatCalculator.InjectMock(typeof(StrengthCalculator), new FakeStatCalculator(42.0));

        var result = ArmorCalculator.Calculate(state);

        Assert.AreEqual(42, result);
    }
}
