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

    public bool IsInjured(double maxLife) => (Life / maxLife) <= 0.35;

    public bool IsHealthy(double maxLife) => (Life / maxLife) >= 0.80;
}
