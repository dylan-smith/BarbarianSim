using BarbarianSim.Enums;

namespace BarbarianSim;

public class PlayerState
{
    public double Life { get; set; }
    public double MaxLife { get; set; }
    public double Fortify { get; set; }
    public ISet<Aura> Auras { get; init; } = new HashSet<Aura>();
    public ICollection<Barrier> Barriers { get; init; } = new List<Barrier>();
    public double Fury { get; set; }
    public double MaxFury { get; set; } = 100;
}
