using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.EventHandlers;

public class IronSkinEventHandler : EventHandler<IronSkinEvent>
{
    public override void ProcessEvent(IronSkinEvent e, SimulationState state)
    {
        e.IronSkinAuraAppliedEvent = new AuraAppliedEvent(e.Timestamp, IronSkin.DURATION, Aura.IronSkin);
        state.Events.Add(e.IronSkinAuraAppliedEvent);

        e.IronSkinCooldownAuraAppliedEvent = new AuraAppliedEvent(e.Timestamp, IronSkin.COOLDOWN, Aura.IronSkinCooldown);
        state.Events.Add(e.IronSkinCooldownAuraAppliedEvent);

        var maxLife = MaxLifeCalculator.Calculate(state);
        var missingLife = state.Player.GetMissingLife(maxLife);
        var barrierPercent = IronSkin.GetBarrierPercentage(state);
        var barrierAmount = missingLife * barrierPercent;

        if (state.Config.Skills.ContainsKey(Skill.EnhancedIronSkin))
        {
            barrierAmount += maxLife * IronSkin.BONUS_FROM_ENHANCED;
        }

        e.BarrierAppliedEvent = new BarrierAppliedEvent(e.Timestamp, barrierAmount, IronSkin.DURATION);
        state.Events.Add(e.BarrierAppliedEvent);

        if (state.Config.Skills.ContainsKey(Skill.TacticalIronSkin))
        {
            for (var i = 0; i < IronSkin.DURATION; i++)
            {
                var healEvent = new HealingEvent(e.Timestamp + i + 1, barrierAmount * IronSkin.HEAL_FROM_TACTICAL);
                e.HealingEvents.Add(healEvent);
                state.Events.Add(healEvent);
            }
        }

        if (state.Config.Skills.ContainsKey(Skill.StrategicIronSkin))
        {
            var fortifyAmount = IronSkin.FORTIFY_FROM_STRATEGIC * state.Player.BaseLife;

            if (state.Player.GetLifePercentage(maxLife) < 0.5)
            {
                fortifyAmount *= 2;
            }

            e.FortifyGeneratedEvent = new FortifyGeneratedEvent(e.Timestamp, fortifyAmount);
            state.Events.Add(e.FortifyGeneratedEvent);
        }
    }
}
