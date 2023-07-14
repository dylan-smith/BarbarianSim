using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.EventFactories;

namespace BarbarianSim.Abilities;

public class LungingStrike
{
    public const double LUCKY_HIT_CHANCE = 0.5;
    public const double FURY_GENERATED = 10;

    public LungingStrike(LungingStrikeEventFactory lungingStrikeEventFactory) => _lungingStrikeEventFactory = lungingStrikeEventFactory;

    private readonly LungingStrikeEventFactory _lungingStrikeEventFactory;

    // Lunge forward and strike an enemy for 33% damage
    public bool CanUse(SimulationState state) => !state.Player.Auras.Contains(Aura.WeaponCooldown);

    public void Use(SimulationState state, EnemyState target) => state.Events.Add(_lungingStrikeEventFactory.Create(state.CurrentTime, target));
}
