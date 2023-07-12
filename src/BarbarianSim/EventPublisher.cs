using BarbarianSim.Abilities;
using BarbarianSim.Aspects;
using BarbarianSim.Events;
using BarbarianSim.Skills;

namespace BarbarianSim;

public static class EventPublisher
{
    public static void PublishEvent(EventInfo e, SimulationState state)
    {
        state.ProcessedEvents.Add(e);

        switch (e)
        {
            case DamageEvent ev:
                state.Config.Gear.GetAllAspects<AspectOfTheProtector>().ForEach(a => a.ProcessEvent(ev, state));
                state.Config.Gear.GetAllAspects<GohrsDevastatingGrips>().ForEach(a => a.ProcessEvent(ev, state));
                RallyingCry.ProcessEvent(ev, state);
                WrathOfTheBerserker.ProcessEvent(ev, state);
                EnhancedWhirlwind.ProcessEvent(ev, state);
                break;
            case WhirlwindStoppedEvent ev:
                state.Config.Gear.GetAllAspects<GohrsDevastatingGrips>().ForEach(a => a.ProcessEvent(ev, state));
                break;
            case LuckyHitEvent ev:
                PressurePoint.ProcessEvent(ev, state);
                break;
            case WarCryEvent ev:
                GutteralYell.ProcessEvent(ev, state);
                break;
            case ChallengingShoutEvent ev:
                GutteralYell.ProcessEvent(ev, state);
                state.Config.Gear.GetAllAspects<AspectOfEchoingFury>().ForEach(a => a.ProcessEvent(ev, state));
                break;
            case RallyingCryEvent ev:
                GutteralYell.ProcessEvent(ev, state);
                break;
            case BleedAppliedEvent ev:
                Hamstring.ProcessEvent(ev, state);
                break;
            case FurySpentEvent ev:
                InvigoratingFury.ProcessEvent(ev, state);
                break;
            case AuraAppliedEvent ev:
                state.Config.Gear.GetAllAspects<GhostwalkerAspect>().ForEach(a => a.ProcessEvent(ev, state));
                break;
            case WhirlwindSpinEvent ev:
                ViolentWhirlwind.ProcessEvent(ev, state);
                break;
            case LungingStrikeEvent ev:
                EnhancedLungingStrike.ProcessEvent(ev, state);
                break;
            default:
                break;
        }
    }
}
