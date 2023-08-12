using BarbarianSim.Config;

namespace BarbarianSim.Aspects;

public class ConceitedAspect : Aspect
{
    // Deal 15-25%[x] increased damage while you have a Barrier active
    public double Damage { get; set; }

    public ConceitedAspect(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public virtual double GetDamageBonus(SimulationState state)
    {
        if (IsAspectEquipped(state) && state.Player.Barriers.Any())
        {
            var result = 1 + (Damage / 100.0);

            _log.Verbose($"Conceited Aspect Damage Bonus = {result:F2}x");

            return result;
        }

        return 1.0;
    }
}
