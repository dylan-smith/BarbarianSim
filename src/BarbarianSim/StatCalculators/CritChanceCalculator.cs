namespace BarbarianSim.StatCalculators
{
    public class CritChanceCalculator : BaseStatCalculator
    {
        public static double Calculate(SimulationState state) => Calculate<CritChanceCalculator>(state);

        protected override double InstanceCalculate(SimulationState state)
        {
            return 0.0;
        }
    }
}
