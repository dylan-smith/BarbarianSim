namespace BarbarianSim
{
    public class RangedBonusDamageCalculator : BaseStatCalculator
    {
        public static double Calculate(GearItem weapon, SimulationState state) => Calculate<RangedBonusDamageCalculator>(weapon, state);

        protected override double InstanceCalculate(GearItem weapon, SimulationState state)
        {
            var bonusDamage = state.Config.Gear.GetStatTotal(x => x.RangedBonusDamage);
            bonusDamage += state.Config.Gear.GetStatTotal(x => x.RangedBonusDPS) * weapon.Speed;

            if (state.Auras.Contains(Aura.AimedShot))
            {
                bonusDamage += 70;
            }

            return bonusDamage;
        }
    }
}
