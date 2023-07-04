using BarbarianSim.Config;

namespace BarbarianSim.Aspects;

public class GohrsDevastatingGrips : Aspect
{
    public int Damage { get; init; }

    public GohrsDevastatingGrips(int damage) => Damage = damage;
}
