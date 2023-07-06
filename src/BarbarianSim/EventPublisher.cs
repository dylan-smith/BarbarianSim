using BarbarianSim.Aspects;
using BarbarianSim.Events;
using BarbarianSim.Skills;

namespace BarbarianSim;

public static class EventPublisher
{
    public static void PublishEvent(EventInfo e, SimulationState state)
    {
        state.ProcessedEvents.Add(e);
        Console.WriteLine(e.ToString());

        switch (e)
        {
            case DamageEvent ev:
                state.Config.Gear.AllGear.Select(g => g.Aspect)
                                         .Where(a => a is AspectOfTheProtector)
                                         .Cast<AspectOfTheProtector>()
                                         .ToList()
                                         .ForEach(a => a.ProcessEvent(ev, state));
                state.Config.Gear.AllGear.Select(g => g.Aspect)
                                         .Where(a => a is GohrsDevastatingGrips)
                                         .Cast<GohrsDevastatingGrips>()
                                         .ToList()
                                         .ForEach(a => a.ProcessEvent(ev, state));
                break;
            case WhirlwindStoppedEvent ev:
                state.Config.Gear.AllGear.Select(g => g.Aspect)
                                         .Where(a => a is GohrsDevastatingGrips)
                                         .Cast<GohrsDevastatingGrips>()
                                         .ToList()
                                         .ForEach(a => a.ProcessEvent(ev, state));
                break;
            case LuckyHitEvent ev:
                PressurePoint.ProcessEvent(ev, state);
                break;
            default:
                break;
        }
    }
}
