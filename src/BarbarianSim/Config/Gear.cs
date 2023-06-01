using System;
using System.Collections.Generic;
using System.Linq;

namespace HunterSim
{
    public class Gear
    {
        public GearItem Head { get; set; }
        public GearItem Neck { get; set; }
        public GearItem Shoulder { get; set; }
        public GearItem Back { get; set; }
        public GearItem Chest { get; set; }
        public GearItem Wrist { get; set; }
        public GearItem MainHand { get; set; }
        public GearItem OffHand { get; set; }
        public GearItem Hands { get; set; }
        public GearItem Waist { get; set; }
        public GearItem Legs { get; set; }
        public GearItem Feet { get; set; }
        public GearItem Finger1 { get; set; }
        public GearItem Finger2 { get; set; }
        public GearItem Trinket1 { get; set; }
        public GearItem Trinket2 { get; set; }
        public GearItem Ranged { get; set; }
        public GearItem Ammo { get; set; }
        public GearItem Quiver { get; set; }
        public ICollection<GearItem> Other { get; } = new List<GearItem>();

        public IEnumerable<GearItem> GetAllGear()
        {
            if (Head != null) yield return Head;
            if (Neck != null) yield return Neck;
            if (Shoulder != null) yield return Shoulder;
            if (Back != null) yield return Back;
            if (Chest != null) yield return Chest;
            if (Wrist != null) yield return Wrist;
            if (MainHand != null) yield return MainHand;
            if (OffHand != null) yield return OffHand;
            if (Hands != null) yield return Hands;
            if (Waist != null) yield return Waist;
            if (Legs != null) yield return Legs;
            if (Feet != null) yield return Feet;
            if (Finger1 != null) yield return Finger1;
            if (Finger2 != null) yield return Finger2;
            if (Trinket1 != null) yield return Trinket1;
            if (Trinket2 != null) yield return Trinket2;
            if (Ranged != null) yield return Ranged;
            if (Ammo != null) yield return Ammo;
            if (Quiver != null) yield return Quiver;

            foreach (var x in Other)
            {
                yield return x;
            }
        }

        public IEnumerable<GearItem> GetAllEnchants()
        {
            return GetAllGear().Where(x => x.Enchant != null).Select(x => x.Enchant).ToList();
        }

        public IEnumerable<GearItem> GetAllGems()
        {
            return GetAllGear().SelectMany(g => g.Sockets.Select(s => s.Gem)).Where(g => g != null).ToList();
        }

        public double GetStatTotal(Func<GearItem, double> stat)
        {
            var result = GetAllGear().Sum(g => g.GetStatWithSockets(stat));
            result += GetAllEnchants().Sum(e => e.GetStatWithSockets(stat));

            return result;
        }

        public int GetGearCount(params int[] ids)
        {
            return ids.Count(id => GetAllGear().Any(g => g.Wowhead == id));
        }
    }
}
