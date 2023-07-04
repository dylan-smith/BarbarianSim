using BarbarianSim.Config;
using BarbarianSim.StatCalculators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BarbarianSim.Tests.StatCalculators;

[TestClass]
public class AdditiveDamageBonusCalculatorTests
{
    [TestCleanup]
    public void TestCleanup() => BaseStatCalculator.ClearMocks();

    [TestInitialize]
    public void TestInitialize()
    {
        BaseStatCalculator.InjectMock(typeof(PhysicalDamageCalculator), new FakeStatCalculator(0.0));
        BaseStatCalculator.InjectMock(typeof(DamageToCloseCalculator), new FakeStatCalculator(0.0));
        BaseStatCalculator.InjectMock(typeof(DamageToInjuredCalculator), new FakeStatCalculator(0.0));
        BaseStatCalculator.InjectMock(typeof(DamageToSlowedCalculator), new FakeStatCalculator(0.0));
        BaseStatCalculator.InjectMock(typeof(DamageToCrowdControlledCalculator), new FakeStatCalculator(0.0));
    }

    [TestMethod]
    public void Returns_1_When_No_Additive_Damage_Bonuses()
    {
        var state = new SimulationState(new SimulationConfig());

        var result = AdditiveDamageBonusCalculator.Calculate(state, DamageType.Physical);

        Assert.AreEqual(1.0, result);
    }

    [TestMethod]
    public void Includes_Physical_Damage()
    {
        var state = new SimulationState(new SimulationConfig());

        BaseStatCalculator.InjectMock(typeof(PhysicalDamageCalculator), new FakeStatCalculator(12.0));

        var result = AdditiveDamageBonusCalculator.Calculate(state, DamageType.Physical);

        Assert.AreEqual(1.12, result);
    }

    [TestMethod]
    public void Includes_Damage_To_Close()
    {
        var state = new SimulationState(new SimulationConfig());

        BaseStatCalculator.InjectMock(typeof(DamageToCloseCalculator), new FakeStatCalculator(12.0));

        var result = AdditiveDamageBonusCalculator.Calculate(state, DamageType.Physical);

        Assert.AreEqual(1.12, result);
    }

    [TestMethod]
    public void Includes_Damage_To_Injured()
    {
        var state = new SimulationState(new SimulationConfig());

        BaseStatCalculator.InjectMock(typeof(DamageToInjuredCalculator), new FakeStatCalculator(12.0));

        var result = AdditiveDamageBonusCalculator.Calculate(state, DamageType.Physical);

        Assert.AreEqual(1.12, result);
    }

    [TestMethod]
    public void Includes_Damage_To_Slowed()
    {
        var state = new SimulationState(new SimulationConfig());

        BaseStatCalculator.InjectMock(typeof(DamageToSlowedCalculator), new FakeStatCalculator(12.0));

        var result = AdditiveDamageBonusCalculator.Calculate(state, DamageType.Physical);

        Assert.AreEqual(1.12, result);
    }

    [TestMethod]
    public void Includes_Damage_To_Crowd_Controlled()
    {
        var state = new SimulationState(new SimulationConfig());

        BaseStatCalculator.InjectMock(typeof(DamageToCrowdControlledCalculator), new FakeStatCalculator(12.0));

        var result = AdditiveDamageBonusCalculator.Calculate(state, DamageType.Physical);

        Assert.AreEqual(1.12, result);
    }

    [TestMethod]
    public void Adds_All_Additive_Damage_Bonuses()
    {
        var state = new SimulationState(new SimulationConfig());

        BaseStatCalculator.InjectMock(typeof(PhysicalDamageCalculator), new FakeStatCalculator(12.0));
        BaseStatCalculator.InjectMock(typeof(DamageToCloseCalculator), new FakeStatCalculator(12.0));
        BaseStatCalculator.InjectMock(typeof(DamageToInjuredCalculator), new FakeStatCalculator(12.0));
        BaseStatCalculator.InjectMock(typeof(DamageToSlowedCalculator), new FakeStatCalculator(12.0));
        BaseStatCalculator.InjectMock(typeof(DamageToCrowdControlledCalculator), new FakeStatCalculator(12.0));

        var result = AdditiveDamageBonusCalculator.Calculate(state, DamageType.Physical);

        Assert.AreEqual(1.6, result);
    }
}
