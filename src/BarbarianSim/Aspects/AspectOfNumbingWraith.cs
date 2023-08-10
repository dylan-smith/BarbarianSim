using BarbarianSim.Config;
using BarbarianSim.Events;

namespace BarbarianSim.Aspects;

public class AspectOfNumbingWraith : Aspect, IHandlesEvent<FuryGeneratedEvent>
{
    // Each point of Fury generated while at Maximum Fury grants 0-54 Fortify
    public int Fortify { get; set; }

    public AspectOfNumbingWraith(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public void ProcessEvent(FuryGeneratedEvent e, SimulationState state)
    {
        if (IsAspectEquipped(state))
        {
            if (e.OverflowFury > 0)
            {
                var fortifyGenerated = Fortify * e.OverflowFury;
                state.Events.Add(new FortifyGeneratedEvent(e.Timestamp, "Aspect of Numbing Wraith", fortifyGenerated));
                _log.Verbose($"Aspect of Numbing Wraith created FortifyGeneratedEvent for {fortifyGenerated:F2} Fortify");
            }
        }
    }
}
