using HunterSim.Events;
using System;

namespace HunterSim
{
    public static class EventPublisher
    {
        public static void PublishEvent(EventInfo e, SimulationState state)
        {
            state.ProcessedEvents.Add(e);

            // TODO: Can we use reflection to wire this up dynamically
            switch (e)
            {
                case AutoShotCompletedEvent ev:
                    ImprovedAspectOfTheHawk.ProcessEvent(ev, state);
                    ExposeWeakness.ProcessEvent(ev, state);
                    MasterTactician.ProcessEvent(ev, state);
                    break;
                case CastStartedEvent ev:
                    AutoShot.ProcessEvent(ev, state);
                    break;
            }
        }
    }
}
