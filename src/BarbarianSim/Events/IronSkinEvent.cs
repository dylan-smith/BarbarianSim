using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.EventFactories;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.Events;

public class IronSkinEvent : EventInfo
{
    public IronSkinEvent(AuraAppliedEventFactory auraAppliedEventFactory,
                         MaxLifeCalculator maxLifeCalculator,
                         IronSkin ironSkin,
                         BarrierAppliedEventFactory barrierAppliedEventFactory,
                         HealingEventFactory healingEventFactory,
                         FortifyGeneratedEventFactory fortifyGeneratedEventFactory,
                         double timestamp) : base(timestamp)
    {
        _auraAppliedEventFactory = auraAppliedEventFactory;
        _maxLifeCalculator = maxLifeCalculator;
        _ironSkin = ironSkin;
        _barrierAppliedEventFactory = barrierAppliedEventFactory;
        _healingEventFactory = healingEventFactory;
        _fortifyGeneratedEventFactory = fortifyGeneratedEventFactory;
    }

    private readonly AuraAppliedEventFactory _auraAppliedEventFactory;
    private readonly MaxLifeCalculator _maxLifeCalculator;
    private readonly IronSkin _ironSkin;
    private readonly BarrierAppliedEventFactory _barrierAppliedEventFactory;
    private readonly HealingEventFactory _healingEventFactory;
    private readonly FortifyGeneratedEventFactory _fortifyGeneratedEventFactory;

    public AuraAppliedEvent IronSkinAuraAppliedEvent { get; set; }
    public AuraAppliedEvent IronSkinCooldownAuraAppliedEvent { get; set; }
    public BarrierAppliedEvent BarrierAppliedEvent { get; set; }
    public IList<HealingEvent> HealingEvents { get; init; } = new List<HealingEvent>();
    public FortifyGeneratedEvent FortifyGeneratedEvent { get; set; }

    public override void ProcessEvent(SimulationState state)
    {
        IronSkinAuraAppliedEvent = _auraAppliedEventFactory.Create(Timestamp, IronSkin.DURATION, Aura.IronSkin);
        state.Events.Add(IronSkinAuraAppliedEvent);

        IronSkinCooldownAuraAppliedEvent = _auraAppliedEventFactory.Create(Timestamp, IronSkin.COOLDOWN, Aura.IronSkinCooldown);
        state.Events.Add(IronSkinCooldownAuraAppliedEvent);

        var maxLife = _maxLifeCalculator.Calculate(state);
        var missingLife = state.Player.GetMissingLife(maxLife);
        var barrierPercent = _ironSkin.GetBarrierPercentage(state);
        var barrierAmount = missingLife * barrierPercent;

        if (state.Config.Skills.ContainsKey(Skill.EnhancedIronSkin))
        {
            barrierAmount += maxLife * IronSkin.BONUS_FROM_ENHANCED;
        }

        BarrierAppliedEvent = _barrierAppliedEventFactory.Create(Timestamp, barrierAmount, IronSkin.DURATION);
        state.Events.Add(BarrierAppliedEvent);

        if (state.Config.Skills.ContainsKey(Skill.TacticalIronSkin))
        {
            for (var i = 0; i < IronSkin.DURATION; i++)
            {
                var healEvent = _healingEventFactory.Create(Timestamp + i + 1, barrierAmount * IronSkin.HEAL_FROM_TACTICAL);
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

            FortifyGeneratedEvent = _fortifyGeneratedEventFactory.Create(Timestamp, fortifyAmount);
            state.Events.Add(FortifyGeneratedEvent);
        }
    }
}
