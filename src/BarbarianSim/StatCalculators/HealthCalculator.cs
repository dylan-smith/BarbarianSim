namespace BarbarianSim
{
    public class HealthCalculator : BaseStatCalculator
    {
        public static double Calculate(SimulationState state) => Calculate<HealthCalculator>(state);

        protected override double InstanceCalculate(SimulationState state)
        {
            var health = state.Config.PlayerSettings.Health;
            health += StaminaCalculator.Calculate(state) * 10;

            if (state.Config.Talents.ContainsKey(Talent.EnduranceTraining))
            {
                health *= 1 + (state.Config.Talents[Talent.EnduranceTraining] * 0.01);
            }

            if (state.Config.Talents.ContainsKey(Talent.Survivalist))
            {
                health *= 1 + (state.Config.Talents[Talent.Survivalist] * 0.02);
            }

            return health;
        }
    }
}
