namespace BarbarianSim.StatCalculators
{
    public class AdditiveDamageBonusCalculator : BaseStatCalculator
    {
        public static double Calculate(SimulationState state) => Calculate<AdditiveDamageBonusCalculator>(state);

        protected override double InstanceCalculate(SimulationState state)
        {
            return 0.0;
        }
    }
}
