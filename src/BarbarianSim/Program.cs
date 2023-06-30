using BarbarianSim.Abilities;
using BarbarianSim.Config;
using BarbarianSim.Aspects;
using BarbarianSim.Gems;

namespace BarbarianSim
{
    internal class Program
    {
        private static void Main()
        {
            var config = new SimulationConfig();

            config.EnemySettings.Life = 500000;
            config.EnemySettings.NumberOfEnemies = 1;
            config.EnemySettings.Level = 100;

            config.PlayerSettings.Level = 100;
            config.PlayerSettings.ExpertiseTechnique = Expertise.TwoHandedAxe;

            config.Skills.Add(Skill.LungingStrike, 1);
            config.Skills.Add(Skill.EnhancedLungingStrike, 1);
            config.Skills.Add(Skill.Whirlwind, 5);
            config.Skills.Add(Skill.EnhancedWhirlwind, 1);
            config.Skills.Add(Skill.FuriousWhirlwind, 1);
            config.Skills.Add(Skill.PressurePoint, 3);

            config.Gear.Helm.Armor = 904;
            config.Gear.Helm.CooldownReduction = 11.0;
            config.Gear.Helm.PoisonResistance = 45.2;
            config.Gear.Helm.TotalArmor = 6.8;
            config.Gear.Helm.MaxLife = 472;
            config.Gear.Helm.Aspect = new AspectOfGraspingWhirlwind();
            config.Gear.Helm.Gems.Add(new RoyalSapphire());

            config.Gear.Chest.Armor = 1195;
            config.Gear.Chest.PhysicalDamage = 10.5;
            config.Gear.Chest.CritChancePhysicalAgainstElites = 6.0;
            config.Gear.Chest.DamageReductionFromBleeding = 14.6;
            config.Gear.Chest.Thorns = 403;
            config.Gear.Chest.Aspect = new RageOfHarrogath(26);
            config.Gear.Chest.Gems.Add(new RoyalSapphire());
            config.Gear.Chest.Gems.Add(new RoyalSapphire());

            config.Gear.Gloves.Armor = 354;
            config.Gear.Gloves.AttackSpeed = 10.6;
            config.Gear.Gloves.LuckyHitChance = 10.2;
            config.Gear.Gloves.NonPhysicalDamage = 18.0;
            config.Gear.Gloves.Whirlwind = 3;
            config.Gear.Gloves.Aspect = new GohrsDevastatingGrips(24);

            config.Gear.Pants.Armor = 729;
            config.Gear.Pants.PotionSpeedWhileInjured = 45;
            config.Gear.Pants.WarCry = 4;
            config.Gear.Pants.Dodge = 7.4;
            config.Gear.Pants.DamageReductionFromClose = 23.0;
            config.Gear.Pants.DamageReductionWhileInjured = 39.5;
            config.Gear.Pants.Aspect = new AspectOfNumbingWraith(12);
            config.Gear.Pants.Gems.Add(new RoyalSapphire());
            config.Gear.Pants.Gems.Add(new RoyalSapphire());

            config.Gear.Boots.Armor = 344;
            config.Gear.Boots.AttacksReduceEvadeCooldown = 1.2;
            config.Gear.Boots.MovementAfterKillingElite = 22.5;
            config.Gear.Boots.MovementSpeed = 15.8;
            config.Gear.Boots.DodgeAgainstDistant = 6.4;
            config.Gear.Boots.Dexterity = 51;
            config.Gear.Boots.Aspect = new GhostwalkerAspect(10);

            config.Gear.TwoHandBludgeoning.DPS = 2199;
            config.Gear.TwoHandBludgeoning.Expertise = Expertise.TwoHandedMace;
            config.Gear.TwoHandBludgeoning.MinDamage = 1955;
            config.Gear.TwoHandBludgeoning.MaxDamage = 2933;
            config.Gear.TwoHandBludgeoning.AttacksPerSecond = 0.9;
            config.Gear.TwoHandBludgeoning.OverpowerDamage = 94.5;
            config.Gear.TwoHandBludgeoning.DamageToClose = 57.0;
            config.Gear.TwoHandBludgeoning.CoreDamage = 58.5;
            config.Gear.TwoHandBludgeoning.Strength = 156;
            config.Gear.TwoHandBludgeoning.CritDamage = 57.0;
            config.Gear.TwoHandBludgeoning.Aspect = new AspectOfLimitlessRage
            {
                Damage = 4,
                MaxDamage = 60
            };
            config.Gear.TwoHandBludgeoning.Gems.Add(new RoyalEmerald());
            config.Gear.TwoHandBludgeoning.Gems.Add(new RoyalEmerald());

            config.Gear.OneHandLeft.DPS = 645;
            config.Gear.OneHandLeft.Expertise = Expertise.OneHandedSword;
            config.Gear.OneHandLeft.MinDamage = 470;
            config.Gear.OneHandLeft.MaxDamage = 704;
            config.Gear.OneHandLeft.AttacksPerSecond = 1.1;
            config.Gear.OneHandLeft.CritDamage = 18.8;
            config.Gear.OneHandLeft.VulnerableDamage = 21.0;
            config.Gear.OneHandLeft.Strength = 77;
            config.Gear.OneHandLeft.DamageToInjured = 34.5;
            config.Gear.OneHandLeft.CoreDamage = 18.8;
            config.Gear.OneHandLeft.Aspect = new ConceitedAspect(25);
            config.Gear.OneHandLeft.Gems.Add(new RoyalEmerald());

            config.Gear.OneHandRight.DPS = 650;
            config.Gear.OneHandRight.Expertise = Expertise.OneHandedSword;
            config.Gear.OneHandRight.MinDamage = 473;
            config.Gear.OneHandRight.MaxDamage = 709;
            config.Gear.OneHandRight.AttacksPerSecond = 1.1;
            config.Gear.OneHandRight.CritDamage = 18.8;
            config.Gear.OneHandRight.DamageToClose = 24.8;
            config.Gear.OneHandRight.AllStats = 30;
            config.Gear.OneHandRight.DamageToSlowed = 22.5;
            config.Gear.OneHandRight.DamageToCrowdControlled = 13.5;
            config.Gear.OneHandRight.Aspect = new AspectOfBerserkRipping(30);
            config.Gear.OneHandRight.Gems.Add(new RoyalEmerald());

            config.Gear.TwoHandSlashing.DPS = 2465;
            config.Gear.TwoHandSlashing.Expertise = Expertise.TwoHandedSword;
            config.Gear.TwoHandSlashing.MinDamage = 1972;
            config.Gear.TwoHandSlashing.MaxDamage = 2958;
            config.Gear.TwoHandSlashing.AttacksPerSecond = 1.0;
            config.Gear.TwoHandSlashing.CritDamage = 52.5;
            config.Gear.TwoHandSlashing.VulnerableDamage = 69.0;
            config.Gear.TwoHandSlashing.DamageToClose = 57.0;
            config.Gear.TwoHandSlashing.Strength = 174;
            config.Gear.TwoHandSlashing.DamageToInjured = 93.0;
            config.Gear.TwoHandSlashing.Aspect = new AspectOfTheDireWhirlwind
            {
                CritChance = 14,
                MaxCritChance = 42
            };
            config.Gear.TwoHandSlashing.Gems.Add(new RoyalEmerald());
            config.Gear.TwoHandSlashing.Gems.Add(new RoyalEmerald());

            config.Gear.Amulet.ResistanceToAll = 24.1;
            config.Gear.Amulet.DamageReductionFromDistant = 19.6;
            config.Gear.Amulet.HealingReceived = 12.8;
            config.Gear.Amulet.DamageReductionFromClose = 12.5;
            config.Gear.Amulet.FuryCostReduction = 16.7;
            config.Gear.Amulet.Aspect = new EdgemastersAspect(29);
            config.Gear.Amulet.Gems.Add(new RoyalSkull());

            config.Gear.Ring1.LightningResistance = 35.0;
            config.Gear.Ring1.FireResistance = 35.0;
            config.Gear.Ring1.ResourceGeneration = 15.4;
            config.Gear.Ring1.DamageToSlowed = 31.5;
            config.Gear.Ring1.CritChance = 6.9;
            config.Gear.Ring1.CritDamage = 23.3;
            config.Gear.Ring1.Aspect = new BoldChieftainsAspect(1.9);
            config.Gear.Ring1.Gems.Add(new RoyalSkull());

            config.Gear.Ring2.ColdResistance = 32.2;
            config.Gear.Ring2.LightningResistance = 32.2;
            config.Gear.Ring2.DamageToClose = 18.8;
            config.Gear.Ring2.CritChance = 6.0;
            config.Gear.Ring2.DamageToSlowed = 20.3;
            config.Gear.Ring2.CritDamage = 16.5;
            config.Gear.Ring2.Aspect = new AspectOfEchoingFury(4);
            config.Gear.Ring2.Gems.Add(new RoyalSkull());

            LungingStrike.Weapon = config.Gear.TwoHandSlashing;

            var sim = new Simulation(config);
            var state = sim.Run();

            Console.WriteLine(state.ProcessedEvents.Count);

            var hits = state.ProcessedEvents.Where(x => x is DamageEvent).Cast<DamageEvent>().Where(x => x.DamageType == DamageType.Direct);
            var crits = state.ProcessedEvents.Where(x => x is DamageEvent).Cast<DamageEvent>().Where(x => x.DamageType == DamageType.DirectCrit);

            var avgHit = hits.Average(x => x.Damage);
            var avgCrit = crits.Average(x => x.Damage);

            var critBonus = (avgCrit - avgHit) / avgHit * 100;

            Console.WriteLine($"Hits: {hits.Count()}");
            Console.WriteLine($"Crits: {crits.Count()}");
            Console.WriteLine($"Avg Hit: {avgHit}");
            Console.WriteLine($"Avg Crit: {avgCrit}");
            Console.WriteLine($"Crit Bonus: {critBonus}");
        }
    }
}
