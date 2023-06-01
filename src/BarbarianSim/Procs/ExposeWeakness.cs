namespace HunterSim
{
    public static class ExposeWeakness
    {
        public static int AttackPower { get; set; } = 0;

        public static void ProcessEvent(AutoShotCompletedEvent e, SimulationState state)
        {
            if (e.DamageEvent.DamageType != DamageType.Crit)
            {
                return;
            }

            if (state.Config.Talents.ContainsKey(Talent.ExposeWeakness))
            {
                var procChance = state.Config.Talents[Talent.ExposeWeakness] / 3.0;

                var roll = RandomGenerator.Roll(RollType.ExposeWeaknessProc);

                if (roll <= procChance)
                {
                    state.Events.Add(new ExposeWeaknessProcEvent(e.Timestamp));
                }
            }
        }
    }
}
