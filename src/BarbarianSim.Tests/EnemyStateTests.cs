using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BarbarianSim.Tests;

[TestClass]
public class EnemyStateTests
{
    [TestMethod]
    public void IsSlowed_Returns_True_When_Slow_Aura()
    {
        var enemyState = new EnemyState();
        enemyState.Auras.Add(Aura.Slow);

        Assert.IsTrue(enemyState.IsSlowed());
    }

    [TestMethod]
    public void IsSlowed_Returns_False_When_No_Slow_Aura()
    {
        var enemyState = new EnemyState();
        enemyState.Auras.Add(Aura.Chill);

        Assert.IsFalse(enemyState.IsSlowed());
    }

    [TestMethod]
    public void IsCrowdControlled_Returns_True_When_Chilled()
    {
        var enemyState = new EnemyState();
        enemyState.Auras.Add(Aura.Chill);

        Assert.IsTrue(enemyState.IsCrowdControlled());
    }

    [TestMethod]
    public void IsCrowdControlled_Returns_True_When_Dazed()
    {
        var enemyState = new EnemyState();
        enemyState.Auras.Add(Aura.Daze);

        Assert.IsTrue(enemyState.IsCrowdControlled());
    }

    [TestMethod]
    public void IsCrowdControlled_Returns_True_When_Feared()
    {
        var enemyState = new EnemyState();
        enemyState.Auras.Add(Aura.Fear);

        Assert.IsTrue(enemyState.IsCrowdControlled());
    }

    [TestMethod]
    public void IsCrowdControlled_Returns_True_When_Frozen()
    {
        var enemyState = new EnemyState();
        enemyState.Auras.Add(Aura.Freeze);

        Assert.IsTrue(enemyState.IsCrowdControlled());
    }

    [TestMethod]
    public void IsCrowdControlled_Returns_True_When_Immobilized()
    {
        var enemyState = new EnemyState();
        enemyState.Auras.Add(Aura.Immobilize);

        Assert.IsTrue(enemyState.IsCrowdControlled());
    }

    [TestMethod]
    public void IsCrowdControlled_Returns_True_When_Knockback()
    {
        var enemyState = new EnemyState();
        enemyState.Auras.Add(Aura.Knockback);

        Assert.IsTrue(enemyState.IsCrowdControlled());
    }

    [TestMethod]
    public void IsCrowdControlled_Returns_True_When_Knockdown()
    {
        var enemyState = new EnemyState();
        enemyState.Auras.Add(Aura.Knockdown);

        Assert.IsTrue(enemyState.IsCrowdControlled());
    }

    [TestMethod]
    public void IsCrowdControlled_Returns_True_When_Slowed()
    {
        var enemyState = new EnemyState();
        enemyState.Auras.Add(Aura.Slow);

        Assert.IsTrue(enemyState.IsCrowdControlled());
    }

    [TestMethod]
    public void IsCrowdControlled_Returns_True_When_Staggered()
    {
        var enemyState = new EnemyState();
        enemyState.Auras.Add(Aura.Stagger);

        Assert.IsTrue(enemyState.IsCrowdControlled());
    }

    [TestMethod]
    public void IsCrowdControlled_Returns_True_When_Stunned()
    {
        var enemyState = new EnemyState();
        enemyState.Auras.Add(Aura.Stun);

        Assert.IsTrue(enemyState.IsCrowdControlled());
    }

    [TestMethod]
    public void IsCrowdControlled_Returns_True_When_Taunted()
    {
        var enemyState = new EnemyState();
        enemyState.Auras.Add(Aura.Taunt);

        Assert.IsTrue(enemyState.IsCrowdControlled());
    }

    [TestMethod]
    public void IsCrowdControlled_Returns_True_When_Tethered()
    {
        var enemyState = new EnemyState();
        enemyState.Auras.Add(Aura.Tether);

        Assert.IsTrue(enemyState.IsCrowdControlled());
    }

    [TestMethod]
    public void IsCrowdControlled_Returns_False_When_No_Crowd_Control_Aura()
    {
        var enemyState = new EnemyState();
        enemyState.Auras.Add(Aura.Vulnerable);

        Assert.IsFalse(enemyState.IsCrowdControlled());
    }

    [TestMethod]
    public void IsInjured_Returns_True_When_Life_Less_Than_35_Percent()
    {
        var enemyState = new EnemyState();
        enemyState.Life = 34;
        enemyState.MaxLife = 100;

        Assert.IsTrue(enemyState.IsInjured());
    }

    [TestMethod]
    public void IsInjured_Returns_False_When_Life_Greater_Than_35_Percent()
    {
        var enemyState = new EnemyState();
        enemyState.Life = 36;
        enemyState.MaxLife = 100;

        Assert.IsFalse(enemyState.IsInjured());
    }

    [TestMethod]
    public void IsVulnerable_Returns_True_When_Vulnerable_Aura()
    {
        var enemyState = new EnemyState();
        enemyState.Auras.Add(Aura.Vulnerable);

        Assert.IsTrue(enemyState.IsVulnerable());
    }

    [TestMethod]
    public void IsVulnerable_Returns_False_When_No_Vulnerable_Aura()
    {
        var enemyState = new EnemyState();
        enemyState.Auras.Add(Aura.Chill);

        Assert.IsFalse(enemyState.IsVulnerable());
    }
}
