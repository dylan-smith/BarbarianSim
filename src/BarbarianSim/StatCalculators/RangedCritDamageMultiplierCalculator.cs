namespace HunterSim
{
    public class RangedCritDamageMultiplierCalculator : BaseStatCalculator
    {
        public static double Calculate(SimulationState state) => Calculate<RangedCritDamageMultiplierCalculator>(state);

        protected override double InstanceCalculate(SimulationState state)
        {
            var bossType = state.Config.BossSettings.BossType;
            var critMultiplier = 1.0;

            if (state.Config.Talents.ContainsKey(Talent.MonsterSlaying))
            {
                if (bossType == BossType.Beast || bossType == BossType.Giant || bossType == BossType.Dragonkin)
                {
                    critMultiplier += (0.01 * state.Config.Talents[Talent.MonsterSlaying]);
                }
            }

            if (state.Config.Talents.ContainsKey(Talent.HumanoidSlaying))
            {
                if (bossType == BossType.Humanoid)
                {
                    critMultiplier += (0.01 * state.Config.Talents[Talent.HumanoidSlaying]);
                }
            }

            if (state.Config.Talents.ContainsKey(Talent.MortalShots))
            {
                // Mortal shots only increases the BONUS crit damage by 6%, so overall damage is multiplied by 0.03
                // TODO: not sure if that is actually how the math is supposed to work
                critMultiplier += state.Config.Talents[Talent.MortalShots] * 0.03;
            }

            if (state.Auras.Contains(Aura.RelentlessEarthstormDiamond))
            {
                critMultiplier += 0.03;
            }

            return critMultiplier;
        }
    }
}
