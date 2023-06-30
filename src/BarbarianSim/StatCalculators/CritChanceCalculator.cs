namespace BarbarianSim.StatCalculators
{
    public class CritChanceCalculator : BaseStatCalculator
    {
        public static double Calculate(SimulationState state) => Calculate<CritChanceCalculator>(state);

        protected override double InstanceCalculate(SimulationState state)
        {
            var critChance = state.Config.Gear.AllGear.Sum(g => g.CritChance);

            if (state.Config.EnemySettings.IsElite)
            {
                critChance += state.Config.Gear.AllGear.Sum(g => g.CritChancePhysicalAgainstElites);
            }

            return critChance / 100.0;
        }
    }
}
