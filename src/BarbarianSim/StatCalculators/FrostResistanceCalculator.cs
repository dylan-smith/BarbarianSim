namespace HunterSim
{
    public class FrostResistanceCalculator : BaseStatCalculator
    {
        public static double Calculate(SimulationState state) => Calculate<FrostResistanceCalculator>(state);

        protected override double InstanceCalculate(SimulationState state)
        {
            var resist = state.Config.Gear.GetStatTotal(x => x.FrostResistance);

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
