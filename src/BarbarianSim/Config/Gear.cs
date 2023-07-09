namespace BarbarianSim.Config;

public class Gear
{
    public GearItem Helm { get; init; } = new();
    public GearItem Chest { get; init; } = new();
    public GearItem Gloves { get; init; } = new();
    public GearItem Pants { get; init; } = new();
    public GearItem Boots { get; init; } = new();
    public GearItem TwoHandBludgeoning { get; init; } = new();
    public GearItem OneHandLeft { get; init; } = new();
    public GearItem OneHandRight { get; init; } = new();
    public GearItem TwoHandSlashing { get; init; } = new();
    public GearItem Amulet { get; init; } = new();
    public GearItem Ring1 { get; init; } = new();
    public GearItem Ring2 { get; init; } = new();

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

    public IEnumerable<T> GetAllAspects<T>() => AllGear.Select(g => g.Aspect).OfType<T>().Where(a => a != null);

    public double GetStatTotal(Func<GearItem, double> stat) => AllGear.Sum(g => g.GetStatWithGems(stat));

    public double GetStatTotalMultiplied(Func<GearItem, double> stat) => AllGear.Multiply(g => g.GetStatWithGemsMultiplied(stat));
}
