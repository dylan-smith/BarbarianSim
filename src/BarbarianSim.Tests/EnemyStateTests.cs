using BarbarianSim.Enums;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests;

public class EnemyStateTests
{
    [Fact]
    public void IsSlowed_Returns_True_When_Slow_Aura()
    {
        var enemyState = new EnemyState();
        enemyState.Auras.Add(Aura.Slow);

        enemyState.IsSlowed().Should().BeTrue();
    }

    [Fact]
    public void IsSlowed_Returns_False_When_No_Slow_Aura()
    {
        var enemyState = new EnemyState();
        enemyState.Auras.Add(Aura.Chill);

        enemyState.IsSlowed().Should().BeFalse();
    }

    [Fact]
    public void IsCrowdControlled_Returns_True_When_Chilled()
    {
        var enemyState = new EnemyState();
        enemyState.Auras.Add(Aura.Chill);

        enemyState.IsCrowdControlled().Should().BeTrue();
    }

    [Fact]
    public void IsCrowdControlled_Returns_True_When_Dazed()
    {
        var enemyState = new EnemyState();
        enemyState.Auras.Add(Aura.Daze);

        enemyState.IsCrowdControlled().Should().BeTrue();
    }

    [Fact]
    public void IsCrowdControlled_Returns_True_When_Feared()
    {
        var enemyState = new EnemyState();
        enemyState.Auras.Add(Aura.Fear);

        enemyState.IsCrowdControlled().Should().BeTrue();
    }

    [Fact]
    public void IsCrowdControlled_Returns_True_When_Frozen()
    {
        var enemyState = new EnemyState();
        enemyState.Auras.Add(Aura.Freeze);

        enemyState.IsCrowdControlled().Should().BeTrue();
    }

    [Fact]
    public void IsCrowdControlled_Returns_True_When_Immobilized()
    {
        var enemyState = new EnemyState();
        enemyState.Auras.Add(Aura.Immobilize);

        enemyState.IsCrowdControlled().Should().BeTrue();
    }

    [Fact]
    public void IsCrowdControlled_Returns_True_When_Knockback()
    {
        var enemyState = new EnemyState();
        enemyState.Auras.Add(Aura.Knockback);

        enemyState.IsCrowdControlled().Should().BeTrue();
    }

    [Fact]
    public void IsCrowdControlled_Returns_True_When_Knockdown()
    {
        var enemyState = new EnemyState();
        enemyState.Auras.Add(Aura.Knockdown);

        enemyState.IsCrowdControlled().Should().BeTrue();
    }

    [Fact]
    public void IsCrowdControlled_Returns_True_When_Slowed()
    {
        var enemyState = new EnemyState();
        enemyState.Auras.Add(Aura.Slow);

        enemyState.IsCrowdControlled().Should().BeTrue();
    }

    [Fact]
    public void IsCrowdControlled_Returns_True_When_Staggered()
    {
        var enemyState = new EnemyState();
        enemyState.Auras.Add(Aura.Stagger);

        enemyState.IsCrowdControlled().Should().BeTrue();
    }

    [Fact]
    public void IsCrowdControlled_Returns_True_When_Stunned()
    {
        var enemyState = new EnemyState();
        enemyState.Auras.Add(Aura.Stun);

        enemyState.IsCrowdControlled().Should().BeTrue();
    }

    [Fact]
    public void IsCrowdControlled_Returns_True_When_Taunted()
    {
        var enemyState = new EnemyState();
        enemyState.Auras.Add(Aura.Taunt);

        enemyState.IsCrowdControlled().Should().BeTrue();
    }

    [Fact]
    public void IsCrowdControlled_Returns_True_When_Tethered()
    {
        var enemyState = new EnemyState();
        enemyState.Auras.Add(Aura.Tether);

        enemyState.IsCrowdControlled().Should().BeTrue();
    }

    [Fact]
    public void IsCrowdControlled_Returns_False_When_No_Crowd_Control_Aura()
    {
        var enemyState = new EnemyState();
        enemyState.Auras.Add(Aura.Vulnerable);

        enemyState.IsCrowdControlled().Should().BeFalse();
    }

    [Fact]
    public void IsInjured_Returns_True_When_Life_Less_Than_35_Percent()
    {
        var enemyState = new EnemyState
        {
            Life = 34,
            MaxLife = 100
        };

        enemyState.IsInjured().Should().BeTrue();
    }

    [Fact]
    public void IsInjured_Returns_False_When_Life_Greater_Than_35_Percent()
    {
        var enemyState = new EnemyState
        {
            Life = 36,
            MaxLife = 100
        };

        enemyState.IsInjured().Should().BeFalse();
    }

    [Fact]
    public void IsVulnerable_Returns_True_When_Vulnerable_Aura()
    {
        var enemyState = new EnemyState();
        enemyState.Auras.Add(Aura.Vulnerable);

        enemyState.IsVulnerable().Should().BeTrue();
    }

    [Fact]
    public void IsVulnerable_Returns_False_When_No_Vulnerable_Aura()
    {
        var enemyState = new EnemyState();
        enemyState.Auras.Add(Aura.Chill);

        enemyState.IsVulnerable().Should().BeFalse();
    }
}
