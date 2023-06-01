using System;

namespace HunterSim
{
    public class RangedCritCalculator : BaseStatCalculator
    {
        public static double Calculate(SimulationState state) => Calculate<RangedCritCalculator>(state);

        protected override double InstanceCalculate(SimulationState state)
        {
            var critChance = CritCalculator.Calculate(state);
            critChance += state.Config.Gear.GetStatTotal(x => x.RangedCritRating) / 2208; // 22.08 rating = 1% crit (at level 70)

            if (state.Config.Talents.ContainsKey(Talent.LethalShots))
            {
                critChance += state.Config.Talents[Talent.LethalShots] * 0.01;
            }

            // TODO: Trolls get 1% crit with bow racial

            critChance = Math.Max(critChance, 0);
            critChance = Math.Min(critChance, 1.0);

            return critChance;
        }
    }
}
