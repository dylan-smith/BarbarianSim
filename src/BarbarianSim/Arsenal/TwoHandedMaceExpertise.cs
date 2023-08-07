using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Arsenal;

public class TwoHandedMaceExpertise : IHandlesEvent<LuckyHitEvent>
{
    // Up to 10% chance to gain 2 Fury when hitting an enemy. Double the amount of Fury gained while Berserking
    // Rank 10 Bonus: You deal 15%[x] increased Critical Strike Damage to Stunned and Vulnerable enemies while Berserking
    public const double FURY_GENERATED = 2;
    public const double PROC_CHANCE = 0.1;
    public const double CRIT_DAMAGE = 1.15;

    public TwoHandedMaceExpertise(RandomGenerator randomGenerator, SimLogger log)
    {
        _randomGenerator = randomGenerator;
        _log = log;
    }

    private readonly RandomGenerator _randomGenerator;
    private readonly SimLogger _log;

    public void ProcessEvent(LuckyHitEvent e, SimulationState state)
    {
        if (e.Weapon?.Expertise == Expertise.TwoHandedMace || state.Config.PlayerSettings.ExpertiseTechnique == Expertise.TwoHandedMace)
        {
            var procRoll = _randomGenerator.Roll(RollType.TwoHandedMaceExpertise);

            if (procRoll <= PROC_CHANCE)
            {
                state.Events.Add(new TwoHandedMaceExpertiseProcEvent(e.Timestamp));
                _log.Verbose($"2-Handed Mace Expertise procced and created TwoHandedMaceExpertiseProcEvent");
            }
        }
    }

    public virtual double GetCriticalStrikeDamageMultiplier(SimulationState state, GearItem weapon, EnemyState enemy)
    {
        return weapon?.Expertise == Expertise.TwoHandedMace
            && (enemy.IsStunned() || enemy.IsVulnerable())
            && state.Player.Auras.Contains(Aura.Berserking)
                ? CRIT_DAMAGE
                : 1.0;
    }
}
