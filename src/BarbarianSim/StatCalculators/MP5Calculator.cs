namespace HunterSim
{
    public class MP5Calculator : BaseStatCalculator
    {
        public static double Calculate(SimulationState state) => Calculate<MP5Calculator>(state);

        protected override double InstanceCalculate(SimulationState state)
        {
            var mp5 = state.Config.Gear.GetStatTotal(x => x.MP5);

            // TODO: Does spirit affect this?

            return mp5;
        }
    }
}
