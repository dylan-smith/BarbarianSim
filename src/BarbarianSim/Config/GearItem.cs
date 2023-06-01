using System;
using System.Collections.Generic;
using System.Linq;

namespace HunterSim
{
    [AttributeUsage(AttributeTargets.Property)]
    public class YamlProperty : Attribute
    {
        public string PropertyName { get; set; }

        public YamlProperty(string propertyName) => PropertyName = propertyName;
    }

    public class GearItem
    {
        public GearType GearType { get; set; }
        [YamlProperty("name")]
        public string Name { get; set; }
        [YamlProperty("mindmg")]
        public double MinDamage { get; set; } // Weapons
        [YamlProperty("maxdmg")]
        public double MaxDamage { get; set; } // Weapons
        [YamlProperty("speed")]
        public double Speed { get; set; } // Weapons
        [YamlProperty("armor")]
        public double Armor { get; set; }
        [YamlProperty("strength")]
        public double Strength { get; set; }
        [YamlProperty("stamina")]
        public double Stamina { get; set; }
        [YamlProperty("agility")]
        public double Agility { get; set; }
        [YamlProperty("intellect")]
        public double Intellect { get; set; }
        [YamlProperty("spirit")]
        public double Spirit { get; set; }
        [YamlProperty("ap")]
        public double AttackPower { get; set; }
        [YamlProperty("rap")]
        public double RangedAttackPower { get; set; }
        [YamlProperty("map")]
        public double MeleeAttackPower { get; set; }
        [YamlProperty("crit")]
        public double CritRating { get; set; }
        [YamlProperty("rangedcrit")]
        public double RangedCritRating { get; set; }
        [YamlProperty("meleecrit")]
        public double MeleeCritRating { get; set; }
        [YamlProperty("hit")]
        public double HitRating { get; set; }
        [YamlProperty("dodge")]
        public double DodgeRating { get; set; }
        [YamlProperty("haste")]
        public double HasteRating { get; set; }
        [YamlProperty("mp5")]
        public double MP5 { get; set; }
        [YamlProperty("defense")]
        public double Defense { get; set; }
        [YamlProperty("fireresist")]
        public double FireResistance { get; set; }
        [YamlProperty("frostresist")]
        public double FrostResistance { get; set; }
        [YamlProperty("arcaneresist")]
        public double ArcaneResistance { get; set; }
        [YamlProperty("natureresist")]
        public double NatureResistance { get; set; }
        [YamlProperty("shadowresist")]
        public double ShadowResistance { get; set; }
        [YamlProperty("threat")]
        public double ThreatDecrease { get; set; }
        [YamlProperty("stealth")]
        public double Stealth { get; set; }
        [YamlProperty("rangedbonusdps")]
        public double RangedBonusDPS { get; set; } // Ammo
        [YamlProperty("rangedbonusdmg")]
        public double RangedBonusDamage { get; set; } // E.g. Sniper Scope
        [YamlProperty("color")]
        public GemColor Color { get; set; } // used by gems
        [YamlProperty("unique")]
        public bool Unique { get; set; }
        [YamlProperty("type")]
        public WeaponType WeaponType { get; set; }
        public IDictionary<WeaponType, int> WeaponSkill = new Dictionary<WeaponType, int>();
        public GearItem Enchant { get; set; }
        public IList<Socket> Sockets = new List<Socket>();
        public GearItem SocketBonus { get; set; }
        [YamlProperty("wowhead")]
        public int Wowhead { get; set; }
        [YamlProperty("source")]
        public GearSource Source { get; set; }
        [YamlProperty("phase")]
        public int Phase { get; set; }

        public bool IsSocketBonusActive() => Sockets.All(x => x.IsColorMatch());

        public double GetStatWithSockets(Func<GearItem, double> statFunc)
        {
            var result = statFunc(this);

            result += Sockets.Where(x => x.Gem != null).Sum(x => statFunc(x.Gem));

            if (SocketBonus != null && IsSocketBonusActive())
            {
                result += statFunc(SocketBonus);
            }

            return result;
        }
    }
}