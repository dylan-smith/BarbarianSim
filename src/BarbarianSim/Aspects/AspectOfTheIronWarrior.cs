using BarbarianSim.Abilities;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Aspects;

public class AspectOfTheIronWarrior : Aspect, IHandlesEvent<IronSkinEvent>
{
    // Iron Skin grants Unstoppable, and [18 - 28]% Damage Reduction.
    public double DamageReduction { get; set; }

    public AspectOfTheIronWarrior(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public void ProcessEvent(IronSkinEvent e, SimulationState state)
    {
        if (IsAspectEquipped(state))
        {
            state.Events.Add(new AuraAppliedEvent(e.Timestamp, "Aspect of the Iron Warrior", IronSkin.DURATION, Aura.Unstoppable));
            _log.Verbose($"Aspect of the Iron Warrior created AuraAppliedEvent for Unstoppable for {IronSkin.DURATION} seconds");
        }
    }

    public virtual double GetDamageReductionBonus(SimulationState state)
    {
        if (IsAspectEquipped(state) && state.Player.Auras.Contains(Aura.IronSkin))
        {
            _log.Verbose($"Damage Reduction from Aspect of the Iron Warrior = {DamageReduction:F2}%");
            return DamageReduction;
        }

        return 0.0;
    }
}
