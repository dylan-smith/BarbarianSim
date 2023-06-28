namespace BarbarianSim
{
    public class DamageMultiplierCalculator : BaseStatCalculator
    {
        public static double Calculate(SimulationState state) => Calculate<DamageMultiplierCalculator>(state);

        protected override double InstanceCalculate(SimulationState state)
        {
            var bossType = state.Config.BossSettings.BossType;
            var damageMultiplier = 1.0;

            if (state.Config.Talents.ContainsKey(Talent.MonsterSlaying))
            {
                if (bossType == BossType.Beast || bossType == BossType.Giant || bossType == BossType.Dragonkin)
                {
                    damageMultiplier *= 1 + (0.01 * state.Config.Talents[Talent.MonsterSlaying]);
                }
            }

            if (state.Config.Talents.ContainsKey(Talent.HumanoidSlaying))
            {
                if (bossType == BossType.Humanoid)
                {
                    damageMultiplier *= 1 + (0.01 * state.Config.Talents[Talent.HumanoidSlaying]);
                }
            }

            if (state.Config.Talents.ContainsKey(Talent.FocusedFire))
            {
                damageMultiplier *= 1 + (0.01 * state.Config.Talents[Talent.FocusedFire]);
            }

            if (state.Auras.Contains(Aura.TheBeastWithin))
            {
                damageMultiplier *= 1.1;
            }

            return damageMultiplier;
        }
    }
}
