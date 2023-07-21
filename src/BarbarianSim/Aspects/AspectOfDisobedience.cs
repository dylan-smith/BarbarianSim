using BarbarianSim.Config;
using BarbarianSim.Events;

namespace BarbarianSim.Aspects;

public class AspectOfDisobedience : Aspect
{
    // You gain 0.25-0.5%[x] increased Armor for 4 seconds when you deal any form of Damage, stacking up to 15-30%[x]
    public const int DURATION = 4;
    public double ArmorIncrement { get; set; }
    public double MaxArmorBonus { get; set; }

    public virtual double GetArmorBonus(SimulationState state)
    {
        if (IsAspectEquipped(state))
        {
            var damageCount = state.ProcessedEvents.OfType<DamageEvent>()
                                                   .Where(e => e.Timestamp >= state.CurrentTime - 4.0)
                                                   .Count();

            var armorBonus = damageCount * ArmorIncrement / 100.0;

            return 1 + Math.Min(armorBonus, MaxArmorBonus / 100.0);
        }

        return 1.0;
    }
}
