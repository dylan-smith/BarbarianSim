using System;
using System.Linq;

namespace HunterSim
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var config = new SimulationConfig();

            config.SimulationSettings.FightLength = 600.0;

            config.Gear.Neck = GearItemFactory.LoadNeck("Worgen Claw Necklace");

            config.Gear.Shoulder = GearItemFactory.LoadShoulder("Beast Lord Mantle");
            config.Gear.Shoulder.Enchant = GearItemFactory.LoadShoulderEnchant("Greater Inscription of the Blade");
            config.Gear.Shoulder.Sockets.First(s => s.Color == SocketColor.Yellow).Gem = GearItemFactory.LoadGem("Inscribed Ornate Topaz");
            config.Gear.Shoulder.Sockets.First(s => s.Color == SocketColor.Red).Gem = GearItemFactory.LoadGem("Delicate Living Ruby");

            config.Gear.Back = GearItemFactory.LoadBack("Blood Knight War Cloak");
            config.Gear.Back.Enchant = GearItemFactory.LoadBackEnchant("Greater Agility");

            config.Gear.Chest = GearItemFactory.LoadChest("Beast Lord Cuirass");
            config.Gear.Chest.Enchant = GearItemFactory.LoadChestEnchant("Exceptional Stats");
            config.Gear.Chest.Sockets.First(s => s.Color == SocketColor.Red).Gem = GearItemFactory.LoadGem("Delicate Living Ruby");
            config.Gear.Chest.Sockets.Last(s => s.Color == SocketColor.Red).Gem = GearItemFactory.LoadGem("Delicate Living Ruby");
            config.Gear.Chest.Sockets.First(s => s.Color == SocketColor.Blue).Gem = GearItemFactory.LoadGem("Infused Nightseye");

            config.Gear.Wrist = GearItemFactory.LoadWrist("Nightfall Wristguards");
            config.Gear.Wrist.Enchant = GearItemFactory.LoadWristEnchant("Assault");

            config.Gear.Hands = GearItemFactory.LoadHands("Beast Lord Handguards");
            config.Gear.Hands.Enchant = GearItemFactory.LoadHandEnchant("Superior Agility");
            config.Gear.Hands.Sockets.First(s => s.Color == SocketColor.Red).Gem = GearItemFactory.LoadGem("Delicate Living Ruby");
            config.Gear.Hands.Sockets.First(s => s.Color == SocketColor.Blue).Gem = GearItemFactory.LoadGem("Infused Nightseye");

            config.Gear.Waist = GearItemFactory.LoadWaist("Girdle of Treachery");
            config.Gear.Waist.Sockets[0].Gem = GearItemFactory.LoadGem("Delicate Living Ruby");
            config.Gear.Waist.Sockets[1].Gem = GearItemFactory.LoadGem("Delicate Living Ruby");

            config.Gear.Legs = GearItemFactory.LoadLegs("Scaled Greaves of the Marksman");
            config.Gear.Legs.Enchant = GearItemFactory.LoadLegEnchant("Nethercobra Leg Armor");
            config.Gear.Legs.Sockets[0].Gem = GearItemFactory.LoadGem("Delicate Living Ruby");
            config.Gear.Legs.Sockets[1].Gem = GearItemFactory.LoadGem("Delicate Living Ruby");
            config.Gear.Legs.Sockets[2].Gem = GearItemFactory.LoadGem("Delicate Living Ruby");

            config.Gear.Feet = GearItemFactory.LoadFeet("Edgewalker Longboots");
            config.Gear.Feet.Enchant = GearItemFactory.LoadFeetEnchant("Dexterity");
            config.Gear.Feet.Sockets.First(s => s.Color == SocketColor.Red).Gem = GearItemFactory.LoadGem("Delicate Living Ruby");
            config.Gear.Feet.Sockets.First(s => s.Color == SocketColor.Yellow).Gem = GearItemFactory.LoadGem("Wicked Noble Topaz");

            config.Gear.Finger1 = GearItemFactory.LoadFinger("Ring of the Recalcitrant");
            config.Gear.Finger2 = GearItemFactory.LoadFinger("Garona's Signet Ring");
            config.Gear.Trinket1 = GearItemFactory.LoadTrinket("Bloodlust Brooch");

            config.Gear.MainHand = GearItemFactory.LoadMainHand("Big Bad Wolf's Paw");
            config.Gear.MainHand.Enchant = GearItemFactory.LoadOneHandEnchant("Greater Agility");

            config.Gear.OffHand = GearItemFactory.LoadOffHand("Blade of the Unrequited");
            config.Gear.OffHand.Enchant = GearItemFactory.LoadOneHandEnchant("Greater Agility");
            config.Gear.OffHand.Sockets.First(s => s.Color == SocketColor.Red).Gem = GearItemFactory.LoadGem("Delicate Living Ruby");
            config.Gear.OffHand.Sockets.First(s => s.Color == SocketColor.Yellow).Gem = GearItemFactory.LoadGem("Wicked Noble Topaz");
            config.Gear.OffHand.Sockets.First(s => s.Color == SocketColor.Blue).Gem = GearItemFactory.LoadGem("Infused Nightseye");

            config.Gear.Ranged = GearItemFactory.LoadRanged("Barrel-Blade Longrifle");
            config.Gear.Ranged.Enchant = GearItemFactory.LoadRangedEnchant("Stabilized Eternium Scope");

            config.Gear.Ammo = GearItemFactory.LoadAmmo("Warden's Arrow");
            config.Gear.Quiver = GearItemFactory.LoadQuiver("Worg Hide Quiver");

            config.BossSettings.Level = 73;
            config.BossSettings.BossType = BossType.Demon;

            config.PlayerSettings.Race = Race.Draenei;
            config.PlayerSettings.Level = 70;

            config.Talents.Add(Talent.LethalShots, 5);
            config.Talents.Add(Talent.Efficiency, 5);
            config.Talents.Add(Talent.AimedShot, 1);
            config.Talents.Add(Talent.ImprovedArcaneShot, 4);
            config.Talents.Add(Talent.MortalShots, 5);

            var sim = new Simulation(config);
            var state = sim.Run();

            Console.WriteLine(state.ProcessedEvents.Count);

            var hits = state.ProcessedEvents.Where(x => x is DamageEvent).Cast<DamageEvent>().Where(x => x.DamageType == DamageType.Hit);
            var misses = state.ProcessedEvents.Where(x => x is DamageEvent).Cast<DamageEvent>().Where(x => x.DamageType == DamageType.Miss);
            var crits = state.ProcessedEvents.Where(x => x is DamageEvent).Cast<DamageEvent>().Where(x => x.DamageType == DamageType.Crit);

            var avgHit = hits.Average(x => x.Damage);
            var avgCrit = crits.Average(x => x.Damage);

            var critBonus = (avgCrit - avgHit) / avgHit * 100;

            Console.WriteLine($"Hits: {hits.Count()}");
            Console.WriteLine($"Crits: {crits.Count()}");
            Console.WriteLine($"Misses: {misses.Count()}");
            Console.WriteLine($"Avg Hit: {avgHit}");
            Console.WriteLine($"Avg Crit: {avgCrit}");
            Console.WriteLine($"Crit Bonus: {critBonus}");

            var minDmg = config.Gear.Ranged.MinDamage;
            var maxDmg = config.Gear.Ranged.MaxDamage;
            var avgDmg = (minDmg + maxDmg) / 2;

            var minSim = new Simulation(config);

            config.Gear.Ranged.MinDamage = minDmg;
            config.Gear.Ranged.MaxDamage = minDmg;

            var minState = minSim.Run();

            var minHit = minState.ProcessedEvents.Where(x => x is DamageEvent).Cast<DamageEvent>().First(x => x.DamageType == DamageType.Hit).Damage;
            var minCrit = minState.ProcessedEvents.Where(x => x is DamageEvent).Cast<DamageEvent>().First(x => x.DamageType == DamageType.Crit).Damage;

            var maxSim = new Simulation(config);

            config.Gear.Ranged.MinDamage = maxDmg;
            config.Gear.Ranged.MaxDamage = maxDmg;

            var maxState = maxSim.Run();

            var maxHit = maxState.ProcessedEvents.Where(x => x is DamageEvent).Cast<DamageEvent>().First(x => x.DamageType == DamageType.Hit).Damage;
            var maxCrit = maxState.ProcessedEvents.Where(x => x is DamageEvent).Cast<DamageEvent>().First(x => x.DamageType == DamageType.Crit).Damage;

            Console.WriteLine($"Theoretical Hit Min: {minHit}");
            Console.WriteLine($"Theoretical Hit Max: {maxHit}");
            Console.WriteLine($"Theoretical Crit Min: {minCrit}");
            Console.WriteLine($"Theoretical Crit Max: {maxCrit}");
        }
    }
}
