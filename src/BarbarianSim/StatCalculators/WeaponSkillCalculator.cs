namespace HunterSim
{
    public class WeaponSkillCalculator : BaseStatCalculator
    {
        public static double Calculate(GearItem weapon, SimulationState state) => Calculate<WeaponSkillCalculator>(weapon, state);

        protected override double InstanceCalculate(GearItem weapon, SimulationState state)
        {
            // TODO: Is weapon skill completely gone in TBC?
            var skill = state.Config.PlayerSettings.Level * 5;
            var weaponType = weapon.WeaponType;

            // TODO: Need to test this
            skill += (int)state.Config.Gear.GetStatTotal(x => x.WeaponSkill.ContainsKey(weaponType) ? x.WeaponSkill[weaponType] : 0.0);

            if (weaponType == WeaponType.Gun && state.Config.PlayerSettings.Race == Race.Dwarf)
            {
                skill += 5;
            }

            if ((weaponType == WeaponType.OneHandedAxe || weaponType == WeaponType.TwoHandedAxe) && state.Config.PlayerSettings.Race == Race.Orc)
            {
                skill += 5;
            }

            if ((weaponType == WeaponType.Bow || weaponType == WeaponType.Thrown) && state.Config.PlayerSettings.Race == Race.Troll)
            {
                skill += 5;
            }

            return skill;
        }
    }
}
