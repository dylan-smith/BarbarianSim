namespace BarbarianSim.StatCalculators
{
    public class VulnerableDamageBonusCalculator : BaseStatCalculator
    {
        public static double Calculate(SimulationState state) => Calculate<VulnerableDamageBonusCalculator>(state);

        protected override double InstanceCalculate(SimulationState state)
        {
            return 0.0;
        }
    }
}
