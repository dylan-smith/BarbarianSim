namespace HunterSim
{
    public class RangedDamageMultiplierCalculator : BaseStatCalculator
    {
        public static double Calculate(SimulationState state) => Calculate<RangedDamageMultiplierCalculator>(state);

        protected override double InstanceCalculate(SimulationState state)
        {
            var damageMultiplier = DamageMultiplierCalculator.Calculate(state);

            if (state.Config.Talents.ContainsKey(Talent.RangedWeaponSpecialization))
            {
                damageMultiplier += 0.01 * state.Config.Talents[Talent.RangedWeaponSpecialization];
            }

            return damageMultiplier;
        }
    }
}
