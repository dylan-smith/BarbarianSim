namespace BarbarianSim.StatCalculators
{
    public class CritDamageCalculator : BaseStatCalculator
    {
        public static double Calculate(SimulationState state) => Calculate<CritDamageCalculator>(state);

        protected override double InstanceCalculate(SimulationState state)
        {
            return 0.0;
        }
    }
}
