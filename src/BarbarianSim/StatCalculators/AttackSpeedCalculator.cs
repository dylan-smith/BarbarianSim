namespace BarbarianSim.StatCalculators
{
    public class AttackSpeedCalculator : BaseStatCalculator
    {
        public static double Calculate(SimulationState state) => Calculate<AttackSpeedCalculator>(state);

        protected override double InstanceCalculate(SimulationState state)
        {
            return 0.0;
        }
    }
}
