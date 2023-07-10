using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.Events;

public class IronSkinEvent : EventInfo
{
    public IronSkinEvent(double timestamp) : base(timestamp)
    {
    }

    public AuraAppliedEvent IronSkinAuraAppliedEvent { get; set; }
    public CooldownCompletedEvent IronSkinCooldownCompletedEvent { get; set; }
    public BarrierAppliedEvent BarrierAppliedEvent { get; set; }
    public IList<HealingEvent> HealingEvents { get; init; } = new List<HealingEvent>();
    public FortifyGeneratedEvent FortifyGeneratedEvent { get; set; }

    public override void ProcessEvent(SimulationState state)
    {
        IronSkinAuraAppliedEvent = new AuraAppliedEvent(Timestamp, IronSkin.DURATION, Aura.IronSkin);
        state.Events.Add(IronSkinAuraAppliedEvent);

        state.Player.Auras.Add(Aura.IronSkinCooldown);

        IronSkinCooldownCompletedEvent = new CooldownCompletedEvent(Timestamp + IronSkin.COOLDOWN, Aura.IronSkinCooldown);
        state.Events.Add(IronSkinCooldownCompletedEvent);

        var maxLife = MaxLifeCalculator.Calculate(state);
        var missingLife = state.Player.GetMissingLife(maxLife);
        var barrierPercent = IronSkin.GetBarrierPercentage(state);
        var barrierAmount = missingLife * barrierPercent;

        if (state.Config.Skills.ContainsKey(Skill.EnhancedIronSkin))
        {
            barrierAmount += maxLife * IronSkin.BONUS_FROM_ENHANCED;
        }

        BarrierAppliedEvent = new BarrierAppliedEvent(Timestamp, barrierAmount, IronSkin.DURATION);
        state.Events.Add(BarrierAppliedEvent);

        if (state.Config.Skills.ContainsKey(Skill.TacticalIronSkin))
        {
            for (var i = 0; i < IronSkin.DURATION; i++)
            {
                var healEvent = new HealingEvent(Timestamp + i + 1, barrierAmount * IronSkin.HEAL_FROM_TACTICAL);
                HealingEvents.Add(healEvent);
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

            FortifyGeneratedEvent = new FortifyGeneratedEvent(Timestamp, fortifyAmount);
            state.Events.Add(FortifyGeneratedEvent);
        }
    }
}
