using HunterSim.Events;
using System.Linq;

namespace HunterSim
{
    public class AutoShot
    {
        public static bool CanUse(SimulationState state) => !state.Auras.Contains(Aura.AutoShotOnCooldown);

        public static void Use(SimulationState state) => state.Events.Add(new AutoShotCastEvent(state.CurrentTime));

        public static void ProcessEvent(CastStartedEvent _, SimulationState state)
        {
            if (state.Events.Any(x => x is AutoShotCompletedEvent))
            {
                // TODO: auto shot cast interrupted, log this somehow
                state.Events.RemoveAll(x => x is AutoShotCompletedEvent);
            }
        }
    }
}
