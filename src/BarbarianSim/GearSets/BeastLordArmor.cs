namespace HunterSim.GearSets
{
    public class BeastLordArmor : IGearSet
    {
        public void Apply(SimulationState state)
        {
            var count = state.Config.Gear.GetGearCount(28228, 27474, 28275, 27874, 27801);

            state.Auras.Remove(Aura.TrapCooldown);
            state.Auras.Remove(Aura.ImprovedKillCommand);

            if (count >= 2)
            {
                state.Auras.Add(Aura.TrapCooldown);
            }

            if (count >= 4)
            {
                state.Auras.Add(Aura.ImprovedKillCommand);
            }
        }
    }
}
