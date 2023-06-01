namespace HunterSim
{
    public class RangedHasteCalculator : BaseStatCalculator
    {
        public static double Calculate(SimulationState state) => Calculate<RangedHasteCalculator>(state);

        protected override double InstanceCalculate(SimulationState state)
        {
            var haste = 1.0;
            // 15.8 rating = 1% haste https://tbc.wowhead.com/guides/classic-the-burning-crusade-stats-overview\
            haste += state.Config.Gear.GetStatTotal(x => x.HasteRating) / 1580;

            if (state.Auras.Contains(Aura.ImprovedAspectOfTheHawk))
            {
                haste += 0.03 * state.Config.Talents[Talent.ImprovedAspectOfTheHawk];
            }

            if (state.Config.Talents.ContainsKey(Talent.SerpentsSwiftness))
            {
                haste += 0.04 * state.Config.Talents[Talent.SerpentsSwiftness];
            }

            return haste;
        }
    }
}
