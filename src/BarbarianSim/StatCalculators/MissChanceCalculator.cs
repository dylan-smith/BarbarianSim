using System;

namespace HunterSim
{
    public class MissChanceCalculator : BaseStatCalculator
    {
        public static double Calculate(GearItem weapon, SimulationState state) => Calculate<MissChanceCalculator>(weapon, state);

        protected override double InstanceCalculate(GearItem weapon, SimulationState state)
        {
            var bossDefense = state.Config.BossSettings.Defense;
            var weaponSkill = WeaponSkillCalculator.Calculate(weapon, state);
            double missChance;

            if (bossDefense - weaponSkill > 10)
            {
                missChance = 0.07 + ((bossDefense - weaponSkill - 10) * 0.004);
            }
            else
            {
                missChance = 0.05 + ((bossDefense - weaponSkill) * 0.001);
            }

            // https://tbc.wowhead.com/guides/classic-the-burning-crusade-stats-overview
            missChance -= state.Config.Gear.GetStatTotal(x => x.HitRating) / 1580;

            if (state.Config.Talents.ContainsKey(Talent.Surefooted))
            {
                missChance -= state.Config.Talents[Talent.Surefooted] * 0.01;
            }

            // TODO: Draenai 1% hit

            missChance = Math.Max(missChance, 0.0);

            return missChance;
        }
    }
}
