using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Schema;
using System;
using System.Linq;

namespace HunterSim.Tests
{
    [TestClass]
    public class GearItemFactoryTests
    {
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void EmptyYaml()
        {
            var yaml = "";
            GearItemFactory.LoadGearItem(yaml, GearType.Head);
        }

        [TestMethod]
        public void MinimalYaml()
        {
            var yaml = "name: Beast Lord Helm";
            var result = GearItemFactory.LoadGearItem(yaml, GearType.Head);
            Assert.AreEqual("Beast Lord Helm", result.Name);
        }

        [TestMethod]
        public void BeastLordCuirass()
        {
            var yaml = @"
name: Beast Lord Cuirass
armor: 652
agility: 20
stamina: 30
intellect: 24
ap: 40
mp5: 4
sockets:
  red: 2
  blue: 1
  bonus:
    agility: 4
wowhead: 22436
source: dungeon
phase: 1
# Part of Beast Lord Armor set";

            var result = GearItemFactory.LoadGearItem(yaml, GearType.Head);
            Assert.AreEqual(GearType.Head, result.GearType);
            Assert.AreEqual("Beast Lord Cuirass", result.Name);
            Assert.AreEqual(652, result.Armor);
            Assert.AreEqual(20, result.Agility);
            Assert.AreEqual(30, result.Stamina);
            Assert.AreEqual(24, result.Intellect);
            Assert.AreEqual(40, result.AttackPower);
            Assert.AreEqual(4, result.MP5);
            Assert.AreEqual(2, result.Sockets.Count(s => s.Color == SocketColor.Red));
            Assert.AreEqual(1, result.Sockets.Count(s => s.Color == SocketColor.Blue));
            Assert.AreEqual(4, result.SocketBonus.Agility);
            Assert.AreEqual(22436, result.Wowhead);
            Assert.AreEqual(GearSource.Dungeon, result.Source);
            Assert.AreEqual(1, result.Phase);
        }

        [TestMethod]
        public void DeadlyFireOpal()
        {
            var yaml = @"
name: Deadly Fire Opal
color: orange
ap: 8
crit: 5
unique: true
wowhead: 30582
source: heroic
phase: 1";

            var result = GearItemFactory.LoadGearItem(yaml, GearType.Gem);
            Assert.AreEqual(GearType.Gem, result.GearType);
            Assert.AreEqual("Deadly Fire Opal", result.Name);
            Assert.AreEqual(GemColor.Orange, result.Color);
            Assert.AreEqual(8, result.AttackPower);
            Assert.AreEqual(5, result.CritRating);
            Assert.IsTrue(result.Unique);
            Assert.AreEqual(30582, result.Wowhead);
            Assert.AreEqual(GearSource.Heroic, result.Source);
            Assert.AreEqual(1, result.Phase);
        }

        [TestMethod]
        [ExpectedException(typeof(JSchemaValidationException))]
        public void InvalidPropertyInYaml()
        {
            var yaml = "name: Beast Lord Helm\nattackpower: 12";
            GearItemFactory.LoadGearItem(yaml, GearType.Head);
        }

        [TestMethod]
        public void ValidateAllYamlFiles()
        {
            var _ = GearItemFactory.AllGear;
        }

        [TestMethod]
        public void EverythingYaml()
        {
            var yaml = @"
name: Test Helmet
mindmg: 1
maxdmg: 2
speed: 3
armor: 4

fireresist: 5
frostresist: 6
arcaneresist: 7
natureresist: 8
shadowresist: 9


strength: 10
stamina: 11
agility: 12
# random comment
intellect: 13
spirit: 14
ap: 15
rap: 16
map: 17
crit: 18
hit: 19
dodge: 20
haste: 21
mp5: 22
defense: 23

threat: 24
stealth: 25
rangedbonusdps: 26
rangedbonusdmg: 27
type: dagger
bow-skill: 28
crossbow-skill: 29
dagger-skill: 30
fist-skill: 31
gun-skill: 32
axe-skill: 33
mace-skill: 34
sword-skill: 35
polearm-skill: 36
staff-skill: 37
thrown-skill: 38
two-handed-axe-skill: 39
two-handed-mace-skill: 40
two-handed-sword-skill: 41
wand-skill: 42

sockets:
  red: 3
  blue: 7
  yellow: 1
  meta: 1
  bonus:
    gun-skill: 3
    spirit: 7
    rangedbonusdmg: 17
wowhead: 123987
phase: 1
source: gruul
";

            var result = GearItemFactory.LoadGearItem(yaml, GearType.Head);

            Assert.AreEqual("Test Helmet", result.Name);
            Assert.AreEqual(1.0, result.MinDamage);
            Assert.AreEqual(2.0, result.MaxDamage);
            Assert.AreEqual(3.0, result.Speed);
            Assert.AreEqual(4.0, result.Armor);
            Assert.AreEqual(5.0, result.FireResistance);
            Assert.AreEqual(6.0, result.FrostResistance);
            Assert.AreEqual(7.0, result.ArcaneResistance);
            Assert.AreEqual(8.0, result.NatureResistance);
            Assert.AreEqual(9.0, result.ShadowResistance);
            Assert.AreEqual(10.0, result.Strength);
            Assert.AreEqual(11.0, result.Stamina);
            Assert.AreEqual(12.0, result.Agility);
            Assert.AreEqual(13.0, result.Intellect);
            Assert.AreEqual(14.0, result.Spirit);
            Assert.AreEqual(15.0, result.AttackPower);
            Assert.AreEqual(16.0, result.RangedAttackPower);
            Assert.AreEqual(17.0, result.MeleeAttackPower);
            Assert.AreEqual(18.0, result.CritRating);
            Assert.AreEqual(19.0, result.HitRating);
            Assert.AreEqual(20.0, result.DodgeRating);
            Assert.AreEqual(21.0, result.HasteRating);
            Assert.AreEqual(22.0, result.MP5);
            Assert.AreEqual(23.0, result.Defense);

            Assert.AreEqual(-0.24, result.ThreatDecrease);
            Assert.AreEqual(25.0, result.Stealth);
            Assert.AreEqual(26.0, result.RangedBonusDPS);
            Assert.AreEqual(27.0, result.RangedBonusDamage);
            Assert.AreEqual(WeaponType.Dagger, result.WeaponType);
            Assert.AreEqual(28.0, result.WeaponSkill[WeaponType.Bow]);
            Assert.AreEqual(29.0, result.WeaponSkill[WeaponType.Crossbow]);
            Assert.AreEqual(30.0, result.WeaponSkill[WeaponType.Dagger]);
            Assert.AreEqual(31.0, result.WeaponSkill[WeaponType.Fist]);
            Assert.AreEqual(32.0, result.WeaponSkill[WeaponType.Gun]);
            Assert.AreEqual(33.0, result.WeaponSkill[WeaponType.OneHandedAxe]);
            Assert.AreEqual(34.0, result.WeaponSkill[WeaponType.OneHandedMace]);
            Assert.AreEqual(35.0, result.WeaponSkill[WeaponType.OneHandedSword]);
            Assert.AreEqual(36.0, result.WeaponSkill[WeaponType.Polearm]);
            Assert.AreEqual(37.0, result.WeaponSkill[WeaponType.Staff]);
            Assert.AreEqual(38.0, result.WeaponSkill[WeaponType.Thrown]);
            Assert.AreEqual(39.0, result.WeaponSkill[WeaponType.TwoHandedAxe]);
            Assert.AreEqual(40.0, result.WeaponSkill[WeaponType.TwoHandedMace]);
            Assert.AreEqual(41.0, result.WeaponSkill[WeaponType.TwoHandedSword]);
            Assert.AreEqual(42.0, result.WeaponSkill[WeaponType.Wand]);
            
            Assert.AreEqual(123987, result.Wowhead);
            Assert.AreEqual(1, result.Phase);
            Assert.AreEqual(GearSource.Gruul, result.Source);

            Assert.AreEqual(12, result.Sockets.Count);
            Assert.AreEqual(3, result.Sockets.Count(s => s.Color == SocketColor.Red));
            Assert.AreEqual(7, result.Sockets.Count(s => s.Color == SocketColor.Blue));
            Assert.AreEqual(1, result.Sockets.Count(s => s.Color == SocketColor.Yellow));
            Assert.AreEqual(1, result.Sockets.Count(s => s.Color == SocketColor.Meta));

            Assert.AreEqual(3, result.SocketBonus.WeaponSkill[WeaponType.Gun]);
            Assert.AreEqual(7, result.SocketBonus.Spirit);
            Assert.AreEqual(17, result.SocketBonus.RangedBonusDamage);
        }
    }
}
