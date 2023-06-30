using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.Events
{
    public class LungingStrikeEvent : EventInfo
    {
        public LungingStrikeEvent(double timestamp) : base(timestamp)
        { }

        public DamageEvent DamageEvent { get; set; }

        public override void ProcessEvent(SimulationState state)
        {
            var weaponDamage = (LungingStrike.Weapon.MinDamage + LungingStrike.Weapon.MaxDamage) / 2.0;
            var skillFactor = LungingStrike.GetSkillMultiplier(state.Config.Skills[Skill.LungingStrike]);
            var additiveDamageBonus = AdditiveDamageBonusCalculator.Calculate(state);
            var multiplicativeDamageBonus = MultiplicativeDamageBonusCalculator.Calculate(state);
            var vulnerableDamageBonus = VulnerableDamageBonusCalculator.Calculate(state);

            var damage = weaponDamage * additiveDamageBonus * multiplicativeDamageBonus * vulnerableDamageBonus;
            var damageType = DamageType.Direct;

            var critChance = CritChanceCalculator.Calculate(state);
            var critRoll = RandomGenerator.Roll(RollType.CriticalStrike);

            if (critRoll <= critChance)
            {
                damage *= CritDamageCalculator.Calculate(state);
                damageType = DamageType.DirectCrit;
            }

            DamageEvent = new DamageEvent(Timestamp, damage, damageType);
            state.Events.Add(DamageEvent);

            var weaponSpeed = LungingStrike.Weapon.AttacksPerSecond * AttackSpeedCalculator.Calculate(state);
            state.Auras.Add(Aura.WeaponCooldown);
            state.Events.Add(new WeaponAuraCooldownCompletedEvent(Timestamp + weaponSpeed));
        }
    }
}
