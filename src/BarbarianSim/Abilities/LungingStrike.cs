using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.EventFactories;

namespace BarbarianSim.Abilities;

public class LungingStrike
{
    public const double LUCKY_HIT_CHANCE = 0.5;
    public const double FURY_GENERATED = 10;

    public LungingStrike(LungingStrikeEventFactory lungingStrikeEventFactory) => _lungingStrikeEventFactory = lungingStrikeEventFactory;

    private readonly LungingStrikeEventFactory _lungingStrikeEventFactory;

    // Lunge forward and strike an enemy for 33% damage
    public bool CanUse(SimulationState state) => !state.Player.Auras.Contains(Aura.WeaponCooldown);

    public void Use(SimulationState state, EnemyState target) => state.Events.Add(_lungingStrikeEventFactory.Create(state.CurrentTime, target));

    public GearItem Weapon { get; set; }

    public double GetSkillMultiplier(SimulationState state)
    {
        var skillPoints = state?.Config.Skills[Skill.LungingStrike];
        skillPoints += state?.Config.Gear.AllGear.Sum(g => g.LungingStrike);

        return skillPoints switch
        {
            1 => 0.33,
            2 => 0.36,
            3 => 0.39,
            4 => 0.42,
            >= 5 => 0.45,
            _ => 0.0,
        };
    }
}
