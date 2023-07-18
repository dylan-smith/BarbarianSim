using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.EventHandlers;

public class IronSkinEventHandler : EventHandler<IronSkinEvent>
{
    public IronSkinEventHandler(MaxLifeCalculator maxLifeCalculator, IronSkin ironSkin)
    {
        _maxLifeCalculator = maxLifeCalculator;
        _ironSkin = ironSkin;
    }

    private readonly MaxLifeCalculator _maxLifeCalculator;
    private readonly IronSkin _ironSkin;

    public override void ProcessEvent(IronSkinEvent e, SimulationState state)
    {
        e.IronSkinAuraAppliedEvent = new AuraAppliedEvent(e.Timestamp, IronSkin.DURATION, Aura.IronSkin);
        state.Events.Add(e.IronSkinAuraAppliedEvent);

        e.IronSkinCooldownAuraAppliedEvent = new AuraAppliedEvent(e.Timestamp, IronSkin.COOLDOWN, Aura.IronSkinCooldown);
        state.Events.Add(e.IronSkinCooldownAuraAppliedEvent);

        var maxLife = _maxLifeCalculator.Calculate(state);
        var missingLife = state.Player.GetMissingLife(maxLife);
        var barrierPercent = _ironSkin.GetBarrierPercentage(state);
        var barrierAmount = missingLife * barrierPercent;

        if (state.Config.Skills.ContainsKey(Skill.EnhancedIronSkin))
        {
            barrierAmount += maxLife * IronSkin.BONUS_FROM_ENHANCED;
        }

        e.BarrierAppliedEvent = new BarrierAppliedEvent(e.Timestamp, barrierAmount, IronSkin.DURATION);
        state.Events.Add(e.BarrierAppliedEvent);
    }
}
