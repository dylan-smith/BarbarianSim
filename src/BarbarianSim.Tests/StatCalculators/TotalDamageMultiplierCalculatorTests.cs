using BarbarianSim.Config;
using BarbarianSim.StatCalculators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BarbarianSim.Tests.StatCalculators;

[TestClass]
public class TotalDamageMultiplierCalculatorTests
{
    [TestCleanup]
    public void TestCleanup() => BaseStatCalculator.ClearMocks();

    [TestInitialize]
    public void TestInitialize()
    {
        BaseStatCalculator.InjectMock(typeof(AdditiveDamageBonusCalculator), new FakeStatCalculator(1.0));
        BaseStatCalculator.InjectMock(typeof(VulnerableDamageBonusCalculator), new FakeStatCalculator(1.0));
        BaseStatCalculator.InjectMock(typeof(StrengthCalculator), new FakeStatCalculator(0.0));
    }

    [TestMethod]
    public void Returns_1_When_No_Extra_Bonuses()
    {
        var state = new SimulationState(new SimulationConfig());

        var result = TotalDamageMultiplierCalculator.Calculate(state, DamageType.Physical);

        Assert.AreEqual(1.0, result);
    }

    [TestMethod]
    public void Includes_Additive_Damage()
    {
        var state = new SimulationState(new SimulationConfig());

        BaseStatCalculator.InjectMock(typeof(AdditiveDamageBonusCalculator), new FakeStatCalculator(1.12));

        var result = TotalDamageMultiplierCalculator.Calculate(state, DamageType.Physical);

        Assert.AreEqual(1.12, result);
    }

    [TestMethod]
    public void Includes_Vulnerable_Damage()
    {
        var state = new SimulationState(new SimulationConfig());

        BaseStatCalculator.InjectMock(typeof(VulnerableDamageBonusCalculator), new FakeStatCalculator(1.12));

        var result = TotalDamageMultiplierCalculator.Calculate(state, DamageType.Physical);

        Assert.AreEqual(1.12, result);
    }

    [TestMethod]
    public void Includes_Strength_Bonus()
    {
        var state = new SimulationState(new SimulationConfig());

        BaseStatCalculator.InjectMock(typeof(StrengthCalculator), new FakeStatCalculator(42.0));

        var result = TotalDamageMultiplierCalculator.Calculate(state, DamageType.Physical);

        Assert.AreEqual(1.042, result);
    }

    [TestMethod]
    public void Multiplies_All_Bonuses_Together()
    {
        var state = new SimulationState(new SimulationConfig());

        BaseStatCalculator.InjectMock(typeof(AdditiveDamageBonusCalculator), new FakeStatCalculator(1.2));
        BaseStatCalculator.InjectMock(typeof(VulnerableDamageBonusCalculator), new FakeStatCalculator(1.2));
        BaseStatCalculator.InjectMock(typeof(StrengthCalculator), new FakeStatCalculator(50.0));

        var result = TotalDamageMultiplierCalculator.Calculate(state, DamageType.Physical);

        Assert.AreEqual(1.512, result);
    }
}
