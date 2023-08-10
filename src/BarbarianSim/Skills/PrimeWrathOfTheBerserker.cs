using BarbarianSim.Enums;

namespace BarbarianSim.Skills;

public class PrimeWrathOfTheBerserker
{
    // While Wrath of the Berserker is active, gain 20%[+] increased Movement Speed and increase Fury Generation by 30%[x]
    public const double MOVEMENT_SPEED_INCREASE = 20;
    public const double FURY_GENERATION_INCREASE = 1.3;

    public PrimeWrathOfTheBerserker(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public virtual double GetMovementSpeedIncrease(SimulationState state)
    {
        if (state.Config.GetSkillPoints(Skill.PrimeWrathOfTheBerserker) > 0 && state.Player.Auras.Contains(Aura.WrathOfTheBerserker))
        {
            _log.Verbose($"Movement Speed increase from Prime Wrath of the Berserker = {MOVEMENT_SPEED_INCREASE:F2}%");
            return MOVEMENT_SPEED_INCREASE;
        }

        return 0;
    }

    public virtual double GetResourceGeneration(SimulationState state)
    {
        if (state.Config.GetSkillPoints(Skill.PrimeWrathOfTheBerserker) > 0 && state.Player.Auras.Contains(Aura.WrathOfTheBerserker))
        {
            _log.Verbose($"Resource Generation increase from Prime Wrath of the Berserker = {FURY_GENERATION_INCREASE:F2}x");
            return FURY_GENERATION_INCREASE;
        }

        return 1;
    }
}
