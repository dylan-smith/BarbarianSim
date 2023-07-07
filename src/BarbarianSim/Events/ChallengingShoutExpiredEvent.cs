using BarbarianSim.Enums;

namespace BarbarianSim.Events;

public class ChallengingShoutExpiredEvent : EventInfo
{
    public ChallengingShoutExpiredEvent(double timestamp) : base(timestamp)
    { }

    public override void ProcessEvent(SimulationState state)
    {
        // if there are other events it means there's a Challenging Shout been applied with a later expiration time (possible if you get the CD shorter than duration)
        if (!state.Events.Any(e => e is ChallengingShoutExpiredEvent))
        {
            state.Player.Auras.Remove(Aura.ChallengingShout);

            foreach (var enemy in state.Enemies)
            {
                // TODO: This is assuming that Challenging Shout is the only source of Taunt
                enemy.Auras.Remove(Aura.Taunt);
            }
        }
    }
}
