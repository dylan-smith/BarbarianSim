using BarbarianSim.Config;

namespace BarbarianSim.Aspects;

public class ConceitedAspect : Aspect
{
    // Deal 15-25%[x] increased damage while you have a Barrier active
    public double Damage { get; set; }

    public virtual double GetDamageBonus(SimulationState state)
    {
        return IsAspectEquipped(state) && state.Player.Barriers.Any()
            ? 1 + (Damage / 100.0)
            : 1.0;
    }
}
