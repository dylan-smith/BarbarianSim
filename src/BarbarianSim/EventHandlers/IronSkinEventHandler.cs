using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.EventHandlers;

public class IronSkinEventHandler : EventHandler<IronSkinEvent>
{
    public IronSkinEventHandler(MaxLifeCalculator maxLifeCalculator, IronSkin ironSkin, SimLogger log)
    {
        _maxLifeCalculator = maxLifeCalculator;
        _ironSkin = ironSkin;
        _log = log;
    }

    private readonly MaxLifeCalculator _maxLifeCalculator;
    private readonly IronSkin _ironSkin;
    private readonly SimLogger _log;

    public override void ProcessEvent(IronSkinEvent e, SimulationState state)
    {
        e.IronSkinAuraAppliedEvent = new AuraAppliedEvent(e.Timestamp, "Iron Skin", IronSkin.DURATION, Aura.IronSkin);
        state.Events.Add(e.IronSkinAuraAppliedEvent);
        _log.Verbose($"Created AuraAppliedEvent for Iron Skin for {IronSkin.DURATION} seconds");

        e.IronSkinCooldownAuraAppliedEvent = new AuraAppliedEvent(e.Timestamp, "Iron Skin", IronSkin.COOLDOWN, Aura.IronSkinCooldown);
        state.Events.Add(e.IronSkinCooldownAuraAppliedEvent);
        _log.Verbose($"Created AuraAppliedEvent for Iron Skin Cooldown for {IronSkin.COOLDOWN} seconds");

        var maxLife = _maxLifeCalculator.Calculate(state);
        var missingLife = state.Player.GetMissingLife(maxLife);
        _log.Verbose($"Missing Life = {missingLife}");
        var barrierPercent = _ironSkin.GetBarrierPercentage(state);
        var barrierAmount = missingLife * barrierPercent;

        if (state.Config.Skills.ContainsKey(Skill.EnhancedIronSkin))
        {
            barrierAmount += maxLife * IronSkin.BONUS_FROM_ENHANCED;
            _log.Verbose($"Adding {maxLife * IronSkin.BONUS_FROM_ENHANCED:F2} barrier for Enhanced Iron Skin");
        }

        _log.Verbose($"Total Barrier Amount = {barrierAmount}");

        e.BarrierAppliedEvent = new BarrierAppliedEvent(e.Timestamp, "Iron Skin", barrierAmount, IronSkin.DURATION);
        state.Events.Add(e.BarrierAppliedEvent);
        _log.Verbose($"Created BarrierAppliedEvent for {barrierAmount:F2} barrier for {IronSkin.DURATION} seconds");
    }
}
