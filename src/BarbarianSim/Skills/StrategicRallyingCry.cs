using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Skills;

public class StrategicRallyingCry : IHandlesEvent<RallyingCryEvent>, IHandlesEvent<DirectDamageEvent>
{
    // Rallying Cry grants you 10% Base Life (10%[x] HP) as Fortify. While Rallying Cry is active, you gain an additional 2% Base Life (2%[x] HP) as Fortify each time you take or deal Direct Damage
    public const double FORTIFY = 0.1;
    public const double DIRECT_DAMAGE_FORTIFY = 0.02;

    public StrategicRallyingCry(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public void ProcessEvent(RallyingCryEvent e, SimulationState state)
    {
        if (state.Config.GetSkillPoints(Skill.StrategicRallyingCry) > 0)
        {
            var fortifyAmount = FORTIFY * state.Player.BaseLife;
            state.Events.Add(new FortifyGeneratedEvent(e.Timestamp, "Strategic Rallying Cry", fortifyAmount));
            _log.Verbose($"Strategic Rallying Cry created FortifyGeneratedEvent for {fortifyAmount:F2} Fortify");
        }
    }

    public void ProcessEvent(DirectDamageEvent e, SimulationState state)
    {
        if (state.Config.HasSkill(Skill.StrategicRallyingCry) &&
            state.Player.Auras.Contains(Aura.RallyingCry))
        {
            var fortifyAmount = DIRECT_DAMAGE_FORTIFY * state.Player.BaseLife;
            state.Events.Add(new FortifyGeneratedEvent(e.Timestamp, "Strategic Rallying Cry", fortifyAmount));
            _log.Verbose($"Strategic Rallying Cry created FortifyGeneratedEvent for {fortifyAmount:F2} Fortify");
        }
    }
}
