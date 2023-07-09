using BarbarianSim.Enums;

namespace BarbarianSim.Config;

[AttributeUsage(AttributeTargets.Property)]
public sealed class YamlPropertyAttribute : Attribute
{
    public string PropertyName { get; set; }

    public YamlPropertyAttribute(string propertyName) => PropertyName = propertyName;
}

public class GearItem
{
    [YamlPropertyAttribute("armor")]
    public int Armor { get; set; }
    public double CooldownReduction { get; set; }
    public double PoisonResistance { get; set; }
    public double TotalArmor { get; set; }
    public int MaxLife { get; set; }
    public int MaxFury { get; set; }
    public double PhysicalDamage { get; set; }
    public double CritChancePhysicalAgainstElites { get; set; }
    public double DamageReductionFromBleeding { get; set; }
    public int Thorns { get; set; }
    public double AttackSpeed { get; set; }
    public double LuckyHitChance { get; set; }
    public double NonPhysicalDamage { get; set; }
    public double PotionSpeedWhileInjured { get; set; }
    public int WarCry { get; set; }
    public int Whirlwind { get; set; }
    public int LungingStrike { get; set; }
    public int PressurePoint { get; set; }
    public int RallyingCry { get; set; }
    public int ChallengingShout { get; set; }
    public int IronSkin { get; set; }
    public double Dodge { get; set; }
    public double DamageReduction { get; set; }
    public double DamageReductionFromClose { get; set; }
    public double DamageReductionWhileInjured { get; set; }
    public double AttacksReduceEvadeCooldown { get; set; }
    public double MovementAfterKillingElite { get; set; }
    public double MovementSpeed { get; set; }
    public double DodgeAgainstDistant { get; set; }
    public int Dexterity { get; set; }
    public int DPS { get; set; }
    public Expertise Expertise { get; set; }
    [YamlPropertyAttribute("min-damage")]
    public int MinDamage { get; set; }
    [YamlPropertyAttribute("max-damage")]
    public int MaxDamage { get; set; }
    public double AttacksPerSecond { get; set; }
    public double OverpowerDamage { get; set; }
    public double DamageToClose { get; set; }
    public double CoreDamage { get; set; }
    public int Strength { get; set; }
    public double CritDamage { get; set; }
    public double VulnerableDamage { get; set; }
    public double DamageToInjured { get; set; }
    public int AllStats { get; set; }
    public double DamageToSlowed { get; set; }
    public double DamageToCrowdControlled { get; set; }
    public double ResistanceToAll { get; set; }
    public double DamageReductionFromDistant { get; set; }
    public double HealingReceived { get; set; }
    public double FuryCostReduction { get; set; }
    public double LightningResistance { get; set; }
    public double FireResistance { get; set; }
    public double ResourceGeneration { get; set; }
    public double CritChance { get; set; }
    public double ColdResistance { get; set; }
    public double DamageReductionWhileFortified { get; set; }
    public double CritDamageVulnerable { get; set; }
    public double Willpower { get; set; }
    public double Intelligence { get; set; }

    public IList<Gem> Gems { get; init; } = new List<Gem>();
    public Aspect Aspect { get; set; }

    public double GetStatWithGems(Func<GearItem, double> statFunc) => statFunc(this) + Gems.Sum(x => statFunc(x));

    public double GetStatWithGemsMultiplied(Func<GearItem, double> statFunc) => statFunc(this) * Gems.Multiply(x => statFunc(x));
}
