namespace HunterSim
{
    public class RangedAttackPowerCalculator : BaseStatCalculator
    {
        public static double Calculate(SimulationState state) => Calculate<RangedAttackPowerCalculator>(state);

        protected override double InstanceCalculate(SimulationState state)
        {
            var rangedAP = AttackPowerCalculator.Calculate(state);
            rangedAP += state.Config.Gear.GetStatTotal(x => x.RangedAttackPower);

            // This appears to be base RAP, I tested it by removing all gear and talents
            rangedAP += 130;

            if (state.Auras.Contains(Aura.AspectOfTheHawk))
            {
                rangedAP += 155;
            }

            if (state.Config.Talents.ContainsKey(Talent.CarefulAim))
            {
                var intellect = IntellectCalculator.Calculate(state);
                rangedAP += intellect * (0.15 * state.Config.Talents[Talent.CarefulAim]);
                rangedAP = rangedAP.Floor();
            }

            if (state.Config.Talents.ContainsKey(Talent.MasterMarksman))
            {
                rangedAP *= 1 + (0.02 * state.Config.Talents[Talent.MasterMarksman]);
                rangedAP = rangedAP.Floor();
            }

            if (state.Config.Talents.ContainsKey(Talent.SurvivalInstincts))
            {
                rangedAP *= 1 + (0.02 * state.Config.Talents[Talent.SurvivalInstincts]);
                rangedAP = rangedAP.Floor();
            }

            if (state.Config.Buffs.Contains(Buff.HuntersMark) || state.Config.Buffs.Contains(Buff.ImprovedHuntersMark))
            {
                rangedAP += 440;
            }

            return rangedAP;
        }
    }
}
