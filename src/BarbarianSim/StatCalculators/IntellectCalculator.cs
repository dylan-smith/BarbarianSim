namespace BarbarianSim
{
    public class IntellectCalculator : BaseStatCalculator
    {
        public static double Calculate(SimulationState state) => Calculate<IntellectCalculator>(state);

        protected override double InstanceCalculate(SimulationState state)
        {
            var intellect = state.Config.PlayerSettings.Intellect;
            intellect += state.Config.Gear.GetStatTotal(x => x.Intellect);

            if (state.Config.Buffs.Contains(Buff.MarkOfTheWild))
            {
                intellect += 14;
            }

            if (state.Config.Buffs.Contains(Buff.ImprovedMarkOfTheWild))
            {
                intellect += (14 * 1.35).Floor();
            }

            if (state.Config.Buffs.Contains(Buff.ArcaneBrilliance))
            {
                intellect += 40;
            }

            if (state.Config.Talents.ContainsKey(Talent.CombatExperience))
            {
                intellect *= 1 + (0.03 * state.Config.Talents[Talent.CombatExperience]);
                intellect = intellect.Floor();
            }

            if (state.Config.Buffs.Contains(Buff.BlessingOfKings))
            {
                intellect *= 1.1;
                intellect = intellect.Floor();
            }

            return intellect;
        }
    }
}
