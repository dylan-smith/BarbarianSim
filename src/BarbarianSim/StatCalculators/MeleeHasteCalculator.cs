namespace HunterSim
{
    public class MeleeHasteCalculator : BaseStatCalculator
    {
        public static double Calculate(SimulationState state) => Calculate<MeleeHasteCalculator>(state);

        protected override double InstanceCalculate(SimulationState state)
        {
            var haste = 1.0;
            // 15.8 rating = 1% haste https://tbc.wowhead.com/guides/classic-the-burning-crusade-stats-overview\
            haste += state.Config.Gear.GetStatTotal(x => x.HasteRating) / 1580;

            return haste;
        }
    }
}
