using BarbarianSim.Enums;

namespace BarbarianSim;

public class EnemyState
{
    public double Life { get; set; }
    public double MaxLife { get; set; }
    public ISet<Aura> Auras { get; init; } = new HashSet<Aura>();

    public bool IsSlowed() => Auras.Contains(Aura.Slow);

    public bool IsCrowdControlled() => Auras.Any(a => a.IsCrowdControl());

    public bool IsInjured() => ((double)Life / MaxLife) <= 0.35;

    public bool IsHealthy() => ((double)Life / MaxLife) >= 0.80;

    public bool IsVulnerable() => Auras.Contains(Aura.Vulnerable);

    public bool IsBleeding() => Auras.Contains(Aura.Bleeding);
}
