namespace BarbarianSim.StatCalculators
{
    public class MultiplicativeDamageBonusCalculator : BaseStatCalculator
    {
        public static double Calculate(SimulationState state) => Calculate<MultiplicativeDamageBonusCalculator>(state);

        protected override double InstanceCalculate(SimulationState state)
        {
            return 1.0;
        }
    }
}
