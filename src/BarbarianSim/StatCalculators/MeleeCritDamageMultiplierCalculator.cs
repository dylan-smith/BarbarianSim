namespace HunterSim
{
    public class MeleeCritDamageMultiplierCalculator : BaseStatCalculator
    {
        public static double Calculate(SimulationState state) => Calculate<MeleeCritDamageMultiplierCalculator>(state);

        protected override double InstanceCalculate(SimulationState state)
        {
            var bossType = state.Config.BossSettings.BossType;

            var dmgMultiplier = 1.0;

            if (state.Config.Talents.ContainsKey(Talent.MonsterSlaying))
            {
                if (bossType == BossType.Beast || bossType == BossType.Giant || bossType == BossType.Dragonkin)
                {
                    dmgMultiplier += (0.01 * state.Config.Talents[Talent.MonsterSlaying]);
                }
            }

            if (state.Config.Talents.ContainsKey(Talent.HumanoidSlaying))
            {
                if (bossType == BossType.Humanoid)
                {
                    dmgMultiplier += (0.01 * state.Config.Talents[Talent.HumanoidSlaying]);
                }
            }

            if (state.Auras.Contains(Aura.RelentlessEarthstormDiamond))
            {
                dmgMultiplier += 0.03;
            }

            return dmgMultiplier;
        }
    }
}
