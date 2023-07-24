using BarbarianSim.Config;

namespace BarbarianSim.Aspects;

public class IronBloodAspect : Aspect
{
    // Gain [2.0 - 4.0]% Damage Reduction for each Nearby Bleeding enemy up to [10 - 20]% maximum.
    public double DamageReduction { get; set; }
    public double MaxDamageReduction { get; set; }

    public virtual double GetDamageReductionBonus(SimulationState state)
    {
        if (IsAspectEquipped(state))
        {
            var bleedingEnemies = state.Enemies.Count(x => x.IsBleeding());

            return Math.Min(MaxDamageReduction, bleedingEnemies * DamageReduction);
        }

        return 0.0;
    }
}
