namespace HunterSim
{
    public class NatureResistanceCalculator : BaseStatCalculator
    {
        public static double Calculate(SimulationState state) => Calculate<NatureResistanceCalculator>(state);

        protected override double InstanceCalculate(SimulationState state)
        {
            var resist = state.Config.Gear.GetStatTotal(x => x.NatureResistance);

            if (state.Config.Buffs.Contains(Buff.MarkOfTheWild))
            {
                resist += 25;
            }

            if (state.Config.Buffs.Contains(Buff.ImprovedMarkOfTheWild))
            {
                resist += (25 * 1.35).Floor();
            }

            return resist;
        }
    }
}
