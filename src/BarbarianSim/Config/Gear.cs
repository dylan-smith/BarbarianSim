namespace BarbarianSim.Config
{
    public class Gear
    {
        public GearItem Helm { get; private set; } = new GearItem();
        public GearItem Chest { get; private set; }
        public GearItem Gloves { get; private set; }
        public GearItem Pants { get; private set; }
        public GearItem Boots { get; private set; }
        public GearItem TwoHandBludgeoning { get; private set; }
        public GearItem OneHandLeft { get; private set; }
        public GearItem OneHandRight { get; private set; }
        public GearItem TwoHandSlashing { get; private set; }
        public GearItem Amulet { get; private set; }
        public GearItem Ring1 { get; private set; }
        public GearItem Ring2 { get; private set; }

        public IEnumerable<GearItem> AllGear
        {
            get
            {
                yield return Helm;
                yield return Chest;
                yield return Gloves;
                yield return Pants;
                yield return Boots;
                yield return TwoHandBludgeoning;
                yield return OneHandLeft;
                yield return OneHandRight;
                yield return TwoHandSlashing;
                yield return Amulet;
                yield return Ring1;
                yield return Ring2;
            }
        }

        public IEnumerable<Gem> GetAllGems() => AllGear.SelectMany(g => g.Gems);

        public double GetStatTotal(Func<GearItem, double> stat) => AllGear.Sum(g => g.GetStatWithGems(stat));
    }
}
