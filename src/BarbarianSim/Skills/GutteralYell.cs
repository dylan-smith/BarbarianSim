using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Skills;

public class GutteralYell : IHandlesEvent<WarCryEvent>, IHandlesEvent<ChallengingShoutEvent>, IHandlesEvent<RallyingCryEvent>
{
    // Your Shout skills cause enemies to deal X% less damage for 5 seconds
    public const double DURATION = 5;

    public GutteralYell(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public void ProcessEvent(WarCryEvent e, SimulationState state)
    {
        state.Events.Add(new GutteralYellProcEvent(e.Timestamp));
        _log.Verbose($"Gutteral Yell created GutteralYellProcEvent");
    }

    public void ProcessEvent(ChallengingShoutEvent e, SimulationState state)
    {
        state.Events.Add(new GutteralYellProcEvent(e.Timestamp));
        _log.Verbose($"Gutteral Yell created GutteralYellProcEvent");
    }

    public void ProcessEvent(RallyingCryEvent e, SimulationState state)
    {
        state.Events.Add(new GutteralYellProcEvent(e.Timestamp));
        _log.Verbose($"Gutteral Yell created GutteralYellProcEvent");
    }

    public virtual double GetDamageReduction(SimulationState state)
    {
        var skillPoints = state.Config.GetSkillPoints(Skill.GutteralYell);

        var result = skillPoints switch
        {
            1 => 4,
            2 => 8,
            >= 3 => 12,
            _ => 0,
        };

        if (result > 0)
        {
            _log.Verbose($"Gutteral Yell reduced damage by {result:F2}%");
        }

        return result;
    }
}
