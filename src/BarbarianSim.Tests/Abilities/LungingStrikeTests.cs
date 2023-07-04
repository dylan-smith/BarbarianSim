using BarbarianSim.Abilities;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BarbarianSim.Tests.Abilities;

[TestClass]
public class LungingStrikeTests
{
    [TestCleanup]
    public void TestCleanup()
    {
        BaseStatCalculator.ClearMocks();
        RandomGenerator.ClearMock();
    }

    [TestInitialize]
    public void TestInitialize()
    {
        BaseStatCalculator.InjectMock(typeof(TotalDamageMultiplierCalculator), new FakeStatCalculator(1.0));
        BaseStatCalculator.InjectMock(typeof(CritChanceCalculator), new FakeStatCalculator(0.0));
        BaseStatCalculator.InjectMock(typeof(CritDamageCalculator), new FakeStatCalculator(1.5));
        BaseStatCalculator.InjectMock(typeof(AttackSpeedCalculator), new FakeStatCalculator(1.0));
    }

    [TestMethod]
    public void CanUse_When_Weapon_On_Cooldown_Returns_False()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Auras.Add(Aura.WeaponCooldown);

        Assert.IsFalse(LungingStrike.CanUse(state));
    }

    [TestMethod]
    public void CanUse_When_Weapon_Not_On_Cooldown_Returns_True()
    {
        var state = new SimulationState(new SimulationConfig());

        Assert.IsTrue(LungingStrike.CanUse(state));
    }

    [TestMethod]
    public void Use_Adds_LungingStrikeEvent_To_Events()
    {
        var state = new SimulationState(new SimulationConfig())
        {
            CurrentTime = 123
        };

        LungingStrike.Use(state);

        Assert.AreEqual(1, state.Events.Count);
        Assert.IsInstanceOfType(state.Events[0], typeof(LungingStrikeEvent));
        Assert.AreEqual(123, state.Events[0].Timestamp);
    }

    [DataTestMethod]
    [DataRow(0, 0.0)]
    [DataRow(1, 0.33)]
    [DataRow(2, 0.36)]
    [DataRow(3, 0.39)]
    [DataRow(4, 0.42)]
    [DataRow(5, 0.45)]
    [DataRow(6, 0.45)]
    public void GetSkillMultiplier_Converts_Skill_Points_To_Correct_Multiplier(int skillPoints, double expectedMultiplier)
    {
        var state = new SimulationState(new SimulationConfig { Skills = { [Skill.LungingStrike] = skillPoints } });

        var result = LungingStrike.GetSkillMultiplier(state);

        Assert.AreEqual(expectedMultiplier, result);
    }

    [TestMethod]
    public void GetSkillMultiplier_Includes_Skill_Points_And_Gear_Bonuses()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.LungingStrike] = 1 },
        });

        state.Config.Gear.Helm.LungingStrike = 2;

        var result = LungingStrike.GetSkillMultiplier(state);

        Assert.AreEqual(0.39, result);
    }

    [TestMethod]
    public void Adds_DamageEvent_To_Queue()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.LungingStrike] = 1 },
        });
        LungingStrike.Weapon = new GearItem { MinDamage = 1, MaxDamage = 1, AttacksPerSecond = 1 };
        RandomGenerator.InjectMock(new FakeRandomGenerator(RollType.CriticalStrike, 1.0));
        var lungingStrikeEvent = new LungingStrikeEvent(123);

        lungingStrikeEvent.ProcessEvent(state);

        Assert.IsTrue(state.Events.Any(e => e is DamageEvent));
        var damageEvent = (DamageEvent)state.Events.Single(e => e is DamageEvent);
        Assert.AreEqual(123, damageEvent.Timestamp);
        Assert.AreEqual(DamageType.Direct, damageEvent.DamageType);
        Assert.AreEqual(0.33, damageEvent.Damage); // 1 [WeaponDmg] * 0.33 [SkillModifier]
    }

    [TestMethod]
    public void Averages_Weapon_Min_Max_Damage()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.LungingStrike] = 1 },
        });
        LungingStrike.Weapon = new GearItem { MinDamage = 300, MaxDamage = 500, AttacksPerSecond = 1 };

        RandomGenerator.InjectMock(new FakeRandomGenerator(RollType.CriticalStrike, 1.0));
        var lungingStrikeEvent = new LungingStrikeEvent(123);

        lungingStrikeEvent.ProcessEvent(state);

        var damageEvent = (DamageEvent)state.Events.Single(e => e is DamageEvent);
        Assert.AreEqual(132, damageEvent.Damage); // 400 [WeaponDmg] * 0.33 [SkillModifier]
    }

    [TestMethod]
    public void Applies_Skill_Multiplier()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.LungingStrike] = 4 },
        });
        LungingStrike.Weapon = new GearItem { MinDamage = 1, MaxDamage = 1, AttacksPerSecond = 1 };

        RandomGenerator.InjectMock(new FakeRandomGenerator(RollType.CriticalStrike, 1.0));
        var lungingStrikeEvent = new LungingStrikeEvent(123);

        lungingStrikeEvent.ProcessEvent(state);

        var damageEvent = (DamageEvent)state.Events.Single(e => e is DamageEvent);
        Assert.AreEqual(0.42, damageEvent.Damage); // 1 [WeaponDmg] * 0.42 [SkillModifier]
    }

    [TestMethod]
    public void Applies_TotalDamageMultiplier_To_Damage()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.LungingStrike] = 1 },
        });
        LungingStrike.Weapon = new GearItem { MinDamage = 1, MaxDamage = 1, AttacksPerSecond = 1 };

        BaseStatCalculator.InjectMock(typeof(TotalDamageMultiplierCalculator), new FakeStatCalculator(4.5));
        RandomGenerator.InjectMock(new FakeRandomGenerator(RollType.CriticalStrike, 1.0));
        var lungingStrikeEvent = new LungingStrikeEvent(123);

        lungingStrikeEvent.ProcessEvent(state);

        var damageEvent = (DamageEvent)state.Events.Single(e => e is DamageEvent);
        Assert.AreEqual(1.485, damageEvent.Damage); // 1 [WeaponDmg] * 4.5 [DmgMultiplier] * 0.33 [SkillModifier]
    }

    [TestMethod]
    public void Critical_Strike_Applies_CritChance_Bonus()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.LungingStrike] = 1 },
        });
        LungingStrike.Weapon = new GearItem { MinDamage = 1, MaxDamage = 1, AttacksPerSecond = 1 };
        BaseStatCalculator.InjectMock(typeof(CritChanceCalculator), new FakeStatCalculator(0.7));
        RandomGenerator.InjectMock(new FakeRandomGenerator(RollType.CriticalStrike, 0.69));
        var lungingStrikeEvent = new LungingStrikeEvent(123);

        lungingStrikeEvent.ProcessEvent(state);

        Assert.IsTrue(state.Events.Any(e => e is DamageEvent));
        var damageEvent = (DamageEvent)state.Events.Single(e => e is DamageEvent);
        Assert.AreEqual(123, damageEvent.Timestamp);
        Assert.AreEqual(DamageType.DirectCrit, damageEvent.DamageType);
        Assert.AreEqual(0.495, damageEvent.Damage); // 1 [WeaponDmg] * 0.33 [SkillModifier] * 1.5 [Crit]
    }

    [TestMethod]
    public void Critical_Strike_Applies_CritDamaage_Bonus()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.LungingStrike] = 1 },
        });
        LungingStrike.Weapon = new GearItem { MinDamage = 1, MaxDamage = 1, AttacksPerSecond = 1 };
        RandomGenerator.InjectMock(new FakeRandomGenerator(RollType.CriticalStrike, 0.0));
        BaseStatCalculator.InjectMock(typeof(CritDamageCalculator), new FakeStatCalculator(3.5));
        var lungingStrikeEvent = new LungingStrikeEvent(123);

        lungingStrikeEvent.ProcessEvent(state);

        var damageEvent = (DamageEvent)state.Events.Single(e => e is DamageEvent);
        Assert.AreEqual(1.155, damageEvent.Damage); // 1 [WeaponDmg] * 0.33 [SkillModifier] * 3.5 [Crit]
    }

    [TestMethod]
    public void Adds_WeaponAuraCooldownCompletedEvent_To_Queue()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.LungingStrike] = 1 },
        });
        LungingStrike.Weapon = new GearItem { MinDamage = 1, MaxDamage = 2, AttacksPerSecond = 1 };

        var lungingStrikeEvent = new LungingStrikeEvent(123);

        lungingStrikeEvent.ProcessEvent(state);

        Assert.IsTrue(state.Events.Any(e => e is WeaponAuraCooldownCompletedEvent));
        Assert.AreEqual(124, state.Events.Single(e => e is WeaponAuraCooldownCompletedEvent).Timestamp);
    }

    [TestMethod]
    public void Considers_Weapon_AttacksPerSecond_When_Creating_WeaponAuraCooldownCompletedEvent()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.LungingStrike] = 1 },
        });
        LungingStrike.Weapon = new GearItem { MinDamage = 1, MaxDamage = 2, AttacksPerSecond = 2 };

        var lungingStrikeEvent = new LungingStrikeEvent(123);

        lungingStrikeEvent.ProcessEvent(state);

        Assert.IsTrue(state.Events.Any(e => e is WeaponAuraCooldownCompletedEvent));
        Assert.AreEqual(123.5, state.Events.Single(e => e is WeaponAuraCooldownCompletedEvent).Timestamp);
    }

    [TestMethod]
    public void Considers_AttackSpeed_Bonuses_When_Creating_WeaponAuraCooldownCompletedEvent()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.LungingStrike] = 1 },
        });
        LungingStrike.Weapon = new GearItem { MinDamage = 1, MaxDamage = 2, AttacksPerSecond = 1 };

        BaseStatCalculator.InjectMock(typeof(AttackSpeedCalculator), new FakeStatCalculator(0.6));

        var lungingStrikeEvent = new LungingStrikeEvent(123);

        lungingStrikeEvent.ProcessEvent(state);

        Assert.IsTrue(state.Events.Any(e => e is WeaponAuraCooldownCompletedEvent));
        Assert.AreEqual(123.6, state.Events.Single(e => e is WeaponAuraCooldownCompletedEvent).Timestamp);
    }
}
