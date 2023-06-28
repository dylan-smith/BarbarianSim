namespace BarbarianSim
{
    public class DodgeCalculator : BaseStatCalculator
    {
        public static double Calculate(SimulationState state) => Calculate<DodgeCalculator>(state);

        protected override double InstanceCalculate(SimulationState state)
        {
            // TODO: I think hunters might start with a base dodge that's negative (https://wowwiki-archive.fandom.com/wiki/Dodge)
            var dodgeRating = state.Config.Gear.GetStatTotal(x => x.DodgeRating);

            // https://www.reddit.com/r/burningcrusade/comments/ka0tb6/tbc_combat_rating_haste_hit_etc_conversions_at/
            // https://tbc.wowhead.com/guides/classic-the-burning-crusade-stats-overview
            var dodge = dodgeRating / 1890.0;

            // https://tbc.wowhead.com/guides/classic-the-burning-crusade-stats-overview
            dodge += AgilityCalculator.Calculate(state) / 2500.0;

            if (state.Auras.Contains(Aura.AspectOfTheMonkey))
            {
                dodge += 0.08;

                if (state.Config.Talents.ContainsKey(Talent.ImprovedAspectOfTheMonkey))
                {
                    dodge += 0.02 * state.Config.Talents[Talent.ImprovedAspectOfTheMonkey];
                }
            }

            if (state.Config.Talents.ContainsKey(Talent.CatlikeReflexes))
            {
                dodge += 0.01 * state.Config.Talents[Talent.CatlikeReflexes];
            }

            // TODO: Defense increases dodge also (25 def == 1% dodge)

            return dodge;
        }
    }
}
