namespace HunterSim
{
    public static class MasterTactician
    {
        public static void ProcessEvent(AutoShotCompletedEvent e, SimulationState state)
        {
            if (e.DamageEvent.DamageType == DamageType.Miss)
            {
                return;
            }

            if (state.Config.Talents.ContainsKey(Talent.MasterTactician))
            {
                var roll = RandomGenerator.Roll(RollType.MasterTacticianProc);

                if (roll <= 0.06)
                {
                    state.Events.Add(new MasterTacticianProcEvent(e.Timestamp));
                }
            }
        }
    }
}
