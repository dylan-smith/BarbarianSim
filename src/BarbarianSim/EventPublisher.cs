using BarbarianSim.Abilities;
using BarbarianSim.Aspects;
using BarbarianSim.Events;
using BarbarianSim.Skills;
using Microsoft.Extensions.DependencyInjection;

namespace BarbarianSim;

public static class EventPublisher
{
    public static void PublishEvent(EventInfo e, SimulationState state, IServiceProvider sp)
    {
        state.ProcessedEvents.Add(e);

        switch (e)
        {
            case DamageEvent ev:
                state.Config.Gear.GetAllAspects<AspectOfTheProtector>().ForEach(a => a.ProcessEvent(ev, state));
                sp.GetRequiredService<EnhancedWhirlwind>().ProcessEvent(ev, state);
                sp.GetRequiredService<CombatLungingStrike>().ProcessEvent(ev, state);
                break;
            case DirectDamageEvent ev:
                state.Config.Gear.GetAllAspects<GohrsDevastatingGrips>().ForEach(a => a.ProcessEvent(ev, state));
                sp.GetRequiredService<RallyingCry>().ProcessEvent(ev, state);
                sp.GetRequiredService<WrathOfTheBerserker>().ProcessEvent(ev, state);
                break;
            case WhirlwindStoppedEvent ev:
                state.Config.Gear.GetAllAspects<GohrsDevastatingGrips>().ForEach(a => a.ProcessEvent(ev, state));
                break;
            case LuckyHitEvent ev:
                sp.GetRequiredService<PressurePoint>().ProcessEvent(ev, state);
                break;
            case WarCryEvent ev:
                sp.GetRequiredService<GutteralYell>().ProcessEvent(ev, state);
                break;
            case ChallengingShoutEvent ev:
                sp.GetRequiredService<GutteralYell>().ProcessEvent(ev, state);
                state.Config.Gear.GetAllAspects<AspectOfEchoingFury>().ForEach(a => a.ProcessEvent(ev, state));
                break;
            case RallyingCryEvent ev:
                sp.GetRequiredService<GutteralYell>().ProcessEvent(ev, state);
                break;
            case BleedAppliedEvent ev:
                sp.GetRequiredService<Hamstring>().ProcessEvent(ev, state);
                break;
            case FurySpentEvent ev:
                sp.GetRequiredService<InvigoratingFury>().ProcessEvent(ev, state);
                break;
            case AuraAppliedEvent ev:
                state.Config.Gear.GetAllAspects<GhostwalkerAspect>().ForEach(a => a.ProcessEvent(ev, state));
                break;
            case WhirlwindSpinEvent ev:
                sp.GetRequiredService<ViolentWhirlwind>().ProcessEvent(ev, state);
                break;
            case LungingStrikeEvent ev:
                sp.GetRequiredService<EnhancedLungingStrike>().ProcessEvent(ev, state);
                sp.GetRequiredService<BattleLungingStrike>().ProcessEvent(ev, state);
                break;
            default:
                break;
        }
    }
}
