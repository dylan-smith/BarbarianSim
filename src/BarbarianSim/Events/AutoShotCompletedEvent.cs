namespace HunterSim
{
    public class AutoShotCompletedEvent : EventInfo
    {
        public DamageEvent DamageEvent { get; set; }

        public AutoShotCompletedEvent(double timestamp) : base(timestamp)
        { }

        public override void ProcessEvent(SimulationState state)
        {
            double autoShotDamage;
            DamageType damageType;

            var missChance = MissChanceCalculator.Calculate(state.Config.Gear.Ranged, state);
            var critChance = RangedCritCalculator.Calculate(state);

            var missRoll = RandomGenerator.Roll(RollType.AutoShotMiss);

            if (missRoll <= missChance)
            {
                autoShotDamage = 0.0;
                damageType = DamageType.Miss;
            }
            else
            {
                var rangedAP = RangedAttackPowerCalculator.Calculate(state);

                autoShotDamage = (state.Config.Gear.Ranged.MinDamage + state.Config.Gear.Ranged.MaxDamage) / 2;
                autoShotDamage += RangedBonusDamageCalculator.Calculate(state.Config.Gear.Ranged, state);
                autoShotDamage += (rangedAP / 14) * state.Config.Gear.Ranged.Speed; // 14 RAP = 1 DPS
                autoShotDamage *= RangedDamageMultiplierCalculator.Calculate(state);

                damageType = DamageType.Hit;

                // Assuming crit uses a 2-roll system as per this article:
                // https://wowwiki-archive.fandom.com/wiki/Attack_table#Ranged_attacks
                var critRoll = RandomGenerator.Roll(RollType.AutoShotCrit);

                if (critRoll <= critChance)
                {
                    // TODO: is bonus damage (scope) and bonus dps (ammo) doubled when you crit? This assumes yes
                    autoShotDamage *= 2;
                    autoShotDamage *= RangedCritDamageMultiplierCalculator.Calculate(state);
                    damageType = DamageType.Crit;
                }
            }

            // TODO: Boss armor reduction

            // TODO: If we do these calcs earlier we can do just one roll instead of 2
            critChance *= (1 - missChance);
            var hitChance = 1 - missChance - critChance;

            DamageEvent = new DamageEvent(Timestamp, autoShotDamage, damageType, missChance, critChance, hitChance);
            state.Events.Add(DamageEvent);
        }
    }
}
