namespace HunterSim
{
    public class FireResistanceCalculator : BaseStatCalculator
    {
        public static double Calculate(SimulationState state) => Calculate<FireResistanceCalculator>(state);

        protected override double InstanceCalculate(SimulationState state)
        {
            var resist = state.Config.Gear.GetStatTotal(x => x.FireResistance);

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
