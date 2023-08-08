using BarbarianSim.Config;

namespace BarbarianSim.Aspects;

public class IronBloodAspect : Aspect
{
    // Gain [2.0 - 4.0]% Damage Reduction for each Nearby Bleeding enemy up to [10 - 20]% maximum.
    public double DamageReduction { get; set; }
    public double MaxDamageReduction { get; set; }

    public IronBloodAspect(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public virtual double GetDamageReductionBonus(SimulationState state)
    {
        if (IsAspectEquipped(state))
        {
            var bleedingEnemies = state.Enemies.Count(x => x.IsBleeding());

            var result = Math.Min(MaxDamageReduction, bleedingEnemies * DamageReduction);

            if (result > 0.0)
            {
                _log.Verbose($"Damage reduction bonus from Iron Blood Aspect = {result:F2}%");
            }

            return result;
        }

        return 0.0;
    }
}
