namespace BarbarianSim
{
    public class AttackPowerCalculator : BaseStatCalculator
    {
        public static double Calculate(SimulationState state) => Calculate<AttackPowerCalculator>(state);

        protected override double InstanceCalculate(SimulationState state)
        {
            var rangedAP = state.Config.Gear.GetStatTotal(x => x.AttackPower);
            rangedAP += AgilityCalculator.Calculate(state);

            if (state.Config.Talents.ContainsKey(Talent.TrueshotAura) || state.Config.Buffs.Contains(Buff.TrueshotAura))
            {
                rangedAP += 125;
            }

            if (state.Config.Buffs.Contains(Buff.BlessingOfMight))
            {
                rangedAP += 220;
            }

            if (state.Config.Buffs.Contains(Buff.ImprovedBlessingOfMight))
            {
                rangedAP += 264;
            }

            if (state.Auras.Contains(Aura.ExposeWeakness))
            {
                rangedAP += ExposeWeakness.AttackPower;
            }

            // TODO: Orc Bloodfury

            return rangedAP;
        }
    }
}
