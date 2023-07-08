using BarbarianSim.Enums;

namespace BarbarianSim;

public class PlayerState
{
    public double Life { get; set; }
    public double BaseLife { get; set; }
    public double Fortify { get; set; }
    public ISet<Aura> Auras { get; init; } = new HashSet<Aura>();
    public ICollection<Barrier> Barriers { get; init; } = new List<Barrier>();
    public double Fury { get; set; }
    public double MaxFury { get; set; } = 100;

    public bool IsFortified() => Fortify > Life;

    public bool IsInjured(double maxLife) => GetLifePercentage(maxLife) <= 0.35;

    public bool IsHealthy(double maxLife) => GetLifePercentage(maxLife) >= 0.80;

    public double GetLifePercentage(double maxLife) => GetLife(maxLife) / maxLife;

    public double GetMissingLife(double maxLife) => maxLife - GetLife(maxLife);

    private double GetLife(double maxLife) => Math.Min(Life, maxLife);
}
