namespace BarbarianSim.StatCalculators
{
    public class CritDamageCalculator : BaseStatCalculator
    {
        public static double Calculate(SimulationState state) => Calculate<CritDamageCalculator>(state);

        protected override double InstanceCalculate(SimulationState state)
        {
            var critDamage = state.Config.Gear.AllGear.Sum(g => g.CritDamage);

            return 1.5 + (critDamage / 100.0);
        }
    }
}
