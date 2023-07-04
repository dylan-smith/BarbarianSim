using BarbarianSim.Enums;

namespace BarbarianSim;

public class PlayerState
{
    public int Life { get; set; }
    public int MaxLife { get; set; }
    public ISet<Aura> Auras { get; init; } = new HashSet<Aura>();
    public ICollection<Barrier> Barriers { get; init; } = new List<Barrier>();
}
