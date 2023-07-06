using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.Events;

public class WhirlwindStartedEvent : EventInfo
{
    public WhirlwindStartedEvent(double timestamp) : base(timestamp)
    { }

    public IList<DamageEvent> DamageEvents { get; init; } = new List<DamageEvent>();
    public FurySpentEvent FurySpentEvent { get; set; }
    public WhirlwindRefreshEvent WhirlwindRefreshEvent { get; set; }

    public override void ProcessEvent(SimulationState state)
    {
        state.Player.Auras.Add(Aura.Whirlwinding);

        var weaponDamage = (Whirlwind.Weapon.MinDamage + Whirlwind.Weapon.MaxDamage) / 2.0;
        var skillMultiplier = Whirlwind.GetSkillMultiplier(state);
        var damageMultiplier = TotalDamageMultiplierCalculator.Calculate(state, DamageType.Physical);

        var damage = weaponDamage * skillMultiplier * damageMultiplier;
        var critChance = CritChanceCalculator.Calculate(state, DamageType.Physical);

        for (var i = 0; i < state.Config.EnemySettings.NumberOfEnemies; i++)
        {
            var enemyDamage = damage;
            var damageType = DamageType.Direct;
            var critRoll = RandomGenerator.Roll(RollType.CriticalStrike);

            if (critRoll <= critChance)
            {
                enemyDamage *= CritDamageCalculator.Calculate(state);
                damageType = DamageType.DirectCrit;
            }

            var damageEvent = new DamageEvent(Timestamp, enemyDamage, damageType, DamageSource.Whirlwind);
            DamageEvents.Add(damageEvent);
            state.Events.Add(damageEvent);
        }

        FurySpentEvent = new FurySpentEvent(Timestamp, Whirlwind.FURY_COST);
        state.Events.Add(FurySpentEvent);

        var weaponSpeed = 1 / Whirlwind.Weapon.AttacksPerSecond;
        weaponSpeed *= AttackSpeedCalculator.Calculate(state);
        state.Player.Auras.Add(Aura.WeaponCooldown);
        state.Events.Add(new WeaponAuraCooldownCompletedEvent(Timestamp + weaponSpeed));

        WhirlwindRefreshEvent = new WhirlwindRefreshEvent(Timestamp + weaponSpeed);
        state.Events.Add(WhirlwindRefreshEvent);
    }
}
