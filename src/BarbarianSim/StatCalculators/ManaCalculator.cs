namespace HunterSim
{
    public class ManaCalculator : BaseStatCalculator
    {
        public static double Calculate(SimulationState state) => Calculate<ManaCalculator>(state);

        protected override double InstanceCalculate(SimulationState state)
        {
            var mana = state.Config.PlayerSettings.Mana;
            mana += IntellectCalculator.Calculate(state) * 15;

            return mana;
        }
    }
}
