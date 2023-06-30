using BarbarianSim.Config;
using BarbarianSim.Events;

namespace BarbarianSim.Abilities
{
    public static class LungingStrike
    {
        public static bool CanUse(SimulationState state) => !state.Auras.Contains(Aura.WeaponCooldown);

        public static void Use(SimulationState state) => state.Events.Add(new LungingStrikeEvent(state.CurrentTime));

        public static GearItem Weapon { get; set; }

        public static double GetSkillMultiplier(int skillLevel)
        {
            return skillLevel switch
            {
                1 => 1.33,
                2 => 1.36,
                3 => 1.39,
                4 => 1.42,
                5 => 1.45,
                _ => 0.0,
            };
        }
    }
}
