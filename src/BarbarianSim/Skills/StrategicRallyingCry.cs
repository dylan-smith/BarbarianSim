using BarbarianSim.Enums;
using BarbarianSim.EventFactories;
using BarbarianSim.Events;

namespace BarbarianSim.Skills;

public class StrategicRallyingCry
{
    // Rallying Cry grants you 10% Base Life (10%[x] HP) as Fortify. While Rallying Cry is active, you gain an additional 2% Base Life (2%[x] HP) as Fortify each time you take or deal Direct Damage
    public const double DIRECT_DAMAGE_FORTIFY = 0.02;
    public const double FORTIFY = 0.1;

    public StrategicRallyingCry(FortifyGeneratedEventFactory fortifyGeneratedEventFactory) => _fortifyGeneratedEventFactory = fortifyGeneratedEventFactory;

    private readonly FortifyGeneratedEventFactory _fortifyGeneratedEventFactory;

    public void ProcessEvent(DirectDamageEvent damageEvent, SimulationState state)
    {
        if (state.Config.Skills.ContainsKey(Skill.StrategicRallyingCry) &&
            state.Player.Auras.Contains(Aura.RallyingCry))
        {
            state.Events.Add(_fortifyGeneratedEventFactory.Create(damageEvent.Timestamp, DIRECT_DAMAGE_FORTIFY * state.Player.BaseLife));
        }
    }
}
