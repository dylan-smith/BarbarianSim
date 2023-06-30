namespace BarbarianSim.StatCalculators
{
    public class AdditiveDamageBonusCalculator : BaseStatCalculator
    {
        public static double Calculate(SimulationState state) => Calculate<AdditiveDamageBonusCalculator>(state);

        protected override double InstanceCalculate(SimulationState state)
        {
            var physicalDamage = state.Config.Gear.GetStatTotal(g => g.PhysicalDamage);
            var damageToClose = state.Config.Gear.GetStatTotal(g => g.DamageToClose);
            var damageToInjured = state.Enemy.IsInjured() ? state.Config.Gear.GetStatTotal(g => g.DamageToInjured) : 0.0;
            var damageToSlowed = state.Enemy.IsSlowed() ? state.Config.Gear.GetStatTotal(g => g.DamageToSlowed) : 0.0;
            var damageToCrowdControlled = state.Enemy.IsCrowdControlled() ? state.Config.Gear.GetStatTotal(g => g.DamageToCrowdControlled) : 0.0;

            var bonus = physicalDamage + damageToClose + damageToInjured + damageToSlowed + damageToCrowdControlled;

            return 1.0 + (bonus / 100.0);
        }
    }
}
