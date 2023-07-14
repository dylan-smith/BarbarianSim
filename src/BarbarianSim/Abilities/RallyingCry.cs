using BarbarianSim.Enums;
using BarbarianSim.EventFactories;

namespace BarbarianSim.Abilities;

public class RallyingCry
{
    public const double MOVEMENT_SPEED = 30.0;
    public const double DURATION = 6.0;
    public const double COOLDOWN = 25.0;
    public const double FURY_FROM_TACTICAL_RALLYING_CRY = 20.0;
    public const double RESOURCE_GENERATION_FROM_TACTICAL_RALLYING_CRY = 1.20;

    public RallyingCry(RallyingCryEventFactory rallyingCryEventFactory) => _rallyingCryEventFactory = rallyingCryEventFactory;

    private readonly RallyingCryEventFactory _rallyingCryEventFactory;

    // Bellow a Rallying Cry, increasing your Movement Speed by 30%[+] and Resource Generation by 40%[x] for 6.0 seconds, and Nearby allies for 3.0 seconds (Cooldown: 25 seconds)
    public bool CanUse(SimulationState state) => !state.Player.Auras.Contains(Aura.RallyingCryCooldown);

    public void Use(SimulationState state) => state.Events.Add(_rallyingCryEventFactory.Create(state.CurrentTime));
}
