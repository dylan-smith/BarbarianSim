using BarbarianSim.Events;

namespace BarbarianSim.EventHandlers;

public class AspectOfEchoingFuryProcEventHandler : EventHandler<AspectOfEchoingFuryProcEvent>
{
    public override void ProcessEvent(AspectOfEchoingFuryProcEvent e, SimulationState state)
    {
        for (var i = 0; i < Math.Floor(e.Duration); i++)
        {
            var furyEvent = new FuryGeneratedEvent(e.Timestamp + i + 1, e.Fury);
            e.FuryGeneratedEvents.Add(furyEvent);
            state.Events.Add(furyEvent);
        }
    }
}
