using BarbarianSim.Events;

namespace BarbarianSim.Abilities
{
    public class SteadyShot
    {
        public static bool CanUse(SimulationState state) => !state.Auras.Contains(Aura.CastInProgress) && !state.Auras.Contains(Aura.GlobalCooldown);

        public static void Use(SimulationState state) => state.Events.Add(new SteadyShotCastEvent(state.CurrentTime));
    }
}
