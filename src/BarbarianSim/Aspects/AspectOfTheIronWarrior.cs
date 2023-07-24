using BarbarianSim.Abilities;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Aspects;

public class AspectOfTheIronWarrior : Aspect, IHandlesEvent<IronSkinEvent>
{
    // Iron Skin grants Unstoppable, and [18 - 28]% Damage Reduction.
    public double DamageReduction { get; set; }

    public void ProcessEvent(IronSkinEvent e, SimulationState state)
    {
        if (IsAspectEquipped(state))
        {
            state.Events.Add(new AuraAppliedEvent(e.Timestamp, IronSkin.DURATION, Aura.Unstoppable));
        }
    }

    public virtual double GetDamageReductionBonus(SimulationState state)
    {
        return IsAspectEquipped(state) && state.Player.Auras.Contains(Aura.IronSkin)
            ? DamageReduction
            : 0.0;
    }
}
