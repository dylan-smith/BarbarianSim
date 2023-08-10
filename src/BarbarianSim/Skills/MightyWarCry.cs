using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Skills;

public class MightyWarCry : IHandlesEvent<WarCryEvent>
{
    // War Cry grants you 15%[x] Base Life (15%[x] HP) as Fortify
    public const double FORTIFY = 0.15;

    public MightyWarCry(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public void ProcessEvent(WarCryEvent e, SimulationState state)
    {
        if (state.Config.GetSkillPoints(Skill.MightyWarCry) > 0)
        {
            var fortify = state.Player.BaseLife * FORTIFY;
            state.Events.Add(new FortifyGeneratedEvent(e.Timestamp, "Mighty War Cry", fortify));
            _log.Verbose($"Mighty War Cry created FortifyGeneratedEvent for {fortify:F2} Fortify");
        }
    }
}
