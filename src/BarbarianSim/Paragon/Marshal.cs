using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Paragon;

public class Marshal : IHandlesEvent<AuraAppliedEvent>
{
    // After casting a Shout Skill, the active Cooldown of every other Shout Skill is reduced by 1.2 seconds.
    public const double COOLDOWN_REDUCTION = 1.2;

    public void ProcessEvent(AuraAppliedEvent e, SimulationState state)
    {
        if (!state.Config.HasParagonNode(ParagonNode.Marshal))
        {
            return;
        }

        if (e.Aura is Aura.ChallengingShoutCooldown or Aura.RallyingCryCooldown or Aura.WarCryCooldown)
        {
            var cooldownEvents = state.Events.OfType<AuraExpiredEvent>().Where(x => x.Aura is Aura.ChallengingShoutCooldown or Aura.RallyingCryCooldown or Aura.WarCryCooldown && x.Aura != e.Aura);

            foreach (var cooldownEvent in cooldownEvents)
            {
                cooldownEvent.Timestamp -= COOLDOWN_REDUCTION;
                cooldownEvent.Timestamp = Math.Max(cooldownEvent.Timestamp, state.CurrentTime);
            }
        }
    }
}
