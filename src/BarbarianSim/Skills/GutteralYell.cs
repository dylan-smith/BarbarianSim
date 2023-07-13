using BarbarianSim.Enums;
using BarbarianSim.EventFactories;
using BarbarianSim.Events;

namespace BarbarianSim.Skills;

public class GutteralYell
{
    // Your Shout skills cause enemies to deal 4% less damage for 5 seconds
    public const double DURATION = 5;

    public GutteralYell(GutteralYellProcEventFactory gutteralYellProcEventFactory) => _gutteralYellProcEventFactory = gutteralYellProcEventFactory;

    private readonly GutteralYellProcEventFactory _gutteralYellProcEventFactory;

    public void ProcessEvent(WarCryEvent e, SimulationState state)
    {
        state.Events.Add(_gutteralYellProcEventFactory.Create(e.Timestamp));
    }

    public void ProcessEvent(ChallengingShoutEvent e, SimulationState state)
    {
        state.Events.Add(_gutteralYellProcEventFactory.Create(e.Timestamp));
    }

    public void ProcessEvent(RallyingCryEvent e, SimulationState state)
    {
        state.Events.Add(_gutteralYellProcEventFactory.Create(e.Timestamp));
    }

    public double GetDamageReduction(SimulationState state)
    {
        var skillPoints = 0;

        if (state.Config.Skills.TryGetValue(Skill.GutteralYell, out var pointsSpent))
        {
            skillPoints += pointsSpent;
        }

        return skillPoints switch
        {
            1 => 4,
            2 => 8,
            >= 3 => 12,
            _ => 0,
        };
    }
}
