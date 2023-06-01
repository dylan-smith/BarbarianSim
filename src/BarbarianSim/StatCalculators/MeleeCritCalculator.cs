using System;

namespace HunterSim
{
    public class MeleeCritCalculator : BaseStatCalculator
    {
        public static double Calculate(SimulationState state) => Calculate<MeleeCritCalculator>(state);

        protected override double InstanceCalculate(SimulationState state)
        {
            var critChance = CritCalculator.Calculate(state);
            critChance += state.Config.Gear.GetStatTotal(x => x.MeleeCritRating) / 2208; // 22.08 rating = 1% crit (at level 70)

            critChance = Math.Max(critChance, 0);
            critChance = Math.Min(critChance, 1.0);

            return critChance;
        }
    }
}
