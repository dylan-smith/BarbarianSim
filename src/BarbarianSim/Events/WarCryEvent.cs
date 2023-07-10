﻿using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.Skills;

namespace BarbarianSim.Events;

public class WarCryEvent : EventInfo
{
    public WarCryEvent(double timestamp) : base(timestamp)
    {
    }

    public AuraAppliedEvent WarCryAuraAppliedEvent { get; set; }
    public AuraAppliedEvent WarCryCooldownAuraAppliedEvent { get; set; }
    public AuraAppliedEvent BerserkingAuraAppliedEvent { get; set; }
    public FortifyGeneratedEvent FortifyGeneratedEvent { get; set; }
    public RaidLeaderProcEvent RaidLeaderProcEvent { get; set; }
    public double Duration { get; set; }

    public override void ProcessEvent(SimulationState state)
    {
        Duration = WarCry.DURATION * BoomingVoice.GetDurationIncrease(state);

        WarCryAuraAppliedEvent = new AuraAppliedEvent(Timestamp, Duration, Aura.WarCry);
        state.Events.Add(WarCryAuraAppliedEvent);

        WarCryCooldownAuraAppliedEvent = new AuraAppliedEvent(Timestamp, WarCry.COOLDOWN, Aura.WarCryCooldown);
        state.Events.Add(WarCryCooldownAuraAppliedEvent);

        if (state.Config.Skills.ContainsKey(Skill.EnhancedWarCry))
        {
            BerserkingAuraAppliedEvent = new AuraAppliedEvent(Timestamp, WarCry.BERSERKING_DURATION_FROM_ENHANCED, Aura.Berserking);
            state.Events.Add(BerserkingAuraAppliedEvent);
        }

        if (state.Config.Skills.ContainsKey(Skill.MightyWarCry))
        {
            FortifyGeneratedEvent = new FortifyGeneratedEvent(Timestamp, WarCry.FORTIFY_FROM_MIGHTY * state.Player.BaseLife);
            state.Events.Add(FortifyGeneratedEvent);
        }

        if (state.Config.Skills.ContainsKey(Skill.RaidLeader))
        {
            RaidLeaderProcEvent = new RaidLeaderProcEvent(Timestamp, Duration);
            state.Events.Add(RaidLeaderProcEvent);
        }
    }
}
